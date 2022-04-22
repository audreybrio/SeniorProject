<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Matching !!!!!
        </div>
        <div>
            <button @click="activity">Activity Profile</button>
            <button @click="tutoring">Tutoring Profile</button>
        </div>
        <button @click="onSubmit">Return to Homepage</button>

    </div>
    <router-view />
</template>

<script lang="js">
    import router from '@/router'
    import jwt_decode from "jwt-decode"

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username
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

                fetch('https://studentmultitool.me:5001/weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });

            },
            activity() {
                router.push({name: "activityProfile"})
            },

            tutoring() {
                router.push({name: "tutoringProfile"})
            },

            onSubmit() {
                router.push({name: "HomePage"})
            }


        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>