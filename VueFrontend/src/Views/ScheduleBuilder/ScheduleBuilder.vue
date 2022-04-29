<template>
    <div class="list">
        <div v-if="loading" class="loading">
            Loading...
        </div>
        <h2>
            Schedule Builder
        </h2>
        <div>
            <h3 v-if="title != ''">{{ title }}</h3><h3 v-if="scheduleId != null"> ({{ scheduleId }})</h3>
        </div>
        <div>
            <button @click="save">Save Schedule</button>
            <button @click="onBack">Return to Selection</button>
        </div>
        <br />
        <div>
            <CreateItemForms @item-added="addItem" :nextId="nextId" :submitText="createButtonText" />
        </div>
        <br />
        <div>
            <Schedules :items="Items"
                       :editableItems="true"
                       @item-updated="updateItem"
                       @item-deleted="deleteItem" />
        </div>
    </div>
    <router-view />
</template>

<script>
    import axios from 'axios'
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
            Items() {
                return this.items;
            },
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
            this.loadSchedule();
            
            this.title = this.$route.params.title;
            this.created = this.$route.params.created;
            this.modified = this.$route.params.modified;
        },
        methods: {
            // Make an AJAX request to get items on page load
            loadSchedule() {
                this.loading = true;
                let requestName = "LoadSchedule";
                this.user = this.$route.params.user;
                this.scheduleId = this.$route.params.scheduleId;
                console.log(requestName);
                axios.get(`${URLS.api.scheduleBuilder.getSchedule}/${this.user}/${this.scheduleId}`, { timeout: 5000 })
                    .then(response => {
                        this.items = response.data
                        this.loading = false
                    })
                    .catch(e => {
                        console.log(e)
                    })
            },
            // Creates a schedule item and adds it to this.items.
            addItem(newItem) {
                newItem.creator = String(this.user);
                newItem.scheduleId = Number(this.scheduleId);
                this.items.push(newItem);
                this.unsavedChanges = true;
            },
            // Updates a schedule item
            updateItem(updatedItem) {
                let updated = false;
                for (let i = 0; i < this.items.length && !updated; i++) {
                    if (this.items[i].id === updatedItem.id) {
                        this.items[i] = updatedItem;
                        updated = true;
                    }
                }
                if (updated) {
                    this.unsavedChanges = true;
                }
            },
            // Deletes a schedule item based on ID.
            deleteItem(deleteableItem) {
                let deleted = false;
                for (let i = 0; i < this.items.length && !deleted; i++) {
                    if (this.items[i].id === deleteableItem.id) {
                        this.items = this.items.filter(item => (item.id !== this.items[i].id));
                        deleted = true;
                    }
                }
                if (!deleted) {
                    console.log("Could not delete");
                }
                else {
                    this.unsavedChanges = true;
                }
            },

            // Save the schedule to a file on the server
            save() {
                // Format the data so the controller will accept it
                let scheduleItems = [];
                for (let i = 0; i < this.items.length; i++) {
                    scheduleItems.push({
                        Id: this.items[i].id,
                        ScheduleId: this.scheduleId,
                        Creator: this.user,
                        Contact: this.items[i].contact,
                        DaysOfWeek: this.items[i].daysOfWeek,
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

                // Make the request
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
                        console.log(data)
                        this.unsavedChanges = false;
                    },
                    error: function (error) {
                        console.log(requestName + "- Error saving schedule");
                        console.log(error);
                    }
                });
            },

            // Go back to the selection page
            onBack() {
                let userWantsToGoBack = true;
                if (this.unsavedChanges) {
                    userWantsToGoBack = confirm("You have unsaved changes. Are you sure you want to go back?");
                }
                if (userWantsToGoBack) {
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
