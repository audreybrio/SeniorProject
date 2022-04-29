<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Matching !!!!!
            <br />
        </div>
        <!-- Warning Error for if something goes wrong -->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>
        <!-- Toggle button to opt in and opt of matching -->
        <button class="ui button big toggle"
                :class="{active:isActive}"
                @click="toggle">
            {{isActive ? 'YOU ARE OPTED IN OF MATCING' : 'YOU ARE OPTED OUT OF MATCHING'}}
        </button>
        <!-- Buttons to go to different pages -->
        <div>
            <button @click="activity">Activity Profile</button>
            <button @click="tutoring">Tutoring Profile</button>
        </div>
        <div>
            <button @click="generateActivityMatches">Generate Activity Matches</button>
            <button @click="generateTutoringMatches">Generate Tutoring Matches</button>
            <button @click="displayMatches">Display Matches</button>
        </div>

        <button @click="onSubmit">Return to Homepage</button>

    </div>
    <router-view />
</template>

<script lang="js">
    // Imports
    import router from '@/router'
    import jwt_decode from "jwt-decode"
    import URLS from '../../variables'

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
            this.fetchData();
        },
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

            },
            // Routes to activity profile
            activity() {
                router.push({name: "activityProfile"})
            },
            // Routes to tutoring profile
            tutoring() {
                router.push({name: "tutoringProfile"})
            },
            //  Returns to home page 
            onSubmit() {
                router.push({name: "HomePage"})
            },
            // Changes a users opt in/opt out status
            toggle() {
                this.isActive = !this.isActive;
                //$.ajax({
                //    url: `${URLS.api.matching.updateOptStatus}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${this.isActive}`,
                //    context: this,
                //    method: 'GET',
                //    success: function () {
                //        console.log("ajax success");

                //    },
                //    error: function () {
                //        console.log("error");
                //        this.errors = "No Profile Created";
                //    }
                //})

                fetch(
                    `${URLS.api.matching.updateOptStatus}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${this.isActive}`, {
                    method: 'GET',
                    context: this,
                    headers: {
                        'Accept': 'application/json',
                        //    'Content-Type': 'application/json'
                    },

                }).then(() => { })
                  .catch(() => { this.errors = "You didnt create a profile" });
            },
            // Goes to display matches page 
            displayMatches() {

                router.push({ name: "displayMatches" })

            },
            // Generates the tutoring Matches
            generateTutoringMatches() {
                fetch(
                    `${URLS.api.matching.matchTutoring}/${jwt_decode(window.sessionStorage.getItem("token")).username}`, {
                        method: 'GET',
                        context: this,
                    headers: {
                        'Accept': 'application/json',
                    //    'Content-Type': 'application/json'
                    },

                }).then(() => { this.errors = "SUCCESS" })
                    .catch(() => { this.errors = "You didnt create a profile" });
            },
            // Generates the activity matches 
            generateActivityMatches() {
                fetch(
                    `${URLS.api.matching.matchActivity}/${jwt_decode(window.sessionStorage.getItem("token")).username}`, {
                    method: 'GET',
                    context: this,
                    headers: {
                        'Accept': 'application/json',
                        //    'Content-Type': 'application/json'
                    },

                }).then(() => { this.errors = "SUCCESS" })
                    .catch(() => { this.errors = "You didnt create a profile" });
            }
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