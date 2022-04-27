<template>
    <div>
<<<<<<< HEAD
        <center>
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
                        <Days v-for="day in days" :key="day" :items="items" :index="this.days.indexOf(day)"/>
                    </tr>
                </tbody>
            </table>
        </center>
=======
        <table>
            <thead>
                <tr>
                    <td v-for="day in days" :key="day" >
                        <h3>{{ day }}</h3>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr v-if="items">
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
                <tr v-else>
                    <td v-for="day in days" :key="day">
                        Use the form above to add items to your schedule!
                    </td>
                </tr>
            </tbody>
        </table>
>>>>>>> main
    </div>
</template>

<script>
import Days from "./Days"
export default {
    name: "ScheduleBuilderSchedules",
    props: {
        items: Array,
    },
    components:{
        Days,
    },
    data() {
        return {
            days: [],
        }
    },
    computed: {
        indexOfDay(day) {
            return this.days.indexOf(day)
        }
    },
    created() {
        this.days =  ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
    },
    methods:{
        // getItemsForDay(day){
        //     results = this.items.filter(item => item.days[day] === true)
        //     return results
        // }
    },
}
</script>

<style scoped>
    td {
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }
</style>