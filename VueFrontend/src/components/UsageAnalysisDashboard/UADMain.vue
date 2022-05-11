<template>
    <div class="post">
        <div v-if="loading" class="loading">
            USAGE ANALYSIS DASHBOAD !!!!!
            <br />
        </div>
        <!-- Warning Error for if something goes wrong -->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- Buttons to see different graphs -->
        <div>
            <button @click="mostVisited">Most Visited Pages</button>
            <button @click="topSchool">Top Schools</button>
            <button @click="longestView">Average Duration</button>
        </div>
        <div>
            <button @click="numLogin">Logins/Day</button>
            <button @click="numMatches">Matches/Day</button>
            <button @click="numRegister">Registrations/Day</button>
        </div>

        <!-- Most Visited Graph-->
        <div v-if="most == true">
            <MostVisited :count="this.topView.count" :name="this.topView.name" />
        </div>
        <!-- top School Graph-->
        <div v-if="school == true">
            <TopSchool :count="this.schoolTop.count" :name="this.schoolTop.name" />
        </div>
        <!-- Aveage Duration Graph-->
        <div v-if="duration == true">
            <AverageDuration :count="this.avgDur.count" :name="this.avgDur.name" />
        </div>

        <!-- Number of Logins Graph-->
        <div v-if="login == true">
            <NumLogin :count="this.numLog.count" :name="this.numLog.days" />
        </div>
        <!-- Number of Matches Graph-->
        <div v-if="matches == true">
            <NumMatches :count="this.numMat.count" :name="this.numMat.days" />
        </div>
        <!-- Number of Registrations Graph-->
        <div v-if="register == true">
            <NumRegister :count="this.numReg.count" :name="this.numReg.days" />
        </div>

        <br />

        <button @click="onSubmit">Return to Homepage</button>

    </div>
    <router-view />
</template>

<script lang="js">
    // Imports
    import router from '@/router'
    import jwt_decode from "jwt-decode"
    import URLS from '../../variables'
    import MostVisited from '../UsageAnalysisDashboard/MostVisited.vue'
    import TopSchool from '../UsageAnalysisDashboard/TopSchool.vue'
    import AverageDuration from '../UsageAnalysisDashboard/AverageDuration.vue'
    import NumLogin from '../UsageAnalysisDashboard/NumLogin.vue'
    import NumMatches from '../UsageAnalysisDashboard/NumMatches.vue'
    import NumRegister from '../UsageAnalysisDashboard/NumRegister.vue'

    export default ({
        props: ['name','count'],
        components: {
            MostVisited,
            TopSchool,
            AverageDuration,
            NumLogin,
            NumMatches,
            NumRegister
        },
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                errors: "",
                most: false, login: false, matches: false,
                school: false, duration: false, register: false,
                topView: null, numLog: null, numMat: null,
                schoolTop: null, avgDur: null, numReg: null,

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
            // Graph for Most Visited
            mostVisited() {
                this.errors = ""
                this.most = true; this.login = false;
                this.school = false; this.duration = false;
                this.register = false; this.matches = false;
                fetch(
                    `${URLS.api.uad.mostVisited}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                    }
                    else {
                        console.log("ajax success")
                        return response.json()

                    }
                }).then((result) => {
                    console.log(result)
                    // this.topView = JSON.stringify(result);
                    this.topView = result
                    if (this.topView != undefined) {
                        let count = this.topView.count
                        let name = this.topView.name
                        console.log("topView", this.topView)
                        console.log("topView name", name)
                        console.log("topView count", count)
                    }
                    else {
                        console.log("error")
                    }

                })
                
            },
            // Graph for Top School
            topSchool() {

                this.errors = ""
                this.most = false; this.duration = false;
                this.school = true; this.login = false;
                this.register = false; this.matches = false;

                fetch(
                    `${URLS.api.uad.topSchool}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                    }
                    else {
                        console.log("ajax success")
                        return response.json()

                    }
                }).then((result) => {
                    console.log(result)
                    // this.topView = JSON.stringify(result);
                    this.schoolTop = result
                    if (this.schoolTop != undefined) {
                        let count = this.schoolTop.count
                        let name = this.schoolTop.name
                        console.log("topShool", this.schoolTop)
                        console.log("topView name", name)
                        console.log("topView count", count)
                    }
                    else {
                        console.log("error")
                    }

                })

            },

            // Graph for Average Duration
            longestView() {
                this.errors = ""
                this.most = false; this.school = false;
                this.duration = true; this.login = false;
                this.register = false; this.matches = false;

                fetch(
                    `${URLS.api.uad.averageDuration}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                    }
                    else {
                        console.log("ajax success")
                        return response.json()

                    }
                }).then((result) => {
                    console.log(result)
                    // this.topView = JSON.stringify(result);
                    this.avgDur = result
                    if (this.avgDur != undefined) {
                        let count = this.avgDur.count
                        let name = this.avgDur.name
                        console.log("topShool", this.avgDur)
                        console.log("topView name", name)
                        console.log("topView count", count)
                    }
                    else {
                        console.log("error")
                    }

                })
            },

            // Graph for Number of Logins/Day
            numLogin() {
                this.errors = ""
                this.most = false; this.school = false;
                this.duration = false; this.login = true;
                this.register = false; this.matches = false;

                fetch(
                    `${URLS.api.uad.numLogin}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                    }
                    else {
                        console.log("ajax success")
                        return response.json()

                    }
                }).then((result) => {
                    console.log(result)
                    this.numLog = result
                    if (this.numLog != undefined) {
                        let count = this.numLog.count
                        let name = this.numLog.days
                        console.log("topShool", this.numLog)
                        console.log("topView days", name)
                        console.log("topView count", count)
                    }
                    else {
                        console.log("error")
                    }

                })
            },
            // Graph for Number of Matches/Day
            numMatches() {

                this.errors = ""
                this.most = false; this.school = false;
                this.duration = false; this.login = false;
                this.register = false; this.matches = true;

                fetch(
                    `${URLS.api.uad.numMatches}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                    }
                    else {
                        console.log("ajax success")
                        return response.json()

                    }
                }).then((result) => {
                    console.log(result)
                    this.numMat = result
                    if (this.numMat != undefined) {
                        let count = this.numMat.count
                        let name = this.numMat.days
                        console.log("topShool", this.numMat)
                        console.log("topView days", name)
                        console.log("topView count", count)
                    }
                    else {
                        console.log("error")
                    }

                })
            },

            // Graph for number of registrations/day
            numRegister() {
                this.errors = ""
                this.most = false; this.school = false;
                this.duration = false; this.login = false;
                this.register = true; this.matches = false;

                fetch(
                    `${URLS.api.uad.numRegister}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },

                }).then((response) => {
                    if (!response.ok) {

                        console.log("error")
                    }
                    else {
                        console.log("ajax success")
                        return response.json()

                    }
                }).then((result) => {
                    console.log(result)
                    this.numReg = result
                    if (this.numReg != undefined) {
                        let count = this.numReg.count
                        let name = this.numReg.days
                        console.log("topShool", this.numReg)
                        console.log("topView days", name)
                        console.log("topView count", count)
                    }
                    else {
                        console.log("error")
                    }

                })
            },

            //  Returns to home page
            onSubmit() {
                router.push({name: "HomePage"})
            },

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