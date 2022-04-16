<template>
    <form @submit.prevent="onSubmit" @reset.prevent="onReset">
        <div>
            <!-- Title -->
            <label for="titleField">Title</label>
            <input v-model="title" id="titleField" type="text" maxlength="64" required>

            <!-- Contact -->
            <label for="contactField">Contact</label>
            <input v-model="contact" id="contactField" type="text" maxlength="64" />

            <!-- Location -->
            <label for="locationField">Location</label>
            <input v-model="location" id="locationField" type="text" maxlength="64" />

            <!-- Notes -->
            <label for="notesField">Notes</label>
            <input v-model="notes" id="notesField" type="text" maxlength="64" />
        </div>

        <!-- Days -->
        <div>
            <label for="sundayField">Sunday</label>
            <input v-model="days[0]" id="sundayField" type="checkbox" />
            <label for="mondayField">Monday</label>
            <input v-model="days[1]" id="mondayField" type="checkbox" />
            <label for="tuesdayField">Tuesday</label>
            <input v-model="days[2]" id="tuesdayField" type="checkbox" />
            <label for="wednesdayField">Wednesday</label>
            <input v-model="days[3]" id="wednesdayField" type="checkbox" />
            <label for="thursdayField">Thursday</label>
            <input v-model="days[4]" id="thursdayField" type="checkbox" />
            <label for="fridayField">Friday</label>
            <input v-model="days[5]" id="fridayField" type="checkbox" />
            <label for="saturdayField">Saturday</label>
            <input v-model="days[6]" id="saturdayField" type="checkbox" />
        </div>

        <div>
            <!-- Start Time -->
            <label for="startField">Start Time</label>
            <input v-model="startInput" id="startField" type="time" min="00:00" max="23:59" required />

            <!-- End Time -->
            <label for="endField">End Time</label>
            <input v-model="endInput" id="endField" type="time" min="00:00" max="23:59" required />
        </div>

        <div>
            <!-- create button -->
            <button type="submit">{{ submitText }}</button>
            <!-- clear button -->
            <button type="reset">Clear</button>
        </div>
    </form>
</template>

<script>
    import * as $ from 'jquery'
    const defaultStart = "00:00";
    const defaultEnd = "00:01";
        export default {
        props: {
            nextId: Number,
            submitText: String
        },
        data(){
            return {
                title: "",
                contact: "",
                location: "",
                notes: "",
                //days: {
                //    sunday: false,
                //    monday: false,
                //    tuesday: false,
                //    wednesday: false,
                //    thursday: false,
                //    friday: false,
                //    saturday: false
                //},
                days: [false, false, false, false, false, false, false],
                startInput: defaultStart,
                endInput: defaultEnd,
                creatorId: -1
            };
        },
        methods: {
            parseTime(time) {
                console.log("Parsing " + time);
                let splitTime = time.split(":");
                if (splitTime.length === 2) {
                    return {
                        hour: Number(splitTime[0]),
                        minute: Number(splitTime[1])
                    };
                }
                else if (splitTime.length === 1) {
                    return {
                        hour: Number(splitTime[0]),
                        minute: 0
                    };
                }
                else {
                    return {
                        hour: 0,
                        minute: 0
                    };
                }
            },
            validateTimes(startTime, endTime) {
                // Return false if the user entered something like:
                // start:  1:00 PM, end: 12:00 PM
                if (startTime.hour > endTime.hour) {
                    return false;
                }

                // Return false if the user entered something like:
                // start:  1:05 PM, end:  1:00 PM
                if (startTime.hour === endTime.hour &&
                    startTime.minute >= endTime.minute) {
                    return false;
                }

                return true;
            },
            validateDays() {
                // At least one day must be checked (true) for the
                // form to be valid.
                return (
                        this.days[0]
                     || this.days[1]
                     || this.days[2]
                     || this.days[3]
                     || this.days[4]
                     || this.days[5]
                     || this.days[6]
                );
            },
            onSubmit() {
                console.log("Form submitted");
                console.log("Creating new item");
                // Check that the user checked at least one day
                if (!this.validateDays()) {
                    alert("You must select at least one day.");
                    console.log(this.days);
                    return;
                }

                // Translate times into separate hour, minute values
                let newStart = this.parseTime(this.startInput);
                let newEnd = this.parseTime(this.endInput);

                // Check that the start time is earlier than the end time
                if (!this.validateTimes(newStart, newEnd)) {
                    alert("Item cannot start at the same time or later than it ends.");
                    return;
                }
                console.log("Times parsed");

                // Create the new item
                let newItem = {
                    id: this.nextId,
                    title: this.title,
                    contact: this.contact,
                    location: this.location,
                    notes: this.notes,
                    days: this.days,
                    startHour: newStart.hour,
                    startMinute: newStart.minute,
                    endHour: newEnd.hour,
                    endMinute: newEnd.minute,
                    creator: this.creatorId
                };

                // Emit the new item to the ScheduleBuilder so it can be shown to the user
                console.log(newItem);
                console.log("Emitting new item");
                this.$emit("item-added", newItem);
                // TODO: make request to submit
                $.ajax({
                    url: '',
                    context: this,
                    method: 'POST',
                    data: newItem,
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
            onReset() {
                console.log("Form resetting");
                this.clear();
            },
            clear() {
                console.log("Clear");
                this.title = "";
                this.contact = "";
                this.location = "";
                this.notes = "";
                this.days = [false, false, false, false, false, false, false];
                this.startInput = defaultStart;
                this.endInput = defaultEnd;
            },
        }
    }
</script>