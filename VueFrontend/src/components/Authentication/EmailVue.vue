<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Welcome! Please enter email and passcode!
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
                if (this.email == "audrey.brio@student.csulb.edu" && this.passcode == "hello world") {
                    router.push({name: "LoginVue"});

                }
                else if (this.email == "bradley.nickle@student.csulb.edu" && this.passcode == "marvel fan") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "joseph.cutri@student.csulb.edu" && this.passcode == "number one") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "albert.toscano01@student.csulb.edu" && this.passcode == "happy meal") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "jacob.delgado01@student.csulb.edu" && this.passcode == "super man") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "szeman.tang@student.csulb.edu" && this.passcode == "wonder woman") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "devarsh.patel@student.csulb.edu" && this.passcode == "pizza guy") {
                    router.push({ name: "LoginVue" });

                }
                else {
                    alert("Incorect email/passcode");
                }
            },

            skip() {
                window.sessionStorage.setItem("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQXVkcmV5IEJyaW8iLCJ1c2VybmFtZSI6ImFicmlvIiwiZW1haWwiOiJhdWRyZXkuYnJpb0BzdHVkZW50LmNzdWxiLmVkdSIsInBhc3Njb2RlIjoiaGVsbG8gd29ybGQiLCJyb2xlIjoiYWRtaW4iLCJzY2hvb2wiOiJDU1VMQiJ9.O9qyhghpLVvFIurYuDaAzLV6r9HVpO0DrXBhTbB-3Yo");
                router.push({ name: "HomePage" })
            }

        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>