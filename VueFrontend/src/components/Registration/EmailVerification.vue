<template>
    <div class="container">
        <h1>This is the email verification page</h1>

        <div v-if="this.isVerified">
            <div class="congrates">
                Congratulations {{this.username }}.
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
    import * as $ from 'jquery'
    import URLS from '../../variables'
    export default {
        data() {
            return {
                username: this.$route.params.username,
                token: this.$route.params.token,
                isVerified: false
            }
        },
        created() {
            //const baseURL = "https://localhost:5002";
            $.ajax({
                url: `${URLS.apiRoot}/registration/emailVerification/${this.username}/${this.token}`,
                context: this,
                processData: true,
                method: 'POST',
                success: function (data) {
                    if (data == "Success") {
                        this.isVerified = true;
                    }
                    else {
                        this.isVerified = false;
                    }
                    
                    return true;
                },
                error: function (error) {
                    console.log(error);
                    
                    return false;
                }
            });

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
    .congrates{
        font-size:30px;
    }

</style>