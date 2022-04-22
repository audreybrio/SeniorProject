<template>
    <h1>This is the email verification page</h1>
    the user name is {{$route.params.username}} and token: {{$route.params.token}}
</template>

<script>
    export default {
        //props: ["name"],
        data() {
            return {
                username: "",
                token: "",


            }
        },
        created{
        this.username = $route.params.username;
        this.token = $route.params.token;

    }
        computed: {
            postData() {
                console.log('posting data...');
                $.ajax({
                    url: `${baseURL}/api/registration/emailVerification/${this.username}/${this.token}`,
                    context: this,
                    processData: true,
                    method: 'POST',
                    success: function (data) {
                        this.isAccountCreated = true;
                        // resets user input values
                        this.resetInputValues
                        return;
                    },
                    error: function (error) {
                        console.log(error);
                        return;
                    }
                });
            }
        }
    }
</script>