<template>
    <div>
        <div>
            <!-- Title -->
            <label for="titleField">Title</label>
            <input v-model="title" id="titleField" required />

            <!-- Contact -->
            <label for="contactField">Contact</label>
            <input v-model="contact" id="contactField" />

            <!-- Location -->
            <label for="locationField">Location</label>
            <input v-model="location" id="locationField" />

            <!-- Notes -->
            <label for="notesField">Notes</label>
            <input v-model="notes" id="notesField" />
        </div>

        <!-- Days -->
        <div>
            <label for="sundayField">Sunday</label>
            <input v-model="days.sunday" id="sundayField" type="checkbox" />
            <label for="mondayField">Monday</label>
            <input v-model="days.monday" id="mondayField" type="checkbox" />
            <label for="tuesdayField">Tuesday</label>
            <input v-model="days.tuesday" id="tuesdayField" type="checkbox" />
            <label for="wednesdayField">Wednesday</label>
            <input v-model="days.wednesday" id="wednesdayField" type="checkbox" />
            <label for="thursdayField">Thursday</label>
            <input v-model="days.thursday" id="thursdayField" type="checkbox" />
            <label for="fridayField">Friday</label>
            <input v-model="days.friday" id="fridayField" type="checkbox" />
            <label for="saturdayField">Saturday</label>
            <input v-model="days.saturday" id="saturdayField" type="checkbox" />
        </div>

        <div>
            <!-- Start Time -->
            <label for="Field">Start Time</label>
            <input v-model="startInput" id="Field" type="time" min="00:00" max="23:59" required />

            <!-- End Time -->
            <label for="Field">End Time</label>
            <input v-model="endInput" id="Field" type="time" min="00:00" max="23:59" required />
        </div>

        <div>
            <!-- create button -->
            <button @click="createItem">Create</button>
            <!-- clear button -->
            <button @click="clear">Clear</button>
        </div>
    </div>
</template>

<script>
import Time from "./Time"
import * as $ from 'jquery'
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
            startInput: {
                hour: 0,
                minute: 0
            },
            endInput: {
                hour: 0,
                minute: 0
            },
            creatorId: -1
        };
    },
    methods: {
        createItem() {
            console.log("Form submitted")
            let newTitle = this.title;
            let newContact = this.contact;
            let newLocation = this.location;
            let newNotes = this.notes;
            let newDays = [
                this.days.sunday,
                this.days.monday,
                this.days.tuesday,
                this.days.wednesday,
                this.days.thursday,
                this.days.friday,
                this.days.saturday
            ];
            // TODO: translate times into separate ints
            let newStart = this.startInput;
            let newEnd = this.endInput;
            console.log(newTitle);
            console.log(newContact);
            console.log(newLocation);
            console.log(newNotes);
            console.log(newDays);
            console.log(newStart);
            console.log(newEnd);
            // TODO: make request to submit
            $.ajax({
                url: '',
                context: this,
                method: 'POST',
                data: {
                    title: newTitle,
                    contact: newContact,
                    location: newLocation,
                    notes: newNotes,
                    days: newDays,
                    startTime: newStart,
                    endTime: newEnd,
                    creator: this.creatorId
                },
                success: function (data) {
                    console.log(data);
                },
                error: function (error) {
                    console.log(error);
                }
            });
            // Clear the form for the next use
            this.clear();
        },
        clear() {
            console.log("Clear");
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
            this.startInput = {
                hour: 0,
                minute: 0
            };
            this.endInput = {
                hour: 0,
                minute: 0
            };
        },
    }
}
</script>