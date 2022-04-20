<template>
    <section>
        <div class="ui segment grey signup-form">
            <h2 class="ui header">CREATE ACCOUNT</h2>
            <div class="ui message red big">
                <div v-for="(error, index) in errors" :key="index">{{error}}</div>
            </div>


            <form class="ui form" @submit.prevent="signUpButtonPressed">
                <div class="ui stacked segment">

                    <div class="field">
                        <div class="ui left icon input big">
                            <i class="user icon"></i>
                            <label for="username"></label>
                            <input name="username" v-model="username" placeholder="Username" required>
                            <!--<input type="text" placeholder="Username" />-->
                        </div>
                    </div>

                    <div class="field">
                        <div class="ui left icon input big">
                            <i class="lock icon"></i>
                            <label for="password"></label>
                            <input name="password" v-model="password" placeholder="password" type="password" required>
                            <!--<input type="password" placeholder="Password" />-->
                        </div>
                    </div>

                    <div class="field">
                        <div class="ui left icon input big">
                            <i class="lock icon"></i>
                            <label for="retype_password"></label>
                            <input name="retype_password" v-model="retype_password" placeholder="Re-type password" type="password">
                            <!--<input type="password" placeholder="Re-type password" />-->
                        </div>
                    </div>

                    <div class="field">
                        <div class="ui left icon input big">
                            <i class="envelope icon"></i>
                            <label for="email"></label>
                            <input name="email" v-model="email" placeholder="email" type="email" required>
                            <!-- <input type="email" placeholder="Email" />-->
                        </div>
                    </div>

                    <div class="field">
                        <div class="ui left icon input big">
                            <i class="envelope icon"></i>
                            <label for="retype_email"></label>
                            <input name="retype_email" v-model="retype_email" placeholder="Re-type email" type="email">
                            <!--<input type="email" placeholder="Re-type email" />-->
                        </div>
                    </div>

                    <div class="field">
                        <div class="ui left icon input big">
                            <i class="university icon"></i>
                            <label for="university"></label>
                            <input name="university" v-model="university" placeholder="University" required>
                            <!--<input type="text" placeholder="University" />-->
                        </div>
                    </div>
                    <button class="ui button big red fluid" @submit="signUpButtonPressed">SIGN UP</button>
                </div>
            </form>
            <div class="ui message large">
                Already have an account?
                <a><router-link to="/">Login</router-link></a>
            </div>
        </div>
    </section>








<!--    <div>
        <h2>Registration Form</h2>
        <form @submit.prevent="validateUserName">
            <div>
                <label for="username"></label>
                <input name="username" v-model="username" placeholder="Username" required>
            </div>
            <div>
                <label for="password"></label>
                <input name="password" v-model="password" placeholder="password" type="password" required>
            </div>
            <div>
                <label for="retype_password"></label>
                <input name="retype_password" v-model="retype_password" placeholder="Re-type password" type="password">
            </div>
            <div>
                <label for="email"></label>
                <input name="email" v-model="email" placeholder="email" type="email" required>
            </div>
            <div>
                <label for="retype_email"></label>
                <input name="retype_email" v-model="retype_email" placeholder="Re-type email" type="email">
            </div>
            <div>
                <label for="university"></label>
                <input name="university" v-model="university" placeholder="University" required>
            </div>
            <input type="submit" value="register">

            <div>
                <router-link to="/">Login</router-link>
            </div>
        </form>
    </div>-->
</template>
<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
        // name: "App",
        data() {
            return {
                username: "",
                password: "",
                retype_password: "",
                email: "",
                retype_email: "",
                university: "",
                items: [],
                errors: [],
                validate: {
                    username: false,
                    password: false,
                    email: false,
                    university: false,
                    usernameExist: true,
                    emailExist: true
                }
            };
        },
        methods: {
            signUpButtonPressed(e) {
                console.log("Sign up button pressed!")
                this.errors = []
                if (this.username == "") {
                    this.errors.push("User name is empty");
                }
                if (this.email == "") {
                    this.errors.push("Email is empty");
                }
                if (this.password == "") {
                    this.errors.push("Password is empty");
                }
                if (this.password != this.retype_password) {
                    this.errors.push("Passwords do not match")
                    this.retype_password = ""
                }
                if (this.email != this.retype_email) {
                    this.errors.push("Email addresses do not match")
                    this.retype_email = ""
                }
            },
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
                        console.log(data)
                        this.validate.username = data[0].username;
                        this.validate.password = data[0].password;
                        this.validate.email = data[0].email;
                        this.validate.university = data[0].university;
                        this.validate.usernameExist = data[0].usernameExist;
                        this.validate.emailExist = data[0].emailExist;
                        console.log(this.validate)
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

<style>
    section {
        height: 100vh;
        background-color: #ececec;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .signup-form {
        width: 450px;
        text-align: center;
    }
</style>