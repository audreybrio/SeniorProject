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
            {{isActive ? 'YOU ARE OPTED IN OF MATCHING' : 'YOU ARE OPTED OUT OF MATCHING'}}
        </button>
        <!-- Buttons to go to different pages -->
        <div>
            <button @click="activity">Activity Profile</button>
            <button @click="tutoring">Tutoring Profile</button>
        </div>
        <div>
            <button @click="generateActivityMatches">Generate Activity Matches</button>
            <button @click="generateTutoringMatches">Generate Tutoring Matches</button>
            <button @click="displayMatches">Display All Matches</button>
        </div>

        <div>
            <MatchesChild v-for="match in matches" :key="match.match" :match="match">
                <h5>{{match.match}}</h5>
            </MatchesChild>
        </div>


        <div>
            <MatchesChild v-for="match in matchesTutoring" :key="match.match" :match="match">
                <h5>{{match.match}}</h5>
            </MatchesChild>
        </div>


        <button @click="onSubmit">Return to Homepage</button>

    </div>
    <router-view />
</template>

<script lang="js">
    // Imports
    import router from '@/router'
    import axios from "axios"
    import jwt_decode from "jwt-decode"
    import MatchesChild from '../Matching/MatchesChild.vue'
    import URLS from '../../variables'

    export default ({
        props: ['match'],
        components: {
            MatchesChild
        },
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                isActive: true,
                errors: "",
                matches: [],
                matchesTutoring: [],
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
                let isValid = jwt_decode(window.sessionStorage.getItem("token")).isValid
                if (isValid == true) {
                    console.log("valid token")
                }
                router.push({name: "activityProfile"})
            },
            // Routes to tutoring profile
            tutoring() {
                let isValid = jwt_decode(window.sessionStorage.getItem("token")).isValid
                if (isValid == true) {
                    console.log("valid token")
                }
                router.push({name: "tutoringProfile"})
            },
            //  Returns to home page 
            onSubmit() {
                router.push({name: "HomePage"})
            },
            // Changes a users opt in/opt out status
            toggle() {
                this.isActive = !this.isActive;
                fetch(
                    `${URLS.api.matching.updateOptStatus}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${this.isActive}`, {
                    method: 'GET',
                    context: this,
                    headers: {
                        'Accept': 'application/json',
                        //    'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                        this.errors = "No Profile Created";
                    }
                    else {
                        console.log("success")
                    }
                })
            },
            // Goes to display matches page 
            displayMatches() {

                router.push({ name: "displayMatches" })

            },
            // Generates the tutoring Matches
            generateTutoringMatches() {
                //fetch(
                //    `${URLS.api.matching.matchTutoring}/${jwt_decode(window.sessionStorage.getItem("token")).username}`, {
                //        method: 'GET',
                //        context: this,
                //    headers: {
                //        'Accept': 'application/json',
                //        'Content-Type': 'application/json'
                //    },

                //}).then((response) => {
                //    if (!response.ok) {

                //        console.log("error")
                //        this.errors = "No Profile Created";
                //    }
                //    else {
                //        console.log("success")
                //        this.errors = "SUCCESS"
                //    }
                //})
                this.matches = []
                let username = jwt_decode(window.sessionStorage.getItem("token")).username
                axios.get(`${URLS.api.matching.matchTutoring}/${username}`)
                    .then(response => this.matchesTutoring = response.data);
                if (this.matchesTutoring == null) {
                    this.errors = "No Matches";
                }
                console.log(this.matchesTutoring);
            },
            // Generates the activity matches 
            generateActivityMatches() {
                //fetch(
                //    `${URLS.api.matching.matchActivity}/${jwt_decode(window.sessionStorage.getItem("token")).username}`, {
                //    method: 'GET',
                //    context: this,
                //    headers: {
                //        'Accept': 'application/json',
                //            'Content-Type': 'application/json'
                //    },

                //}).then((response) => {
                //    if (!response.ok) {

                //        console.log("error")
                //        this.errors = "No Profile Created";
                //    }
                //    else {
                //        console.log("success")
                //        this.errors = "SUCCESS"
                //    }
                //})
                this.matchesTutoring = []
                let username = jwt_decode(window.sessionStorage.getItem("token")).username
                axios.get(`${URLS.api.matching.matchActivity}/${username}`)
                    .then(response => this.matches = response.data);
                if (this.matches == null) {
                    this.errors = "No Matches";
                }
                console.log(this.matches);
            }
        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }

    .post {
        display: flex;
        flex-direction: column;
        align-items: center;
        align-content: center;
    }

    .d1 {
        width: 20px;
        position: absolute;
        align-content: center;
        margin-top: 10px;
    }
    .warning {
        color: red;
        margin: auto;
        width: 440px;
        text-align: center;
        font-size: 11px;
    }
</style>