<template>
    <div>
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
                        <Day v-for="day in days" :key="day" :items="items" :index="this.days.indexOf(day)"/>
                    </tr>
                </tbody>
            </table>
        </center>
    </div>
</template>

<script>
import Day from "./Day"
export default {
    name: "Schedule",
    props: {
        items: Array,
    },
    components:{
        Day,
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