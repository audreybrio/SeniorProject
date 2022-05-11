<template v-if="IsOnDay">
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
                <tr v-if="item.title !== ''">
                    <td>{{ item.title }}</td>
                </tr>
                <tr v-if="item.contact !== ''">
                    <td>{{ item.contact }}</td>
                </tr>
                <tr v-if="item.location !== ''">
                    <td>{{ item.location }}</td>
                </tr>
                <tr v-if="item.notes !== ''">
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
            editable: Boolean,
            index: Number
        },
        computed: {
            IsOnDay() {
                return this.item.daysOfWeek[this.index];
            }
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
                    daysOfWeek: this.item.daysOfWeek,
                    startHour: this.item.startHour,
                    startMinute: this.item.startMinute,
                    endHour: this.item.endHour,
                    endMinute: this.item.endMinute,
                    editing: null
                };
            },
            cancelEdit(){
                this.editing = false;
            },
            updateItem(updatedItem) {
                this.editing = false;
                // emit to parent component
                this.$emit("item-updated", updatedItem);
            },
            cancel() {
                this.editing = false;
            },
            deleteItem(deleteableItem) {
                this.editing = false;
                this.$emit("item-deleted", deleteableItem);
            }
        }
    }
</script>