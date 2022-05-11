<template>
    <div>
        <h3>Delete My Account</h3>
        This page will delete all of the data associated with your account.
        This is permanent. Only click the button if you wish to proceed.
        <button @click="onDelete">Delete My Account</button>
    </div>
    <div>
        <br />
        <button @click="onBack">Return</button>
    </div>
    <router-view />
</template>

<script lang="js">
    import axios from 'axios'
    import router from '../../router'
    import URLS from '../../variables'
    import jwt_decode from 'jwt-decode'
    export default {
        name: 'AccountDeletion',
        data() {
            return {
                user: jwt_decode(window.sessionStorage.getItem("token")).username
            }
        },
        created() {
        },
        methods: {
            onDelete() {
                let username = this.user
                let confirmed = confirm("Are you sure you want to delete your account?\nThis is irreversible.")
                if (confirmed) {
                    axios.post(`${URLS.api.userPrivacy.accountDeletion}?username=${username}`)
                        .then(response => {
                            console.log(response)
                            alert("Your account has been deleted.")
                            router.push({ path: '/' })
                        })
                        .catch(error => {
                            alert("Error: Could not delete your account\n" + error)
                            console.log(error)
                        })
                }
            },
            onBack() {
                router.push({ name: "HomePage" })
            }
        }
    };
</script>