<template>
    <div v-if="editing && editable" id="editdiv">
        <UpdateItemForms
                   @item-updated="updateItem"
                   @item-deleted="deleteItem"
                   @cancel="cancel"
                   :nextId="item.id"
                   :item="item"
                   :editing="editing"
                   :submitText="updateButtonText"
                   >
        </UpdateItemForms>
    </div>
    <div v-else id="noeditdiv">
        <table>
            <thead>
                <tr v-if="editable">
                    <td>
                        <button @click="edit()" type="button">Edit</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <Times
                               :startHour="item.startHour"
                               :startMinute="item.startMinute"
                               :endHour="item.endHour"
                               :endMinute="item.endMinute"
                         />
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
    import UpdateItemForms from "./UpdateItemForms"
    import Times from "./Times"
    export default {
        name: "ScheduleItems",
        props: {
            item: Object,
            Time: Object,
            editable: Boolean
        },
        components: {
            UpdateItemForms,
            Times,
        },
        created(){
            this.editing = false;
            this.tempItem = {};
        },
        data(){
            return {
                editing: this.editing,
                tempItem: this.tempItem,
                updateButtonText: "Save"
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
                    days: this.item.daysOfWeek,
                    startHour: this.item.startHour,
                    startMinute: this.item.startMinute,
                    endHour: this.item.endHour,
                    endMinute: this.item.endMinute
                };
            },
            cancelEdit(){
                this.editing = false;
            },
            updateItem(updatedItem) {
                // emit to parent component
                this.$emit("item-updated", updatedItem);
            },
            cancel() {
                this.editing = false;
            },
            deleteItem(deleteableItem) {
                this.$emit("item-deleted", deleteableItem);
            }
        }
    }
</script>