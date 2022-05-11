<template>
    <div>
        <td>{{ formatted() }}</td>
    </div>
</template>

<script>
export default {
    name: "ScheduleItemTimes",
    props: {
        startHour: Number,
        startMinute: Number,
        endHour: Number,
        endMinute: Number,
    },
    components:{
    },
    methods: {
        formatted() {
            let prettyStart = null
            let prettyEnd = null
            this.startMinute < 10 ? prettyStart = "0" + this.startMinute.toString() : prettyStart = this.startMinute.toString()
            this.endMinute   < 10 ? prettyEnd   = "0" + this.endMinute.toString()   : prettyEnd   = this.endMinute.toString()
            return `${this.startHour.toString()}:${prettyStart} - ${this.endHour.toString()}:${prettyEnd}`
        }
    },
    computed: {
    }
}
</script>