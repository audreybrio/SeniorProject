<template>
    <div>
        <form id="itemForm">
            <table>
                <tbody>
                    <tr>
                        <td>
                            <!-- Title -->
                            <label for="titleField">Title</label>
                            <input v-bind="title" id="titleField" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Contact -->
                            <label for="contactField">Contact</label>
                            <input v-bind="contact" id="contactField" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Location -->
                            <label for="locationField">Location</label>
                            <input v-bind="location" id="locationField" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Notes -->
                            <label for="notesField">Notes</label>
                            <input v-bind="notes" id="notesField" />
                        </td>
                    </tr>
                    <!-- Days -->
                    <tr>
                        <label for="sundayField">Sunday</label>
                        <input v-bind="days.sunday" id="sundayField" type="checkbox" />
                    </tr>
                    <tr>
                        <label for="mondayField">Monday</label>
                        <input v-bind="days.monday" id="mondayField" type="checkbox" />
                    </tr>
                    <tr>
                        <label for="tuesdayField">Tuesday</label>
                        <input v-bind="days.tuesday" id="tuesdayField" type="checkbox" />
                    </tr>
                    <tr>
                        <label for="wednesdayField">Wednesday</label>
                        <input v-bind="days.wednesday" id="wednesdayField" type="checkbox" />
                    </tr>
                    <tr>
                        <label for="thursdayField">Thursday</label>
                        <input v-bind="days.thursday" id="thursdayField" type="checkbox" />
                    </tr>
                    <tr>
                        <label for="fridayField">Friday</label>
                        <input v-bind="days.friday" id="fridayField" type="checkbox" />
                    </tr>
                    <tr>
                        <label for="saturdayField">Saturday</label>
                        <input v-bind="days.saturday" id="saturdayField" type="checkbox" />
                    </tr>
                    <tr>
                        <td>
                            <!-- Start Time -->
                            <label for="Field">Start</label>
                            <input v-bind="startInput" id="Field" type="time" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- End Time -->
                            <label for="Field"></label>
                            <input v-bind="endInput" id="Field" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- create button -->
                            <button @click="create()">Create</button>
                        </td>
                        <td>
                            <!-- clear button -->
                            <button @click="clear()">Clear</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
    </div>
</template>

<script>
import Time from "./Time"
export default {
    props: {
        Time: Object,
    },
    components:{
        Time,
    },
    created(){
    },
    data(){
        return {
            itemId: -1,
            title: "",
            contact: "",
            location: "",
            notes: "",
            days: {
                sunday: false,
                monday: false,
                tuesday: false,
                wednesday: false,
                thursday: false,
                friday: false,
                saturday: false
            },
            start: {
                hour: 0,
                minute: 0
            },
            end: {
                hour: 0,
                minute: 0
            },
        };
    },
    methods: {
        create() {
            // TODO: Grab title from form
            // TODO: Grab contact from form
            // TODO: Grab location from form
            // TODO: Grab notes from form
            // TODO: Grab days from form
            // TODO: Translate days into JSON array
            // TODO: Grab start time from form
            // TODO: Translate start time into JSON object
            // TODO: Grab end time from form
            // TODO: Translate end time into JSON object
            // Clear the form for the next use
            this.clear();
        },
        clear() {
            this.itemId = -1;
            this.title = "";
            this.contact = "";
            this.location = "";
            this.notes = "";
            this.days = {
                sunday: false,
                monday: false,
                tuesday: false,
                wednesday: false,
                thursday: false,
                friday: false,
                saturday: false
            };
            this.start = {
                hour: 0,
                minute: 0
            };
            this.end = {
                hour: 0,
                minute: 0
            };
        },
    }
}
</script>