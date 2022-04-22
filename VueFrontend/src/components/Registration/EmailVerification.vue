<template>
    <h1>This is the email verification page</h1>
    <div class="congrates">
        Congratulations {{this.username }}.
    </div>
    Your Account is activated.
</template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
        data() {
            return {
                username: this.$route.params.username,
                token: this.$route.params.token,

            }
        },
        created() {
            this.isVerified
        },
        computed: {
            isVerified() {
                console.log('posting data...');
                var validaded = $.ajax({
                    url: `${baseURL}/api/registration/emailVerification/${this.username}/${this.token}`,
                    context: this,
                    processData: true,
                    method: 'POST',
                    success: function (data) {
                        //this.isAccountCreated = true;
                        // resets user input values
                        //this.isVerified = true;
                        return true;
                    },
                    error: function (error) {
                        console.log(error);
                        //this.isVerified = false;
                        return false;
                    }
                });
                return validated;
            }
        }
    }
</script>

<style>
    .congrates{
        font-size:30px;
    }
</style>