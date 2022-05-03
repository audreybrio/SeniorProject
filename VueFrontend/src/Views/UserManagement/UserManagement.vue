<template>
    <div v-if="display">
    very true!
        <table>
            <thead>
                <tr>
                    <th>User ID</th>
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
                    <th>{{ user.username }}</th>
                    <th>{{ user.email }}</th>
                    <th><input type="checkbox" v-model="user.active" /></th>
                    <th><input type="range" v-model="user.role" /></th>
                    <th><button>Update</button></th>
                    <th><button>Delete</button></th>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
    false!
    </div>
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
                users: []
            }
        },
        created() {
            //this.user = this.$router.params.user;
            //this.role = this.$router.params.role;
            this.role = ADMIN;
            if (this.role === ADMIN) {
                this.display = true;
                this.loadUsers();
            }
        },
        methods: {
            loadUsers() {
                this.loading = true
                axios.get(`${URLS.api.admin.getUsers}`, { timeout: 5000 })
                    .then(response => {
                        this.users = response.data
                        this.loading = false
                        alert("Users loaded")
                    })
                    .catch(e => {
                        alert("Could not load users\n" + e)
                    })
            },
            routeUnauthorized() {
                // route the user to unauthorized page
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