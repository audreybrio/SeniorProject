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
                    <Days v-for="day in days" :key="day" :items="items" :index="this.days.indexOf(day)" @item-updated="updateItem"/>
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
        },
        components: {
            Days,
        },
        data() {
            return {
                days: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
            };
        },
        computed: {
            indexOfDay(day) {
                return this.days.indexOf(day)
            },
            getItemsForDay(index) {
                return this.items.filter(item => (item.days[index]));
            },
        },
        created() {
            this.days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
        },
        methods: {
            updateItem(updatedItem) {
                console.log("Schedules.updateItem()");
                this.$emit("item-updated", updatedItem);
            }
                // getItemsForDay(day){
                //     results = this.items.filter(item => item.days[day] === true)
                //     return results
                // }
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