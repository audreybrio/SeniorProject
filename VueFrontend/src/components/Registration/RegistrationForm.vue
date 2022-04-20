<template>
    <div id="app">
        <h2>Registration Form</h2>
        <form @submit.prevent="validateUserName">
            <div>
                <label for="username"></label>
                <input name="username" v-model="username" placeholder="Username" >
            </div>
            <div>
                <label for="password"></label>
                <input name="password" v-model="password" placeholder="password" type="password">
            </div>
            <div>
                <label for="retype_password"></label>
                <input name="retype_password" v-model="retype_password" placeholder="Re-type password" type="password">
            </div>
            <div>
                <label for="email"></label>
                <input name="email" v-model="email" placeholder="email" type="email">
            </div>
            <div>
                <label for="retype_email"></label>
                <input name="retype_email" v-model="retype_email" placeholder="Re-type email" type="email">
            </div>
            <div>
                <label for="university"></label>
                <input name="university" v-model="university" placeholder="University">
            </div>
            <input type="submit" value="register">

            <div>
                <router-link to="/">Login</router-link>
            </div>
        </form>
    </div>
</template>
<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
        // name: "App",
        data() {
            return {
                username: '',
                password: '',
                retype_password: '',
                email: '',
                retype_email: '',
                university: '',
                items: [],
                validate: {
                    username: false
                }
            };
        },
        methods: {
            validateUserName() {
                console.log('validating name...');
                $.ajax({
                    // set the HTTP request URL
                    url: `${baseURL}/api/registration/validation/${this.username}/${this.password}/${this.email}/${this.university}`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    method: 'GET',
                    // On a successful AJAX request:
                    success: function (data) {
                        //this.validate.username = data;
                        console.log(data)
                        // log that we've completed
                        console.log("ajax Success")
                        return true;
                    },
                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(error);
                        this.items = null;
                        return false;
                    }
                });
            },
            postData() {
                console.log('posting data...');
                $.ajax({
                    url: `${baseURL}/api/registration/newRegistration/${this.username}/${this.password}/${this.email}/${this.university}`,
                    context: this,
                    processData: true,
                    method: 'POST',
                    success: function (data) {
                        console.log(data);
                        // this.loading = false;
                        return;
                    },
                    error: function (error) {
                        console.log(error);
                        // this.loading = false;
                        return;
                    }
                });
            },
            getData() {
                console.log('getting data...');
                $.ajax({
                    // set the HTTP request URL
                    url: `${baseURL}/api/registration`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    method: 'GET',
                    // On a successful AJAX request:
                    success: function (data) {
                        this.items = data;
                        console.log(this.items)
                        // log that we've completed
                        console.log("ajax Success")
                        return true;
                    },
                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(error);
                        this.items = null;
                        return false;
                    }
                });
            },

            async login() {
                const { username, password, retype_password, email, retype_email, university } = this;
                let res = await fetch(
                    "https://SomberHandsomePhysics--five-nine.repl.co/register",
                    {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({
                            username,
                            password,
                            retype_password,
                            email,
                            retype_email,
                            university
                        })
                    }
                )
                const data = await res.json();
                console.log(data);
            }
        }
    };
</script>

