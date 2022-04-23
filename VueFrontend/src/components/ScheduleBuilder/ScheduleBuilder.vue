<template>
    

    <div class="container">
        <Schedules :items="items" />
    </div>
</template>

<script>
    import * as $ from 'jquery'
    import Schedules from './Schedules'
    //import ItemForms from './ItemForms'
    export default {
        name: 'ScheduleBuilder',
        components: {
            Schedules,
            //ItemForms,
        },
        data() {
            return {
                loading: false,
                items: [],
                demo: true
            }
        },
        created() {
            if (!this.demo) {
                this.loadSchedule();
            }
            else {
                console.log("Demo");
                this.items = [
                    {
                        id: 1,
                        title: "{title}",
                        contact: "{Contact}",
                        location: "{location}",
                        notes: "{notes}",
                        days: [false, false, false, false, false, true, false],
                        startHour: 17,
                        startMinute: 30,
                        endHour: 18,
                        endMinute: 45,
                        editing: false
                    },
                    {
                        id: 2,
                        title: "CECS 277",
                        contact: "Cleary",
                        location: "ECS 407",
                        notes: "rocks",
                        days: [false, false, true, false, true, false, false],
                        startHour: 19,
                        startMinute: 0,
                        endHour: 21,
                        endMinute: 45,
                        editing: false
                    },
                    {
                        id: 3,
                        title: "CECS 228",
                        contact: "Nguyen",
                        location: "ECS 308",
                        notes: "is rough",
                        days: [false, true, false, true, false, false, false],
                        startHour: 9,
                        startMinute: 0,
                        endHour: 11,
                        endMinute: 15,
                        editing: false
                    },
                    {
                        id: 4,
                        title: "CECS 282",
                        contact: "Nguyen",
                        location: "ECS 308",
                        notes: "is ok",
                        days: [false, true, false, true, false, false, false],
                        startHour: 13,
                        startMinute: 0,
                        endHour: 15,
                        endMinute: 15,
                        editing: false
                    },
                    {
                        id: 5,
                        title: "CWL 305",
                        contact: "DeSuza",
                        location: "LA 305",
                        notes: "is fun",
                        days: [false, true, false, true, false, false, false],
                        startHour: 17,
                        startMinute: 0,
                        endHour: 18,
                        endMinute: 15,
                        editing: false
                    },
                ]
                console.log(this.items);
            }
        },
        methods: {
            // Make an AJAX request to get items on page load
            loadSchedule() {
                this.loading = true;

                console.log("ajax time (SB)");
                $.ajax({
                    // set the HTTP request URL
                    url: 'schedule/getschedule/1',

                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,

                    // HTTP method
                    method: 'GET',

                    // On a successful AJAX request:
                    success: function (data) {
                        this.items = data;
                        this.loading = false;
                        // TODO: delete console.logs
                        console.log("this.list:")
                        console.log(this.list)
                        console.log("this.loading:")
                        console.log(this.loading)
                        // log that we've completed
                        console.log("ajax Success")
                        return true;
                    },

                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(error);
                        this.items = null;
                        this.loading = false;
                        return false;
                    }
                });
            },
            // TODO: create schedule item
            // TODO: update schedule item
            // TODO: delete schedule item
        },
    }
</script>

<style>
</style>
