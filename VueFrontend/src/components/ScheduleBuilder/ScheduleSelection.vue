<!--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>-->
<template>
    <div class="list">
        <div v-if="loading" class="loading">
            Loading...
        </div>

        <div class="newSchedule">
            <form id="newScheduleForm">
                <p>
                    Create a new Schedule
                    <input v-model="newScheduleTitle" placeholder="Title" />
                    <button id="newScheduleSubmit" @click="PostSchedule()">Create</button>
                </p>
            </form>
        </div>

        <div v-if="list" class="content">
            <center>
                <table>
                    <thead>
                        <tr>
                            <th>id</th>
                            <th>title</th>
                            <th>created</th>
                            <th>modified</th>
                            <th>Owner</th>
                            <th>Edit</th>
                            <th>Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="schedule in list" :key="schedule.id">
                            <!--TODO: implement Schedule component-->
                            <!--<Schedule :schedule="schedule"/>-->
                            <td>{{ schedule.id }}</td>
                            <td>{{ schedule.title }}</td>
                            <td>{{ schedule.created }}</td>
                            <td>{{ schedule.modified }}</td>
                            <td>{{ schedule.owner }}</td>
                            <td><button>Edit</button></td>
                            <td><button>Delete</button></td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </div>
    </div>
</template>

<script lang="js">
    import * as $ from 'jquery'
    export default ({
        data() {
            return {
                loading: false,
                list: null,
                newScheduleTitle: ""
            };
        },
        props: {
            //newScheduleTitle: String,
        },
        created() {
            // TODO: get user id or other identifier from cookie to plug into getList()

            // fetch the data when the view is created and the data is
            // already being observed
            this.getList();
            console.log("created")
        },
        watch: {
            // call again the method if the route changes
            '$route': 'getList'
        },
        methods: {
            getList() {
                this.loading = true;

                console.log("ajax time");
                $.ajax({
                    // set the HTTP request URL
                    url: 'https://localhost:5001/' + 'schedule/getlist/aloafofbrad',

                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,

                    // HTTP method
                    method: 'GET',

                    // On a successful AJAX request:
                    success: function (data) {
                        this.list = data;
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
                        this.list = null;
                        this.loading = false;
                        return false;
                    }
                });
            },
            PostSchedule() {
                this.loading = true;
                let username = "aloafofbrad";
                $.ajax({
                    url: 'https://localhost:5001/' + "schedule/newschedule/" + this.newScheduleTitle + "/" + username,
                    context: this,
                    //contentType: "application/json; charset=utf-8",
                    //data: {
                    //    title: this.newScheduleTitle,
                    //    username: username
                    //},
                    //dataType: "json",
                    processData: true,
                    method: 'POST',
                    success: function (data) {
                        console.log(data);
                        this.loading = false;
                        return;
                    },
                    error: function (error) {
                        console.log(error);
                        this.loading = false;
                        return;
                    }
                });
            },
        }
    });
</script>