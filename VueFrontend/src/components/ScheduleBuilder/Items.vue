<template>
    <div v-if="editing" id="editdiv">
        <table>
            <thead>
                <tr>
                    <td>
                        <button @click="saveEdit()" type="button">Save</button>
                        <button @click="cancelEdit()" type="reset">Cancel</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- TODO: form for start time -->
                        <input v-model="tempItem.startHour">
                        <input v-model="tempItem.startMinute">
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- TODO: form for end time -->
                        <input v-model="tempItem.endHour">
                        <input v-model="tempItem.endMinute">
                    </td>
                </tr>
                <!-- TODO: form for days -->
                <tr>
                    <td v-for="(day, index) in item.days" :key="day">
                        <input v-model="tempItem.days[index]" type="checkbox" v-if="day" checked>
                        <input v-model="tempItem.days[index]" type="checkbox" v-else>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <!-- TODO: form for title -->
                        <label for="titleEditField">Title</label>
                        <input v-model="tempItem.title" id="titleEditField">
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- TODO: form for contact -->
                        <label for="contactEditField">Contact</label>
                        <input v-model="tempItem.contact" id="contactEditField">
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- TODO: form for location -->
                        <label for="locationEditField">Location</label>
                        <input v-model="tempItem.location" id="locationEditField">
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- TODO: form for notes -->
                        <label for="notesEditField">Notes</label>
                        <textarea v-model="tempItem.notes" id="notesEditField" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else id="noeditdiv">
        <table>
            <thead>
                <tr>
                    <td>
                        <button @click="edit()" type="button">Edit</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <Time :startHour="item.startHour" :startMinute="item.startMinute" :endHour="item.endHour" :endMinute="item.endMinute" />
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>{{ item.title }}</td>
                </tr>
                <tr>
                    <td>{{ item.contact }}</td>
                </tr>
                <tr>
                    <td>{{ item.location }}</td>
                </tr>
                <tr>
                    <td>{{ item.notes }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
//import Times from "./Times"
export default {
    name: "ScheduleItems",
    props: {
        Time: Object,
    },
    components:{
        //Times,
    },
    created(){
    },
    data(){
        return {
            editing: false,
            tempItem: {}
        };
    },
    methods: {
        edit(){
            this.editing = true;

            // Set up tempItem to hold values while editing
            this.tempItem = {
                id: this.item.id,
                title: this.item.title,
                contact: this.item.contact,
                location: this.item.location,
                notes: this.item.notes,
                days: this.item.days,
                startHour: this.item.startHour,
                startMinute: this.item.startMinute,
                endHour: this.item.endHour,
                endMinute: this.item.endMinute
            };
            this.item.editing = true;
        },
        saveEdit(){
            // Update the vaules
            this.item = {
                id: this.item.id,
                title: this.tempItem.title,
                contact: this.tempItem.contact,
                location: this.tempItem.location,
                notes: this.tempItem.notes,
                days: this.tempItem.days,
                startHour: this.tempItem.startHour,
                startMinute: this.tempItem.startMinute,
                endHour: this.tempItem.endHour,
                endMinute: this.tempItem.endMinute
            };
            this.item.editing = false;
            this.editing = false;
        },
        cancelEdit(){
            this.item.editing = false;
            this.editing = false;
            // Don't update this.item's values
        }
    }
}
</script>