<template>
    <div class="list">
        <div v-if="loading" class="loading">
            Loading...
        </div>

        <div class="newSchedule">
            <form id="newCollaboratorForm">
                <p>Add a new Collaborator</p>
                <input v-model="newCollaborator" placeholder="Username" />
                <p v-if="search()">{{ this.newCollaborator }} exists</p>
                <p v-else>{{ this.newCollaborator }} does not exist</p>
                <button id="newScheduleSubmit" @click="postCollaborator">Add</button>
                <!--<button id="search" @click="search()">Search</button>-->
            </form>
        </div>

        <div v-if="list" class="content">
            <table>
                <thead>
                    <tr>
                        <th>id</th>
                        <th>Username</th>
                        <th>Can Write</th>
                        <th>Is Owner</th>
                        <th>Update</th>
                        <th>Remove</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="collaborator in list" :key="collaborator.id">
                        <td>{{ collaborator.id }}</td>
                        <td>{{ collaborator.username }}</td>
                        <td><input type="checkbox" v-model="collaborator.canWrite" /></td>
                        <td><input type="checkbox" v-model="collaborator.isOwner" /></td>
                        <td><button @click="onUpdate(collaborator)">Edit</button></td>
                        <td><button @click="onRemove(collaborator.id)">Delete</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <router-view />
</template>

<script lang="js">
    import * as $ from 'jquery'
    //import router from '../../router'
    import URLS from '../../variables'
    import axios from 'axios'
    export default ({
        name: "ScheduleCollaborators",
        data() {
            return {
                loading: false,
                list: null,
                newCollaborator: "",
                scheduleId: this.$route.params.scheduleId,
                // get user id or other identifier from the router to plug into getList()
                user: this.$route.params.user
            };
        },
        created() {
            this.scheduleId = this.$route.params.scheduleId
            this.user = this.$route.params.user
            this.getList();
        },
        watch: {
        },
        methods: {
            // Gets the list of collaborators for the given schedule
            getList() {
                this.loading = true;
                let requestName = "schedule/getCollaborators";
                console.log(requestName);
                $.ajax({
                    url: `${URLS.api.scheduleBuilder.getCollaborators}/${this.scheduleId}`,
                    context: this,
                    method: 'GET',
                    success: function (data) {
                        this.list = data;
                        this.loading = false;
                        return true;
                    },
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
            validateCollaborator() {
                return (this.newCollaborator !== "")
            },
            search() {
                let exists = false
                if (this.validateCollaborator()) {
                    axios.get(`${URLS.api.scheduleBuilder.searchUser}/${this.newCollaborator}`)
                        .then(response => {
                            alert(response)
                            return true
                        })
                        .catch(e => {
                            alert(e)
                            return false
                        })
                }
                return exists
            },
            postCollaborator() {
                const scheduleId = this.scheduleId
                const collaborator = this.newCollaborator
                axios.post(`${URLS.api.scheduleBuilder.addCollaborator}/${scheduleId}/${collaborator}`)
                    .then(response => {
                        alert(response.data)
                    })
                    .catch(e => {
                        alert(e)
                    })
                //if (this.search()) {
                //}
                //else {
                //    alert("User \"" + this.newCollaborator + "\" does not exist")
                //}
            },
            // Updates a collaborator on a given schedule
            onUpdate(collaborator) {
                this.loading = true;
                $.ajax({
                    url: `${URLS.api.scheduleBuilder.updateCollaborator}/${this.scheduleId}`,
                    context: this,
                    data: collaborator,
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
            // Removes a collaborator from a given schedule
            onRemove(scheduleId, collaboratorId) {
                let confirmed = confirm("Are you sure you want to remove this collaborator?")
                if (confirmed) {
                    axios.post(`${URLS.api.scheduleBuilder.deleteCollaborator}/${this.scheduleId}`, collaboratorId)
                        .then(response => {
                            alert(response)
                        })
                        .catch(e => {
                            alert(e)
                        })
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