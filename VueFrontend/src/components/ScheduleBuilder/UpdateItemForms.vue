<template>
    <form @submit.prevent="onSubmit" @reset.prevent="onCancel">
        <div>
            <!-- Title -->
            <label for="titleField">Title</label>
            <input v-model="this.title" placeholder="item.title" id="titleField" type="text" maxlength="64" required />

            <!-- Contact -->
            <label for="contactField">Contact</label>
            <input v-model="this.contact" placeholder="item.contact" id="contactField" type="text" maxlength="64" @input="$emit('update:contact', $event.target.value)" />

            <!-- Location -->
            <label for="locationField">Location</label>
            <input v-model="this.location" placeholder="item.location" id="locationField" type="text" maxlength="64" @input="$emit('update:location', $event.target.value)" />

            <!-- Notes -->
            <label for="notesField">Notes</label>
            <input v-model="this.notes" placeholder="item.notes" id="notesField" type="text" maxlength="64" @input="$emit('update:notes', $event.target.value)" />
        </div>

        <!-- Days -->
        <div>
            <label for="sundayField">Sunday</label>
            <input v-model="this.days[0]" :value="item.days[0]" id="sundayField" type="checkbox" />
            <label for="mondayField">Monday</label>
            <input v-model="this.days[1]" :value="item.days[1]" id="mondayField" type="checkbox" />
            <label for="tuesdayField">Tuesday</label>
            <input v-model="this.days[2]" :value="item.days[2]" id="tuesdayField" type="checkbox" />
            <label for="wednesdayField">Wednesday</label>
            <input v-model="this.days[3]" :value="item.days[3]" id="wednesdayField" type="checkbox" />
            <label for="thursdayField">Thursday</label>
            <input v-model="this.days[4]" :value="item.days[4]" id="thursdayField" type="checkbox" />
            <label for="fridayField">Friday</label>
            <input v-model="this.days[5]" :value="item.days[5]" id="fridayField" type="checkbox" />
            <label for="saturdayField">Saturday</label>
            <input v-model="this.days[6]" :value="item.days[6]" id="saturdayField" type="checkbox" />
            <!--<label for="sundayField">Sunday</label>
    <input :value="item.days[0]" id="sundayField" type="checkbox" @input="$emit('update:days[0]', $event.target.value)" />
    <label for="mondayField">Monday</label>
    <input :value="item.days[1]" id="mondayField" type="checkbox" @input="$emit('update:days[1]', $event.target.value)" />
    <label for="tuesdayField">Tuesday</label>
    <input :value="item.days[2]" id="tuesdayField" type="checkbox" @input="$emit('update:days[2]', $event.target.value)" />
    <label for="wednesdayField">Wednesday</label>
    <input :value="item.days[3]" id="wednesdayField" type="checkbox" @input="$emit('update:days[3]', $event.target.value)" />
    <label for="thursdayField">Thursday</label>
    <input :value="item.days[4]" id="thursdayField" type="checkbox" @input="$emit('update:days[4]', $event.target.value)" />
    <label for="fridayField">Friday</label>
    <input :value="item.days[5]" id="fridayField" type="checkbox" @input="$emit('update:days[5]', $event.target.value)" />
    <label for="saturdayField">Saturday</label>
    <input :value="item.days[6]" id="saturdayField" type="checkbox" @input="$emit('update:days[6]', $event.target.value)" />-->
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
            <!-- delete button -->
            <button type="button" @click="onDelete">Delete</button>
            <!-- create button -->
            <button type="submit">{{ submitText }}</button>
            <!-- clear button -->
            <button type="reset">Cancel</button>
        </div>
    </form>
</template>

<script>
    export default {
        props: {
            nextId: Number,
            submitText: String,
            item: Object,
            editing: Boolean
        },
        emits: [
            'item-updated',
            'item-deleted',
            'cancel'
        ],
        data() {
            return {
                title: this.item.title,
                contact: this.item.contact,
                location: this.item.location,
                notes: this.item.notes,
                days: this.item.days,
                startInput: this.constructTime(this.item.startHour, this.item.startMinute),
                endInput: this.constructTime(this.item.endHour, this.item.endMinute),
                creatorId: -1,
            };
        },
        methods: {
            constructTime(hour, minute) {
                let result = "";
                if (hour < 10) {
                    result += "0";
                }
                result += hour;
                result += ":";
                if (minute < 10) {
                    result += "0"
                }
                result += minute;
                return result;
            },
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
                this.$emit('cancel');
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
                this.$emit("item-updated", newItem);
            },
            onDelete() {
                let userChoice = confirm("Are you sure you want to delete?\nThis is irreversible.");
                if (userChoice) {
                    console.log("Executing delete sequence for ");
                    console.log(this.item);
                    this.$emit("item-deleted", this.item);
                }
            },
            onCancel() {
                console.log("Cancelling");
                this.$emit("cancel");
            },
        }
    }
</script>