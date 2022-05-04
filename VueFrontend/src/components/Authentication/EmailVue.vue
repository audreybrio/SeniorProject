<template>
    <div class="post">
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
    <router-view />
</template>

<script> // <!--lang="js"-->
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


            },
            onSubmit() {
                //$.ajax({
                //    url: `${URLS.api.login.validate}/${this.email}/${this.passcode}`,
                //    context: this,
                //    method: 'GET',
                //    success: function () {
                //        console.log("ajax success")
                //        router.push({ name: "LoginVue" });

                //    },
                //    error: function () {
                //        console.log("error")
                //        this.errors = "Email/Passcode Incorrect";
                //    }
                //})

                fetch(
                    `${URLS.api.login.validate}/${this.email}/${this.passcode}`, {
                        method: 'POST',
                        context: this,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                        this.errors = "Email/Passcode Incorrect";
                    }
                    else {
                        console.log("ajax success")
                        router.push({ name: "LoginVue" })
                    }
                    
                }).catch(() => {
                    console.log("error")
                    this.errors = "Email/Passcode Incorrect";

                });
            },

            skip() {
                window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFicmlvIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjUxNjI2NzY4LCJleHAiOjE2NTE3MTMxNjgsImlhdCI6MTY1MTYyNjc2OH0.NqmnnN2bbN36rzjIYzpxd3BTd3WudB_30QpC_ab2spA");
                router.push({ name: "authenticateUser" })
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