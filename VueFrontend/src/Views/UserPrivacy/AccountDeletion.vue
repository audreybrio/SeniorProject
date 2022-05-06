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
    export default {
        name: 'AccountDeletion',
        data() {
            return {
                user: this.$route.params.user
            }
        },
        created() {
        },
        methods: {
            onDelete() {
                let user = this.user
                let confirmed = confirm("Are you sure you want to delete your account?\nThis is irreversible.")
                if (confirmed) {
                    axios.post(`${URLS.api.userPrivacy.accountDeletion}`, user)
                        .then(response => {
                            console.log(response)
                        })
                        .catch(error => {
                            console.log(error)
                        })
                    router.push({ path: '/' })
                }
            },
            onBack() {
                router.push({ name: "HomePage" })
            }
        }
    };
</script>