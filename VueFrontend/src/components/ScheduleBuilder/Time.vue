<template>
    <div>
        <td>{{ formatted() }}</td>
    </div>
</template>

<script>
export default {
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
            let sm = null
            let em = null
            this.startMinute === 0 ? sm = "00" : sm = this.startMinute.toString()
            this.endMinute === 0 ? em = "00" : em = this.endMinute.toString()
            return `${this.startHour.toString()}:${sm} - ${this.endHour.toString()}:${em}`
        }
    },
    computed: {
    }
}
</script>