<template>
    <div class="rankings">
        <!-- Child used to display list of matches -->

        <input id="course" name="course" v-model="course" placeholder="Course">
        <button @click="generateRankings"> Generate Rankings</button>
        <!--<MatchesChild v-for="match in matches" :key="match.match" :match="match">
    <h5>{{match.match}}</h5>-->
        <!--<h5>{{match.reason}}</h5>
    <h5>{{match.overlap}}</h5>-->
        <!--</MatchesChild>-->
    </div>

    <div>
        <button @click="onSubmit"> Return</button>

    </div>
    <router-view />
</template>

<script>
    // Imports
    //import axios from "axios"
    //import jwt_decode from "jwt-decode"
    //import MatchesChild from '../Matching/MatchesChild.vue'
    //import URLS from '../../variables'



    export default ({
        //name: 'displayMatches',
        //props: ['match'],
        //components: {
        //    MatchesChild
        //},
        data() {
            return {
                post: null,
                // matches: [],
                course: null,
            }
        },
        // Other attempt
        //mounted() {
        //    fetch('https://localhost:5002/api/matching/displayMatches')
        //        .then((res) => res.json())
        //        .then(data => this.matches = data)
        //        .catch(err => console.log(err.message))
        //},
        created() {

        },
        computed: {
        },

        // Returns to main matching page
        methods: {
            onSubmit() {
                this.$router.push({name: 'calculatorMain'})
            },

            generateRankings() {
                //let username = jwt_decode(window.sessionStorage.getItem("token")).username
                //axios.get(`${URLS.api.matching.displayMatches}/${username}`)
                //    .then(response => this.matches = response.data);
                //console.log(this.matches);
            }

        }

    })
</script>

<style scoped>
    .rankings {
        display: flex;
        flex-direction: column;
        align-items: center;
    }


    .d1 {
        width: 20px;
        position: absolute;
        align-content: center;
        margin-top: 10px;
    }
</style>