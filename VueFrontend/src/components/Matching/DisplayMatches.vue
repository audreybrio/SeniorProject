<template>
    <h1> You've been matched!</h1>
    <div class="matches">
        <!-- Child used to display list of matches -->
        <MatchesChild v-for="match in matches" :key="match.match" :match="match">
            <h5>{{match.match}}</h5>
            <!--<h5>{{match.reason}}</h5>
            <h5>{{match.overlap}}</h5>--> 
        </MatchesChild>
    </div>

    <div>
        <button @click="previous">Previous</button>
        <button @click="onSubmit"> Return</button>
        <button @click="next">Next</button>

    </div>
    <router-view />
</template>

<script>
    // Imports 
    import axios from "axios"
    import jwt_decode from "jwt-decode"
    import MatchesChild from '../Matching/MatchesChild.vue'
    import URLS from '../../variables'



    export default ({
        name: 'displayMatches',
        props: ['match'],
        components: {
            MatchesChild
        },
        data() {
            return {
                post: null,
                matches: [],  
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
            // Gets list of matches from backend to display 
            let username = jwt_decode(window.sessionStorage.getItem("token")).username
            axios.get(`${URLS.api.matching.displayMatches}/${username}`)
                .then(response => this.matches = response.data);
            console.log(this.matches);

        },
        computed: {
        },

        // Returns to main matching page 
        methods: {
            onSubmit() {
                this.$router.push({name: 'matchingMain'})
            },

        }

    })
</script>

<style scoped>
    .matches {
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