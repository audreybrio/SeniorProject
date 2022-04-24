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
            <button @click="skip">Skip</button>
            <router-link to="/registration">Registration</router-link>
        </div>
    </div>
    <router-view />
</template>

<script> // <!--lang="js"-->
    import router from '@/router'
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                errors: "",
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
                this.email = '';
                this.passcode = '';

                fetch('weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });

            },
            onSubmit() {
                $.ajax({
                    url: `${baseURL}/api/login/validate/${this.email}/${this.passcode}`,
                    context: this,
                    method: 'GET',
                    success: function () {
                        console.log("ajax success")
                        router.push({ name: "LoginVue" });

                    },
                    error: function () {
                        console.log("error")
                        this.errors = "Email/Passcode Incorrect";
                    }
                })
            },

            skip() {
                window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQXVkcmV5IEJyaW8iLCJ1c2VybmFtZSI6ImFicmlvIiwiZW1haWwiOiJhdWRyZXkuYnJpb0BzdHVkZW50LmNzdWxiLmVkdSIsInBhc3Njb2RlIjoiaGVsbG8gd29ybGQiLCJyb2xlIjoiYWRtaW4iLCJzY2hvb2wiOiJDU1VMQiJ9.O9qyhghpLVvFIurYuDaAzLV6r9HVpO0DrXBhTbB-3Yo");
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