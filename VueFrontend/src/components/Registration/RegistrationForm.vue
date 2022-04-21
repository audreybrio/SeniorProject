<template>
        <section>
            <h2>CREATE ACCOUNT</h2>

            <form @submit.prevent="validateUserInput">
                <div class="warning">
                    <div v-for="(error, index) in errors" :key="index">{{index + 1}}. {{error}}</div>
                </div>

                <div>
                    <label for="username"></label>
                    <input type="text" name="username" v-model="username" placeholder="Username" required>
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
                    <input type="text" name="university" v-model="university" placeholder="University" required>
                </div>
                <input type="submit" value="register" formnovalidate>

                <div>
                    <router-link to="/">Login</router-link>
                </div>
            </form>
        </section>
    </template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
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
            signUpButtonPressed() {
                console.log("Sign up button pressed!")
                this.errors = []
                console.log("after validateUserInput")
                console.log(this.validate)

                if (this.validate.username == false) {
                    this.errors.push("Username must be 8 or more int and lowercase characters");
                }

                if (this.password != this.retype_password) {
                    this.errors.push("Passwords do not match")
                }
                else if (this.validate.password == false) {
                    this.errors.push("Password must be 8 or more int and upper/lowercase characters");
                }

                if (this.email != this.retype_email) {
                    this.errors.push("Email addresses do not match")
                }
                else if (this.validate.email == false) {
                    this.errors.push("Email must contaion @ and .edu extension")
                }

                if (this.validate.university == false) {
                    this.errors.push("Invalid value for university")
                }



            },
            validateUserInput() {
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
                        this.signUpButtonPressed()
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
    .warning {
        color:red;
    }

    section {
        /*height: 100vh;*/
        background-color: #ececec;
        /*display: flex;*/
        align-items: center;
        justify-content: center;
    }

    input[type=text]:focus, input[type=password]:focus, input[type=email]:focus {
        background-color: lightblue;
        border: 2px solid red;
        border-radius: 4px;
        text-align:left;
    }
</style>