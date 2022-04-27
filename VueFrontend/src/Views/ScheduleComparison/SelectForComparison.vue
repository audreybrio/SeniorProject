<template>
    <div class="list">
        <div>
            <h2>
                Schedule Comparison
            </h2>
        </div>
        <div v-if="loading" class="loading">
            Loading...
        </div>

        <div v-if="schedules.length > 0" class="content">
            <button v-if="selectionLengthIsInRange" @click="onScheduleComparison()">
                Compare!
            </button>
            <h4 v-else>Select 2 to 5 schedules for comparison.</h4>
            <table>
                <thead>
                    <tr>
                        <th>Compare</th>
                        <th>id</th>
                        <th>title</th>
                        <th>created</th>
                        <th>modified</th>
                        <th>Owner</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="schedule in schedules" :key="schedule.id">
                        <!--TODO: implement Schedule details component!-->
                        <td><input type="checkbox" @click="toggleSelection(schedule.id)" /></td>
                        <td>{{ schedule.id }}</td>
                        <td>{{ schedule.title }}</td>
                        <td>{{ schedule.created }}</td>
                        <td>{{ schedule.modified }}</td>
                        <td>{{ schedule.owner }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-else>
            <p>Create a schedule or two to get started :)</p>
            <button @click="onScheduleBuilder">Go to schedule builder</button>
        </div>
    </div>
    <router-view />
</template>

<script lang="js">
    import * as $ from 'jquery'
    import router from '../../router'
    import URLs from '../../variables'
    export default ({
        data() {
            return {
                loading: false,
                schedules: [],
                selection: [],
                newScheduleTitle: "",

                // get user id or other identifier from the router to plug into getList()
                user: null
            };
        },
        computed: {
            selectionLengthIsInRange() {
                return this.selection.length >= 2 && this.selection.length <= 5;
            }
        },
        created() {
            this.user = this.$route.params.user;
            this.selection = [];
            this.getList();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'getList'
        },
        methods: {
            // Toggle whether or not the schedule was checked. If it was, add it to the 
            toggleSelection(scheduleID) {
                let index = this.selection.find(id => id === scheduleID);
                if (index) {
                    this.selection = this.selection.filter(id => id !== scheduleID);
                }
                else {
                    this.selection.push(scheduleID);
                }
            },
            getList() {
                this.loading = true;
                let requestName = "ScheduleComparison- get list of schedules";
                console.log(requestName);
                $.ajax({
                    // set the HTTP request URL
                    //url: `${baseURL}/api/schedule/getlist/${this.user}`,
                    url: `${URLs.api.scheduleBuilder.getList}/${this.user}`,

                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,

                    // HTTP method
                    method: 'GET',

                    // On a successful AJAX request:
                    success: function (data) {
                        this.schedules = data;
                        this.loading = false;

                        // log that we've completed
                        console.log(requestName + "- Success")
                        return true;
                    },

                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(requestName + "- Error");
                        console.log(error);
                        this.schedules = [];
                        this.loading = false;
                        return false;
                    }
                });
            },
            // Routes the user to the schedule builder selection page
            onScheduleBuilder() {
                router.push({
                    name: 'ScheduleSelection',
                    params: { user: this.user }
                });
            },

            // Rotuse the user to the comparison
            onScheduleComparison() {
                if (!this.selectionLengthIsInRange) {
                    alert("Something went wrong.\nSelect 2 to 5 schedules for comparison.");
                }
                else {
                    router.push({
                        name: 'ScheduleComparison',
                        params: { user: this.user, selection: this.selection }
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