<<<<<<< HEAD
npm<template>
    <div id="app">
        <h2>Registration Form</h2>
        <form @submit.prevent="login">
            <div>
                <label for="username"></label>
                <input name="username" v-model="username" placeholder="Username">
=======
<template>
    <section>
        <h2>CREATE ACCOUNT</h2>
        <br />
        <div class="warning">
            <ul>
                <li v-for="(error, index) in errors" :key="index" class="warning">{{error}}</li>
            </ul>
        </div>
        <div v-if="isAccountCreated" class="accountCreatedStyle">NEW USER ACCOUNT CREATED SUCCESSFULLY</div>
        <div>
            <form @submit.prevent="validateUserInput">
                <div>
                    <label for="username">Username:</label>
                    <br />
                    <input type="text" name="username" v-model="username" placeholder="yourusername12" maxlength="21" required>
                    <br />

                    <label for="password">Password:</label>
                    <br />
                    <input type="password" name="Password" v-model="password" placeholder="YourPassword12" maxlength="25" required>
                    <br />

                    <label for="retype_password">Re-type password:</label>
                    <br />
                    <input type="password" name="Password" v-model="retype_password" placeholder="YourPassword12" maxlength="25" required>
                    <br />

                    <label for="email">Email:</label>
                    <br />
                    <input type="email" name="email" v-model="email" placeholder="Example@student.csulb.edu" maxlength="51" required>
                    <br />

                    <label for="retype_email">Re-type email:</label>
                    <br />
                    <input type="email" name="retype_email" v-model="retype_email" placeholder="Re-type email" maxlength="51" required>
                    <br />

                    <select v-model="university" required>
                        <option disabled value="">Please select a university</option>
                        <option>CSULB</option>
                        <option>Other CSU</option>
                        <option>Any CSU</option>
                    </select>
                    <br />

                    <input type="submit" value="REGISTER" class="button" formnovalidate>
                </div>
            </form>
            <div class="signin">
                Already have an account? <router-link to="/">Login</router-link>
>>>>>>> main
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
<<<<<<< HEAD
=======
    import * as $ from 'jquery'
    //const baseURL = "https://localhost:5002";
    import URLS from '../../variables'
>>>>>>> main
    export default {
        // name: "App",
        data() {
            return {
                username: "",
                password: "",
                retype_password: "",
                email: "",
                retype_email: "",
                university: ""
            };
        },
<<<<<<< HEAD
        methods: {
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
=======
        computed: {
            areValidInputs() {
                if (this.validate.username == true && this.validate.password == true && this.validate.email == true
                    && this.validate.university == true && this.validate.usernameExist == false
                    && this.validate.emailExist == false) {
                    return true
                }
                else {
                    return false
                }
                    
            },
            postData() {
                $.ajax({
                    url: `${URLS.apiRoot}registration/newRegistration/${this.username}/${this.password}/${this.email}/${this.university}`,
                    context: this,
                    processData: true,
                    method: 'POST',
                    success: function () {
                        this.isAccountCreated = true;
                        // resets user input values
                        alert("We have sent an email to " + this.email + " \n \n You need to verify your email to activate"
                        + " your account. If you have not received it, please check your spam or junk email.")
                        this.resetInputValues
                        
                        return;
                    },
                    error: function (error) {
                        console.log(error);
                        return;
                    }
                });
                return true;
            }
        },
        methods: {

            // errorMessages method populates an array with errors when input values are checked for validation
            errorMessages() {
                this.errors = []

                if (this.validate.username == false) {
                    this.errors.push("Username: must be 8 or more charactes and no uppercase/special characters allowed");
                }

                if (this.password != this.retype_password) {
                    this.errors.push("Passwords do not match")
                }
                else if (this.validate.password == false) {
                    this.errors.push("Password: must be 8 or more characters and no special characters allowed");
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

                if (this.validate.emailExist == true) {
                    this.errors.push("Email already exists")
                }
                else {
                    this.emailExist = false;
                }

                if (this.validate.usernameExist == true) {
                    this.errors.push("Username already exists")
                }
                else {
                    this.usernameExist = false;
                }
            },

            // validateUserInput verifies that user input is valid sending a GET request to the server's api
            // and receiving the values of the valid or invalid input fields
            validateUserInput() {
                this.isAccountCreated = false;
                this.resetValidateValues;
                $.ajax({
                    // set the HTTP request URL
                    url: `${URLS.apiRoot}registration/validation/${this.username}/${this.password}/${this.email}/${this.university}`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    method: 'GET',
                    // On a successful AJAX request:
                    success: function (data) {
                        this.validate.username = data[0].username;
                        this.validate.password = data[0].password;
                        this.validate.email = data[0].email;
                        this.validate.university = data[0].university;
                        this.validate.usernameExist = data[0].usernameExist;
                        this.validate.emailExist = data[0].emailExist;
                        // log that we've completed
                        this.errorMessages()
                        if (this.areValidInputs) {
                            // Creates a new user account if user inputs are valid
                            this.postData
                        }
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
            resetValidateValues() {
                this.validate.username = false;
                this.validate.password = false;
                this.validate.email = false;
                this.validate.university = false;
                this.validate.usernameExist = true;
                this.validate.emailExist = true;
            },
            resetInputValues() {
                this.username = "";
                this.password = "";
                this.email = "";
                this.university = "";
                this.retype_email = "";
                this.retype_password = "";
            },
>>>>>>> main
        }
    };
</script>

