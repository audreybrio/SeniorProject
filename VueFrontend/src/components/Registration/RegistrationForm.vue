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
                    <label for="email">Email:</label>
                    <br />
                    <input type="email" name="email" v-model="email" placeholder="Example@student.csulb.edu" maxlength="51" required>
                    <br />

                    <label for="retype_email">Re-type email:</label>
                    <br />
                    <input type="email" name="retype_email" v-model="retype_email" placeholder="Re-type email" maxlength="51" required>
                    <br />

                    <label for="passcode">Passcode:</label>
                    <br />
                    <input type="password" name="Passcode" v-model="passcode" placeholder="YourPasscode12" maxlength="25" required>
                    <br />

                    <label for="retype_passcode">Re-type passcode:</label>
                    <br />
                    <input type="password" name="Passcode" v-model="retype_passcode" placeholder="YourPasscode12" maxlength="25" required>
                    <br />

                    <select v-model="university" required>
                        <option disabled value="">Please select a university</option>
                        <option>CSULB</option>
                        <option>Other CSU</option>
                        <option>Any CSU</option>
                    </select>
                    <br />

                    <input type="submit" value="REGISTER" class="button">
                </div>
            </form>
            <div class="signin">
                Already have an account? <router-link to="/">Login</router-link>
            </div>
        </div>
    </section>
    </template>

<script>
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        data() {
            return {
                isAccountCreated: false,
                email: "",
                retype_email: "",
                passcode: "",
                retype_passcode: "",
                university: "",
                errors: [],
                validate: {
                    email: false,
                    passcode: false,
                    university: false,
                    emailExist: true
                }
            };
        },
        computed: {
            // if all user inputs are valid, returns true
            areValidInputs() {
                if (this.validate.email == true && this.validate.passcode == true
                    && this.validate.university == true && this.validate.emailExist == false
                    && this.passcode == this.retype_passcode) {
                    return true
                }
                else {
                    return false
                }
                    
            }
        },
        methods: {
            // creates a new user account and saves the data in the DB
            postData() {
                axios.post(URLS.api.registration.newRegistration,
                    {
                        email: this.email,
                        passcode: this.passcode,
                        university: this.university
                    },
                    )
                    .then(response => {
                        this.isAccountCreated = true;
                        if (response.status == 200) {
                            alert("We have sent an email to " + this.email + " \n \n You need to verify your email to activate"
                                + " your account. If you have not received it, please check your spam or junk email.")
                        }
                        else {
                            alert("There was an error creating a new account.")
                        }
                        // resets user input values
                        this.resetInputValues()
                    })
                    .catch(e => {
                        console.error("There was an error", e)
                    })
            },
            // errorMessages method populates an array with errors when input values are checked for validation
            errorMessages() {
                this.errors = []

                if (this.passcode != this.retype_passcode) {
                    this.errors.push("Passcodes do not match")
                }
                else if (this.validate.passcode == false) {
                    this.errors.push("Passcode: must be 8 or more characters and no special characters allowed");
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

            },

            // validateUserInput verifies that user input is valid sending a GET request to the server's api
            // and receiving the values of the valid or invalid input fields
            validateUserInput() {
                this.isAccountCreated = false;
                this.resetValidateValues;
                axios.get(URLS.api.registration.inputValidation + this.email + "/" + this.passcode + "/" + this.university,
                    { timeout: 5000 })
                    .then(response => {
                        this.validate.email = response.data[0].validEmail;
                        this.validate.passcode = response.data[0].validPasscode;
                        this.validate.university = response.data[0].validUniversity;
                        this.validate.emailExist = response.data[0].emailExist;
                        // log that we've completed
                        this.errorMessages()
                        if (this.areValidInputs) {
                            // Creates a new user account if user inputs are valid
                            this.postData()
                        }
                    })
                    .catch(e => {
                        console.error("There was an error", e)
                    })

            },
            resetValidateValues() {
                this.validate.email = false;
                this.validate.passcode = false;
                this.validate.university = false;
                this.validate.emailExist = true;
            },
            resetInputValues() {
                this.email = "";
                this.retype_email = "";
                this.passcode = "";
                this.retype_passcode = "";
                this.university = "";
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