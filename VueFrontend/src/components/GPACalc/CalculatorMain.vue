<template>
    <div class="post">
        <div v-if="loading" class="loading">
            GPA/GRADE CALCULATOR !!!!!
            <br />
        </div>
        <!-- Warning Error for if something goes wrong -->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- Buttons to go to different pages -->
        <div>
            <button @click="grade">Calculate Grade</button>
            <button @click="gpa">Calculate GPA</button>
            <button @click="display">Class Rankings</button>
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
        //props: ['match'],
        //components: {
            
        //},
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
            // Routes to grade calc
            grade() {
                router.push({name: "gradeCalc"})
            },
            // Routes to gpa calc
            gpa() {

                router.push({name: "gpaCalc"})
            },

            // Routes to display rankings
            display() {
                router.push({ name: "displayRankings" })
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