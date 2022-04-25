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
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";

    export default ({
        data() {
            // count: 0;
            return {
                loading: false,
                post: null,
                errors: "",
                accountDisabled: "Account Disabled",
                count: 0
                

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
                

                fetch('weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });

            },
            onLogin() {
                $.ajax({
                    url: `${baseURL}/api/login/authenticate/${this.username}/${this.otp}`,
                    context: this,
                    method: 'GET',
                    success: function () {
                        console.log("ajax success")
                        if (this.username == "abrio") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQXVkcmV5IEJyaW8iLCJ1c2VybmFtZSI6ImFicmlvIiwiZW1haWwiOiJhdWRyZXkuYnJpb0BzdHVkZW50LmNzdWxiLmVkdSIsInJvbGUiOiJhZG1pbiIsInNjaG9vbCI6IkNTVUxCIn0.q-t3-fdbH15V8KXYCK6RfgUw9sNxdBMyVEA5tZg6MzE");
                        }
                        else if (this.username == "bnickle") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQnJhZGxleSBOaWNrbGUiLCJ1c2VybmFtZSI6ImJuaWNrbGUiLCJlbWFpbCI6ImJyYWRsZXkubmlja2xlQHN0dWRlbnQuY3N1bGIuZWR1Iiwicm9sZSI6InN0dWRlbnQiLCJzY2hvb2wiOiJDU1VMQiJ9.Ntm8yiFnS7WfrU1FIg37OIgKTbBi7ejCPXLLiVrKRpU");
                        }
                        else if (this.username == "jcutri") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSm9zZXBoIEN1dHJpIiwidXNlcm5hbWUiOiJqY3V0cmkiLCJlbWFpbCI6Impvc2VwaC5jdXRyaUBzdHVkZW50LmNzdWxiLmVkdSIsInJvbGUiOiJzdHVkZW50Iiwic2Nob29sIjoiQ1NVTEIifQ.OzX3sALvaZ9ZN839HNVkE-5XtvYPucteHYok0xSHf_k");
                        }
                        else if (this.username == "atoscano") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQWxiZXJ0IFRvc2Nhbm8iLCJ1c2VybmFtZSI6ImF0b3NjYW5vIiwiZW1haWwiOiJhbGJlcnQudG9zY2FubzAxQHN0dWRlbnQuY3N1bGIuZWR1Iiwicm9sZSI6ImFkbWluIiwic2Nob29sIjoiQ1NVTEIifQ.2zjbN082tbynsaX5DiP9vurjQaNi6_GONtkDi2l663E");
                        }
                        else if (this.username == "jdelgado") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSmFjb2IgRGVsZ2FkbyIsInVzZXJuYW1lIjoiamRlbGdhZG8iLCJlbWFpbCI6ImphY29iLmRlbGdhZG9Ac3R1ZGVudC5jc3VsYi5lZHUiLCJyb2xlIjoic3R1ZGVudCIsInNjaG9vbCI6IkNTVUxCIn0.tPMhd4RnLJgRSlST6_8jgHLxGXD53v8067ApEY9I5N0");
                        }
                        else if (this.username == "stang") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU3plTWFuIFRhbmciLCJ1c2VybmFtZSI6InN0YW5nIiwiZW1haWwiOiJzemVtYW4udGFuZ0BzdHVkZW50LmNzdWxiLmVkdSIsInJvbGUiOiJzdHVkZW50Iiwic2Nob29sIjoiQ1NVTEIifQ.uhv7AvYnnoTyfo4-EFMhGJ1RjV2UJ79GHtNrzkk5wMA");
                        }
                        else if (this.username == "dpatel") {
                            window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiRGV2YXJzaCBQYXRlbCIsInVzZXJuYW1lIjoiZHBhdGVsIiwiZW1haWwiOiJkZXZhcnNoLnBhdGVsQHN0dWRlbnQuY3N1bGIuZWR1Iiwicm9sZSI6InN0dWRlbnQiLCJzY2hvb2wiOiJDU1VMQiJ9.BeIYgDfebLnSJ6RgTdjCc3KNCguixNVYOGelghn1BO8");
                        }
                        router.push({ name: "HomePage" });

                    },
                    error: function () {
                        console.log("error")
                        this.count = this.count + 1;
                        this.errors = "Username/Otp Incorrect";
                        
                        
                    }
                })
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