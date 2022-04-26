<template>
    <h2>
        Schedule Builder
    </h2>
    <div>
        <h3 v-if="title != ''">{{ title }}</h3><h3 v-if="scheduleId != null"> ({{ scheduleId }})</h3>
        <input v-model="title" />
    </div>
    <div>
        <button @click="save">Save Schedule</button>
        <button @click="onBack">Return to Selection</button>
    </div>
    <br />
    <div v-if="items != null">
        <CreateItemForms @item-added="addItem" :nextId="nextId" :submitText="createButtonText" />
    </div>
    <br />
    <div v-if="items != null">
        <Schedules
                   :items="items"
                   :editableItems="true"
                   @item-updated="updateItem"
                   @item-deleted="deleteItem" />
    </div>
    <router-view />
</template>

<script>
    import router from '../../router'
    import * as $ from 'jquery'
    import Schedules from '../../components/ScheduleBuilder/Schedules'
    import CreateItemForms from '../../components/ScheduleBuilder/CreateItemForms'
    import URLS from '../../variables'
    export default {
        name: 'ScheduleBuilder',
        components: {
            Schedules,
            CreateItemForms,
        },
        data() {
            return {
                loading: false,
                items: [],
                demo: false,
                unsavedChanges: false,
                createButtonText: "Create",
                title: "",
                schedule: {},
            }
        },
        computed: {
            nextId() {
                if (this.items == null) {
                    return 0;
                }
                return this.items.length + 1;
            },
            ajaxContext() {
                return this;
            }
        },
        created() {
            //this.items = [];
            this.user = this.$route.params.user;
            this.scheduleId = this.$route.params.scheduleId;
            this.title = this.$route.params.title;
            this.created = this.$route.params.created;
            this.modified = this.$route.params.modified;
            console.log(this.user);
            console.log(this.schedule);
            if (!this.demo) {
                this.loadSchedule();
            }
            else {
                console.log("Demo");
                this.items = [
                    {
                        id: 1,
                        title: "{title}",
                        contact: "{Contact}",
                        location: "{location}",
                        notes: "{notes}",
                        days: [false, false, false, false, false, true, false],
                        startHour: 17,
                        startMinute: 30,
                        endHour: 18,
                        endMinute: 45,
                        editing: false
                    },
                    {
                        id: 2,
                        title: "CECS 277",
                        contact: "Cleary",
                        location: "ECS 407",
                        notes: "rocks",
                        days: [false, false, true, false, true, false, false],
                        startHour: 19,
                        startMinute: 0,
                        endHour: 21,
                        endMinute: 45,
                        editing: false
                    },
                    {
                        id: 3,
                        title: "CECS 228",
                        contact: "Nguyen",
                        location: "ECS 308",
                        notes: "is rough",
                        days: [false, true, false, true, false, false, false],
                        startHour: 9,
                        startMinute: 0,
                        endHour: 11,
                        endMinute: 15,
                        editing: false
                    },
                    {
                        id: 4,
                        title: "CECS 282",
                        contact: "Nguyen",
                        location: "ECS 308",
                        notes: "is ok",
                        days: [false, true, false, true, false, false, false],
                        startHour: 13,
                        startMinute: 0,
                        endHour: 15,
                        endMinute: 15,
                        editing: false
                    },
                    {
                        id: 5,
                        title: "CWL 305",
                        contact: "DeSuza",
                        location: "LA 305",
                        notes: "is fun",
                        days: [false, true, false, true, false, false, false],
                        startHour: 17,
                        startMinute: 0,
                        endHour: 18,
                        endMinute: 15,
                        editing: false
                    },
                ]
                console.log(this.items);
            }
        },
        methods: {
            // Make an AJAX request to get items on page load
            loadSchedule() {
                this.loading = true;
                this.items = [];
                let requestName = "LoadSchedule";
                //var scheduleItems = [];
                console.log(requestName);
                let scheduleItems = $.ajax({
                    // set the HTTP request URL
                    // url: `${baseURL}/schedule/getschedule/${this.user}/${this.scheduleId}`,
                    url: `${URLS.api.scheduleBuilder.getSchedule}/${this.user}/${this.scheduleId}`,

                    //contentType: 'application/json',
                    //datatype: 'json',

                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,

                    // HTTP method
                    method: 'GET',

                    // On a successful AJAX request:
                    success: function (data) {
                        scheduleItems = data;
                        this.items = data;
                        // log that we've completed
                        console.log(requestName + "- Success")
                        return data;
                    },

                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(requestName + "- Error")
                        console.log(error);
                        this.items = [];
                        // return false;
                    }
                });
                console.log("Unpacking...");
                // Unpack the data from the request
                // If there was an error, scheduleItems will be
                // empty and the loop won't execute
                //this.items = [];
                //scheduleItems = JSON.parse(scheduleItems.responseText);
                console.log(scheduleItems);
                
                this.loading = false;
                console.log("this.items:")
                console.log(this.items)
                console.log(scheduleItems)
                console.log("this.loading:")
                console.log(this.loading)
                        // return true;
            },
            // Creates a schedule item and adds it to this.items.
            addItem(newItem) {
                console.log("Adding item");
                newItem.creator = String(this.user);
                newItem.scheduleId = Number(this.scheduleId);
                this.items.push(newItem);
                console.log("Item added");
                this.unsavedChanges = true;
            },
            // Updates a schedule item
            updateItem(updatedItem) {
                console.log("ScheduleBuilder.updateItem()");
                let updated = false;
                for (let i = 0; i < this.items.length && !updated; i++) {
                    if (this.items[i].id === updatedItem.id) {
                        this.items[i] = updatedItem;
                        updated = true;
                        console.log("Successfully updated");
                    }
                }
                if (updated) {
                    this.unsavedChanges = true;
                }
            },
            // Deletes a schedule item based on ID.
            deleteItem(deleteableItem) {
                console.log("ScheduleBuilder.deleteItem()");
                console.log("Deleting item with id " + deleteableItem.id);
                let deleted = false;
                for (let i = 0; i < this.items.length && !deleted; i++) {
                    if (this.items[i].id === deleteableItem.id) {
                        this.items = this.items.filter(item => (item.id !== this.items[i].id));
                        deleted = true;
                        console.log("Successfully deleted from list");
                    }
                }
                if (!deleted) {
                    console.log("Could not delete");
                }
                else {
                    this.unsavedChanges = true;
                }
            },
            save() {
                let scheduleItems = [];
                for (let i = 0; i < this.items.length; i++) {
                    scheduleItems.push({
                        Id: this.items[i].id,
                        ScheduleId: this.scheduleId,
                        Creator: this.user,
                        Contact: this.items[i].contact,
                        DaysOfWeek: this.items[i].days,
                        StartHour: this.items[i].startHour,
                        StartMinute: this.items[i].startMinute,
                        EndHour: this.items[i].endHour,
                        EndMinute: this.items[i].endMinute,
                        Location: this.items[i].location,
                        Notes: this.items[i].notes,
                        Title: this.items[i].title
                    });
                }
                let data = JSON.stringify({
                    Items: scheduleItems,
                    ScheduleId: this.scheduleId,
                    Modified: this.modified
                });
                let requestName = "saveSchedule";
                console.log(requestName);
                $.ajax({
                    url: `${URLS.api.scheduleBuilder.saveSchedule}`,
                    context: this,
                    method: "POST",
                    contentType: 'application/json',
                    data: data,
                    datatype: 'json',
                    success: function (data) {
                        console.log(requestName + "- Successfully saved schedule");
                        console.log(data);
                        this.unsavedChanges = false;
                    },
                    error: function (error) {
                        console.log(requestName + "- Error saving schedule");
                        console.log(error);
                    }
                });
            },
            onBack() {
                let userWantsToGoBack = true;
                if (this.unsavedChanges) {
                    userWantsToGoBack = confirm("You have unsaved changes. Are you sure you want to go back?");
                }
                if (userWantsToGoBack) {
                    console.log("Back to schedule selection");
                    router.push({
                        name: 'SelectForBuilder',
                        params: {
                            user: this.user,
                        }
                    })
                }
            }
        },
    }
</script>

<style>
</style>
