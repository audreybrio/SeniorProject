<template>
    <h2>
        Schedule Builder
    </h2>
    <div>
        <CreateItemForms
                   @item-added="addItem"
                   :nextId="nextId"
                   :submitText="createButtonText"
                   >
        </CreateItemForms>
    </div>
    <br />
    <div>
        <Schedules
                   :items="items"
                   :editableItems="true"
                   @item-updated="updateItem"
                   @item-deleted="deleteItem" />
    </div>
</template>

<script>
    import * as $ from 'jquery'
    import Schedules from './Schedules'
    import CreateItemForms from './CreateItemForms'
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
                createButtonText: "Create"
            }
        },
        computed: {
            nextId() {
                return this.items.length + 1;
            },
            ajaxContext() {
                return this;
            }
        },
        created() {
            this.user = this.$route.params.user;
            this.scheduleId = this.$route.params.scheduleId;
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
                let requestName = "LoadSchedule";
                console.log(requestName);
                $.ajax({
                    // set the HTTP request URL
                    // url: `${baseURL}/schedule/getschedule/${this.user}/${this.scheduleId}`,
                    url: `${URLS.api.scheduleBuilder.getSchedule}/${this.user}/${this.scheduleId}`,

                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,

                    // HTTP method
                    method: 'GET',

                    // On a successful AJAX request:
                    success: function (data) {
                        this.items = data;
                        this.loading = false;
                        // TODO: delete console.logs
                        console.log("this.list:")
                        console.log(this.list)
                        console.log("this.loading:")
                        console.log(this.loading)
                        // log that we've completed
                        console.log("ajax Success")
                        return true;
                    },

                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(error);
                        this.items = null;
                        this.loading = false;
                        return false;
                    }
                });
            },
            // Creates a schedule item and adds it to this.items.
            addItem(newItem) {
                console.log("Adding item");
                this.items.push(newItem);
                console.log("Item added");
                // let sendable = $(newItem).serialize();
                newItem.creator = String(this.user);
                newItem.scheduleId = Number(this.scheduleId);
                let sendable = JSON.stringify(newItem);
                // make ajax request
                $.ajax({
                    // url: this.baseUrl + 'schedule/createItem/' + this.schedule + "/" + newItem.id,
                    // url: `${baseURL}/schedule/createItem/${this.user}/${this.scheduleId}`,
                    url: `${URLS.api.scheduleBuilder.createItem}/${this.user}/${this.scheduleId}`,
                    // url: `${baseURL}/api/schedule/CreateItem/`,
                    context: this.ajaxContext,
                    contentType: 'application/json',
                    method: 'POST',
                    data: sendable,
                    dataType: 'json',
                    success: function (data) {
                        console.log(data);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
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
                // Make ajax request to update the item
                $.ajax({
                    // url: this.baseUrl + 'schedule/updateItem/' + this.schedule + "/" + updatedItem.id,
                    // url: `${baseURL}/schedule/updateItem/${this.user}/${this.scheduleId}`,
                    url: `${URLS.api.scheduleBuilder.updateItem}/${this.user}/${this.scheduleId}`,
                    context: this,
                    method: 'POST',
                    data: updatedItem,
                    success: function (data) {
                        console.log(data);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
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
                // Make AJAX request to delete the item.
                $.ajax({
                    // url: this.baseUrl + 'schedule/deleteItem/' + this.schedule + "/" + deleteableItem.id,
                    // url: `${baseURL}/api/schedule/deleteItem/${this.user}/${this.scheduleId}/${deleteableItem.id}`,
                    url: `${URLS.api.scheduleBuilder.deleteItem}/${this.user}/${this.scheduleId}/${deleteableItem.id}`,
                    context: this,
                    method: 'DELETE',
                    data: deleteableItem,
                    success: function (data) {
                        console.log(data);
                        console.log("Successfully deleted everywhere");
                    },
                    error: function (error) {
                        console.log(error);
                        console.log("Could not delete everywhere");
                    }
                });
            }
        },
    }
</script>

<style>
</style>
