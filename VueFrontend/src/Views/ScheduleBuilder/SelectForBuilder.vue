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
                    <button id="newScheduleSubmit" @click="PostSchedule">Create</button>
                </p>
            </form>
        </div>

        <div v-if="list" class="content">
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
                        <td><button @click="onScheduleBuilder(schedule.id, schedule.title, schedule.created, schedule.modified)">Edit</button></td>
                        <td><button @click="deleteSchedule(schedule.id)">Delete</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <router-view />
</template>

<script lang="js">
    import * as $ from 'jquery'
    import router from '../../router'
    import URLS from '../../variables'
    const baseURL = "https://localhost:5002";
    export default ({
        data() {
            return {
                loading: false,
                list: null,
                newScheduleTitle: "",

                // get user id or other identifier from the router to plug into getList()
                user: this.$route.params.user
            };
        },
        created() {
            console.log("this.user: " + this.user);
            this.getList();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'getList'
        },
        methods: {
            getList() {
                this.loading = true;
                let requestName = "schedule/getList";
                console.log(requestName);
                $.ajax({
                    // set the HTTP request URL
                    //url: `${baseURL}/schedule/getlist/${this.user}`, // won't work; gives 404 in dev
                    //url: `${baseURL}/api/schedule/getlist/${this.user}`, // works in dev
                    // should work in prod, gives CORS in dev w/o adding cors middleware to program.cs
                    url: `${URLS.api.scheduleBuilder.getList}/${this.user}`, 

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
                        console.log("this.list:");
                        console.log(this.list);
                        console.log("this.loading:");
                        console.log(this.loading);
                        // log that we've completed
                        console.log(requestName + "- Success");
                        return true;
                    },

                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(requestName + "- Error");
                        console.log(error);
                        this.list = null;
                        this.loading = false;
                        return false;
                    }
                });
            },
            PostSchedule() {
                this.loading = true;
                $.ajax({
                    //url: `${baseURL}/api/schedule/newschedule/${this.newScheduleTitle}/${this.user}`,
                    url: `${URLS.api.scheduleBuilder.newSchedule}/${this.user}/${this.newScheduleTitle}`,
                    context: this,
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
            onScheduleBuilder(scheduleId, title, created, modified) {
                console.log("To schedule builder @" + scheduleId + "/" + title);
                router.push({
                    name: 'ScheduleBuilder',
                    params: {
                        user: this.user,
                        scheduleId: scheduleId,
                        title: title,
                        created: created,
                        modified: modified,
                    }
                });
            },
            deleteSchedule(scheduleId) {
                //let scheduleId = schedule.id;
                let userConfirmed = confirm("Are you sure you want to delete this schedule?");
                if (userConfirmed) {
                    $.ajax({
                        url: `${baseURL}/api/schedule/deleteSchedule/${this.user}/${scheduleId}`,
                        context: this,
                        method: "DELETE",
                        success: function (data) {
                            console.log(data);
                            this.getList();
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                }
            },
        }
    });
</script>

<style>
    table {
        margin: auto;
    }
    td {
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }
</style>