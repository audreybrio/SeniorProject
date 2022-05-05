<template>
    <h2>User Management</h2>
    <button v-bind="fileUpload" @click="toggleMode" >
        <p v-if="fileUpload">Single Operation Mode</p>
        <p v-else>Bulk Operation Mode</p>
    </button>
    <div v-if="fileUpload">
        <label for="fileField">Upload bulk operations file here ( size limit: 1MB )</label>
        <div>
            <form>
                <input type="file" id="fileField" required />
                <button @click="onClear" type="reset">Clear</button>
                <button @click="onExecute" type="submit">Execute</button>
            </form>
        </div>
    </div>
    <!--<div v-if="display">-->
    <div v-else>
    very true!
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Active</th>
                    <th>Role</th>
                    <th>Update</th>
                    <th>Delete</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users" :key="user.id">
                    <th>{{ user.id }}</th>
                    <th><input type="text" v-model="user.username" /></th>
                    <th><input type="text" v-model="user.email" /></th>
                    <th><input type="checkbox" v-model="user.active" /></th>
                    <th>
                        <select v-model="user.role">
                            <option disabled value="">This user's role</option>
                            <option v-for="role in possibleRoles" :key="role">{{ role }}</option>
                        </select>
                    </th>
                    <th><button @click="update(user)">Update</button></th>
                    <th><button @click="onDelete(user)">Delete</button></th>
                </tr>
            </tbody>
        </table>
    </div>
    <!--<div v-else>
    false!
    </div>-->
</template>

<script lang="js">
    /* eslint-disable */
    import router from '../../router'
    import URLS from '../../variables'
    import axios from 'axios'
    const ADMIN = 'admin'
    export default ({
        data() {
            return {
                user: null,
                role: null,
                users: [],
                possibleRoles: [],
                fileUpload: false,
                file: null
            }
        },
        created() {
            //this.user = this.$router.params.user;
            //this.role = this.$router.params.role;
            this.role = ADMIN;
            if (this.role === ADMIN) {
                this.display = true;
                this.loadRoles();
                this.loadUsers();
            }
        },
        methods: {
            toggleMode() {
                this.fileUpload = !this.fileUpload
            },
            onClear() {
                this.file = null;
            },
            onExecute(file, onUploadProgress) {
                let confirmed = confirm("Are you sure you want to upload and execute?");
                if (confirmed) {
                    let formData = new FormData();
                    formData.append("file", file);
                    axios.post(`${URLS.api.admin.runBulkOperation}`, formData, {
                        headers: {
                            "Content-Type": "multipart/form-data"
                        },
                        onUploadProgress
                    })
                        .then(response => {
                            alert(response)
                        })
                        .catch(e => {
                            alert(e)
                        })
                }
            },
            loadRoles() {
                this.loading = true
                axios.get(`${URLS.api.admin.getRoles}`, { timeout: 5000 })
                    .then(response => {
                        this.possibleRoles = response.data
                        this.loading = false
                        alert("Roles loaded")
                    })
                    .catch(e => {
                        this.loading = false
                        alert("Could not load users\n" + e)
                    })
            },
            loadUsers() {
                this.loading = true
                axios.get(`${URLS.api.admin.getUsers}`, { timeout: 5000 })
                    .then(response => {
                        this.users = response.data
                        this.loading = false
                        alert("Users loaded")
                    })
                    .catch(e => {
                        this.loading = false
                        alert("Could not load users\n" + e)
                    })
            },
            routeUnauthorized() {
                router.push({ name: "not-authorized" });
            },
            routeBulk() {
                router.push({ name: "BulkOperations" });
            },
            routeHome() {
                router.push({ name: "HomePage" });
            },
            update(user) {
                user.passcode = "null"
                user.password = "null"
                axios.post(`${URLS.api.admin.updateUsers}`, [user], {
                    timeout: 5000
                })
                    .then(response => {
                        alert("Updated user with id " + user.id)
                    })
                    .catch(e => {
                        alert("Could not update user with id " + user.id + "\n" + e)
                    })
            },
            onDelete(user) {
                user.passcode = "null"
                user.password = "null"
                axios.post(`${URLS.api.admin.deleteUsers}`, [user], {
                    timeout: 5000
                })
                    .then(response => {
                        alert("Deleted user with id " + user.id)
                    })
                    .catch(e => {
                        alert("Could not delete user with id " + user.id + "\n" + e)
                    })
            }
        }
    });
</script>

<style>
    table {
        margin: auto;
    }
    th {
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }
    td {
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }
</style>