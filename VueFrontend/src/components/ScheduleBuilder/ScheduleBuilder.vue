<template>
    <div class="container">
        <ItemForm />
    </div>
    <div class="container">
        <Schedule :items="items" />
    </div>
</template>

<script>
    import * as $ from 'jquery'
    import Schedule from './Schedule'
    import ItemForm from './ItemForm'
    export default {
        name: 'ScheduleBuilder',
        components: {
            Schedule,
            ItemForm,
        },
        data() {
            return {
                loading: false,
                items: [],
            }
        },
        created() {
            this.loadSchedule();
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
    #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
    }
</style>
