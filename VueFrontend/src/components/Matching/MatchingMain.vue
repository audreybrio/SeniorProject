<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Matching !!!!!
            <br />
        </div>
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>
        <button class="ui button big toggle"
                :class="{active:isActive}"
                @click="toggle">
            {{isActive ? 'YOU ARE OPTED IN OF MATCING' : 'YOU ARE OPTED OUT OF MATCHING'}}
        </button>

        <div>
            <button @click="activity">Activity Profile</button>
            <button @click="tutoring">Tutoring Profile</button>
            <button @click="generateMatches">Generate Matches</button>
        </div>
        <button @click="onSubmit">Return to Homepage</button>

    </div>
    <router-view />
</template>

<script lang="js">
    import router from '@/router'
    import jwt_decode from "jwt-decode"
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                isActive: true,
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

                //fetch('https://studentmultitool.me:5001/weatherforecast')
                //    .then(r => r.json())
                //    .then(json => {
                //        this.post = json;
                //        this.loading = false;
                //        return;
                //    });

            },
            activity() {
                router.push({name: "activityProfile"})
            },

            tutoring() {
                router.push({name: "tutoringProfile"})
            },

            onSubmit() {
                router.push({name: "HomePage"})
            },

            toggle() {
                this.isActive = !this.isActive;
                $.ajax({
                    url: `${baseURL}/api/matching/updateOptStatus/${jwt_decode(window.sessionStorage.getItem("token")).username}/${this.isActive}`,
                    context: this,
                    method: 'GET',
                    success: function () {
                        console.log("ajax success");

                    },
                    error: function () {
                        console.log("error");
                        this.errors = "No Profile Created";
                    }
                })
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