<template>
    <div>
        <table>
            <thead>
                <tr>
                    <td v-for="day in days" :key="day" >
                        <h3>{{ day }}</h3>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <Days
                          v-for="day in days"
                          :key="day"
                          :items="items"
                          :index="this.days.indexOf(day)"
                          :editableItems="editableItems"
                          @item-updated="updateItem"
                          @item-deleted="deleteItem"
                     />
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import Days from "./Days"
    export default {
        name: "ScheduleBuilderSchedules",
        props: {
            items: Array,
            editableItems: Boolean
        },
        components: {
            Days,
        },
        data() {
            return {
                days: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
            };
        },
        created() {
            this.days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
        },
        methods: {
            updateItem(updatedItem) {
                console.log("Schedules.updateItem()");
                this.$emit("item-updated", updatedItem);
            },
            deleteItem(deleteableItem) {
                console.log("Schedules.deleteItem()");
                this.$emit("item-deleted", deleteableItem);
            }
        }
    }
</script>

<style scoped>
    table {
        margin: auto;
    }
    td {
        width: 11.15pc;
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }
</style>