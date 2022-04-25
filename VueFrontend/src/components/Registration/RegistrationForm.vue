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
            </div>
        </div>
    </section>
    </template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
        data() {
            return {
                isAccountCreated: false,
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
                    url: `${baseURL}/api/registration/newRegistration/${this.username}/${this.password}/${this.email}/${this.university}`,
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
        }
    };
</script>

<style scoped>
    .signin {
        margin:auto;
        margin-top: 7px;
        margin-bottom: 7px;
        width: 295px;
        border: 2px solid;
        border-color: dimgray;
        background-color: beige;
        border-radius: 5px;
        height:45px;
        padding-top:20px;
        align-content:center;
    }
    select {
        text-align:left;
        height:30px;
        font-size:15px;
        margin-bottom:7px;
    }
    .button {
        color: white;
        width: 100%;
        background-color: red;
        font-size:15px;
        border-radius:10px;
    }
    input{
        font-size:15px;
        height:30px;
        width:95%;
        margin-bottom:7px;
    }
    .accountCreatedStyle{
        font-size:11px;
        background-color:gold;
        border: 1px solid gold;
        border-radius: 5px 4px;

    }
    form {
        margin: auto;
        width: 270px;
        text-align:left;
        border:2px solid;
        border-color:dimgray;
        box-shadow:2px 2px;
        padding:10px;
    }
    .warning {
        color:red;
        margin:auto;
        width: 440px;
        text-align:left;
        font-size:11px;

    }

    section {
        margin: auto;
        background-color:#ececec;
    }

    input[type=text]:focus, input[type=password]:focus, input[type=email]:focus {
        background-color: lightblue;
        border: 2px solid red;
        border-radius: 4px;
    }
</style>