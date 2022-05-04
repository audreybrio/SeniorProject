<template>
    <div class="post">
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
</template>

<script lang="js">
    import router from '@/router'
    import URLS from '../../variables'

    export default ({
        data() {
            // count: 0;
            return {
                loading: false,
                post: null,
                errors: "",
                accountDisabled: "Account Disabled",
                count: 0,
                token: "",
                tok: "",
                

            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;
                this.username = '';
                this.otp = '';
                

            },
            onLogin() {
                this.count++;
                if (this.count < 6) {
                    fetch(
                        `${URLS.api.login.authenticate}/${this.username}/${this.otp}`, {
                        method: 'POST',
                        context: this,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        
                    }).then((response) => {
                        if (!response.ok) {

                            console.log("error")
                            this.errors = "Username/OTP Incorrect";
                        }
                        else {
                            console.log("ajax success")
                            return response.json()
                           
                        }

                    }).then((result) => {
                        console.log(result)
                        this.token = JSON.stringify(result);
                        console.log("token", this.token)
                        window.sessionStorage.setItem("token", this.token)
                        router.push({ name: "HomePage" });
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
                router.push({ name: "EmailVue" });
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