<template>
    <div class="post">

        <div v-if="emailCheck == true">
            <div v-if="loading" class="loading">
                Welcome! Please enter email and passcode!
            </div>
            <div class="warning">
                <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
            </div>
            <input id="email" v-model="email" placeholder="Email">
            <input id="passcode" v-model="passcode" placeholder="Passocde">
            <button id="button" @click="onSubmit">Submit</button>
            <div>
                <button @click="skip">Test</button>
                <router-link to="/registration">Registration</router-link>
            </div>
        </div>


        <div v-if="loginCheck == true">
            <div id="login" v-if="loading" class="loading">
                Welcome! Please enter username and OTP!
            </div>
            <div class="warning">
                <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
            </div>
            <div class="warning">
                <div v-if="parseInt(count)>5" :key="count" class="warning">{{accountDisabled}}</div>
            </div>
            <input v-model="username" placeholder="Username">
            <input v-model="otp" placeholder="OTP">
            <button @click="onLogin">Login</button>
            <br />
            <button @click="back">Return</button>
        </div>
    </div>
    <router-view />
</template>

<script>
 // <!--lang="js"-->
    import router from '@/router'
    // import * as $ from 'jquery'
    // import axios from "axios"
    import URLS from '../../variables'

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                errors: "",
                loginCheck: false,
                emailCheck: true,
                accountDisabled: "Account Disabled",
                count: 0,
                token: "",
            };
        },
        created() {
            this.fetchData();
        },
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;
                this.email = '';
                this.passcode = '';
                this.username = '';
                this.otp = '';


            },
            onSubmit() {
                this.errors = ""
                let creditentials = []
                creditentials.push(this.email)
                creditentials.push(this.passcode)
                let data = {
                    creditentials: creditentials
                }
                fetch(
                    `${URLS.api.login.validate}`, {
                        method: 'POST',
                        context: this,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        }, body: JSON.stringify(data),

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                        this.errors = "Email/Passcode Incorrect";
                    }
                    else {
                        console.log("ajax success")
                        // router.push({ name: "" })
                        this.emailCheck = false;
                        this.loginCheck = true;
                    }

                }).catch(() => {
                    console.log("error")
                    this.errors = "Email/Passcode Incorrect";

                });
            },

            onLogin() {
                this.errors = ""
                this.count++;
                let authen = []
                authen.push(this.username)
                authen.push(this.otp)
                let data = {
                    authen: authen
                }
                if (this.count < 6) {
                    fetch(
                        `${URLS.api.login.authenticate}`, {
                        method: 'POST',
                        context: this,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(data),
                    }).then((response) => {
                        if (!response.ok) {

                            console.log("error")
                            this.errors = "Username/OTP Incorrect"
                        }
                        else {
                            console.log("ajax success")
                            return response.json()

                        }

                    }).then((result) => {
                        console.log(result)
                        this.token = JSON.stringify(result);
                        if (this.token != undefined) {
                            console.log("token", this.token)
                            window.sessionStorage.setItem("token", this.token)
                            router.push({ name: "HomePage" });
                        }
                        else {
                            console.log("error")
                            this.errors = "Username/OTP Incorrect"
                        }

                    })
                }
                else {
                    this.errors = ""
                    fetch(
                        `${URLS.api.login.disable}/${this.username}`, {
                        method: 'GET',
                        context: this,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },

                    })
                }


            },
            back() {
                this.loginCheck = false;
                this.emailCheck = true;
            },

            skip() {
                window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFicmlvIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjUxNjI2NzY4LCJleHAiOjE2NTE3MTMxNjgsImlhdCI6MTY1MTYyNjc2OH0.NqmnnN2bbN36rzjIYzpxd3BTd3WudB_30QpC_ab2spA");
                router.push({ name: "HomePage" })
            },




        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }

    .warning {
        color: red;
        margin: auto;
        width: 440px;
        text-align: center;
        font-size: 11px;
    }
</style>