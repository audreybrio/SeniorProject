<template>
    <div class="container">
        <h1>This is the email verification page</h1>

        <div v-if="this.isVerified">
            <div class="congrats">
                Congratulations!
                <br />
            </div>
            Your Account is activated. Please log in to your Student Multi-Tool account.
        </div>
        <div v-else>
            <div class="error">
                Error.
                <br />
            </div>
            There is an error in the email verification proccess.
        </div>
        <div class="action">
            <router-link to="/" class="link">Login</router-link>
        </div>
        
    </div>
</template>

<script>
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        data() {
            return {
                token: this.$route.params.token,
                isVerified: false
            }
        },
        created() {
            axios.post(URLS.api.registration.emailVerification,
                { token: this.token },
                { timeout: 5000 })
                .then(response => {
                    if (response.data == "Success") {
                        this.isVerified = true;
                    }
                    else {
                        this.isVerified = false;
                    }
                })
                .catch(e => {
                    console.log(e)
                })

        }
    }
</script>

<style scope>
    .error {
        color: red;
        font-size: 30px;
    }
    .container {
        margin: auto;
        background-color: #ececec;
    }
    .congrats{
        font-size:30px;
    }

</style>