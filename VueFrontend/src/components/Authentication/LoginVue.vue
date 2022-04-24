<template>
    <div class="post">
        <div id="login" v-if="loading" class="loading">
            Welcome! Please enter username and OTP!
        </div>
        <input v-model="username" placeholder="Username">
        <input v-model="otp" placeholder="OTP">
        <button @click="onLogin">Login</button>
        <button id="skip" @click="onSkip">Skip</button>

    </div>
    <router-view/>
</template>

<script lang="js">
    import router from '@/router'
    
    export default ({
        data() {
            return {
                loading: false,
                post: null,

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
            onSkip() {

                window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQXVkcmV5IEJyaW8iLCJ1c2VybmFtZSI6ImFicmlvIiwiZW1haWwiOiJhdWRyZXkuYnJpb0BzdHVkZW50LmNzdWxiLmVkdSIsInBhc3Njb2RlIjoiaGVsbG8gd29ybGQiLCJyb2xlIjoiYWRtaW4iLCJzY2hvb2wiOiJDU1VMQiJ9.O9qyhghpLVvFIurYuDaAzLV6r9HVpO0DrXBhTbB-3Yo");
                router.push({ name: "HomePage" });

            },
            onLogin() {
                if (this.username == "abrio" && this.otp == "ha3dal9w") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQXVkcmV5IEJyaW8iLCJ1c2VybmFtZSI6ImFicmlvIiwiZW1haWwiOiJhdWRyZXkuYnJpb0BzdHVkZW50LmNzdWxiLmVkdSIsInBhc3Njb2RlIjoiaGVsbG8gd29ybGQiLCJyb2xlIjoiYWRtaW4iLCJzY2hvb2wiOiJDU1VMQiJ9.O9qyhghpLVvFIurYuDaAzLV6r9HVpO0DrXBhTbB-3Yo");
                    router.push({ name: "HomePage" });

                }
                else if (this.username == "bnickle" && this.otp == "9c8e0aaj") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQnJhZGxleSBOaWNrbGUiLCJ1c2VybmFtZSI6ImJuaWNrbGUiLCJlbWFpbCI6ImJyYWRsZXkubmlja2xlQHN0dWRlbnQuY3N1bGIuZWR1IiwicGFzc2NvZGUiOiJtYXJ2ZWwgZmFuIiwicm9sZSI6InN0dWRlbnQiLCJzY2hvb2wiOiJDU1VMQiJ9.RfciY1UlYZq28gi1yCg0XAL0MiqDzYiyTYj_GEQBLyc");

                    router.push({ name: "HomePage" });

                }
                else if (this.username == "jcutri" && this.otp == "vef99e4b") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSm9zZXBoIEN1dHJpIiwidXNlcm5hbWUiOiJqY3V0cmkiLCJlbWFpbCI6Impvc2VwaC5jdXRyaUBzdHVkZW50LmNzdWxiLmVkdSIsInBhc3Njb2RlIjoibnVtYmVyIG9uZSIsInJvbGUiOiJzdHVkZW50Iiwic2Nob29sIjoiQ1NVTEIifQ.gKoaT7lUQBH1po6U1tOLaSarl4Y4o9rAZRLObv9dyfco");

                    router.push({ name: "HomePage" });

                }
                else if (this.username == "atoscano" && this.otp == "9z93c18u") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQWxiZXJ0IFRvc2Nhbm8iLCJ1c2VybmFtZSI6ImF0b3NjYW5vIiwiZW1haWwiOiJhbGJlcnQudG9zY2FubzAxQHN0dWRlbnQuY3N1bGIuZWR1IiwicGFzc2NvZGUiOiJoYXBweSBtZWFsIiwicm9sZSI6ImFkbWluIiwic2Nob29sIjoiQ1NVTEIifQ.pWdFoOiopjMglW5yWcFtoPWCch8qFwmNJyciCt8lQlw");

                    router.push({ name: "HomePage" });

                }
                else if (this.username == "jdelgado" && this.otp == "yjc3ghih") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSmFjb2IgRGVsZ2FkbyIsInVzZXJuYW1lIjoiamRlbGdhZG8iLCJlbWFpbCI6ImphY29iLmRlbGdhZG9Ac3R1ZGVudC5jc3VsYi5lZHUiLCJwYXNzY29kZSI6InN1cGVyIG1hbiIsInJvbGUiOiJzdHVkZW50Iiwic2Nob29sIjoiQ1NVTEIifQ.hDqZc51Cy5H_8mWs3V3lGqlKm9-HE069F0VxIvhB6PI");

                    router.push({ name: "HomePage" });

                }
                else if (this.username == "stang" && this.otp == "7kfldn35") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU3plTWFuIFRhbmciLCJ1c2VybmFtZSI6InN0YW5nIiwiZW1haWwiOiJzemVtYW4udGFuZ0BzdHVkZW50LmNzdWxiLmVkdSIsInBhc3Njb2RlIjoid29uZGVyIHdvbWFuIiwicm9sZSI6InN0dWRlbnQiLCJzY2hvb2wiOiJDU1VMQiJ9.In7UisIgih29XN30yGGMlEw9ySHbjGlFrVbjD_HAKg0");

                    router.push({ name: "HomePage" });

                }
                else if (this.username == "dpatel" && this.otp == "123measd") {
                    window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiRGV2YXJzaCBQYXRlbCIsInVzZXJuYW1lIjoiZHBhdGVsIiwiZW1haWwiOiJkZXZhcnNoLnBhdGVsQHN0dWRlbnQuY3N1bGIuZWR1IiwicGFzc2NvZGUiOiJwaXp6YSBndXkiLCJyb2xlIjoic3R1ZGVudCIsInNjaG9vbCI6IkNTVUxCIn0.qwmUc9bhYScOvskSxu0J8KX39tsKJzVGROW-j-F46hI");

                    router.push({ name: "HomePage" });

                }
                else {
                    alert("Incorect username/otp");
                }
            }
        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>