<template>
    <h1> You've been matched!</h1>
    <div class="matches">
        <MatchesChild v-for="match in matches" :key="match.match" :match="match">
            <h5>{{match.match}}</h5>
            <!--<h5>{{match.reason}}</h5>
            <h5>{{match.overlap}}</h5>--> 
        </MatchesChild>

        <!--<div class="pagination">

            <router-link id="page-prev" :to="{ name: 'displayMatches', query: {page: page - 1}}"
                         rel="prev" v-if="page != 1"><button>&#60;Previous Page</button></router-link>
            <router-link id="page-next" :to="{ name: 'displayMatches', query: {page: page + 1}}"
                         rel="next" v-if="hasNextPage"><button>Next Page &#62; </button></router-link>
        </div>-->
    </div>

        <div>
            <button @click="onSubmit"> Return</button>

        </div>
    <router-view />
</template>

<script>
    // import { watchEffect } from 'vue'
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
        //mounted() {
        //    fetch('https://localhost:5002/api/matching/displayMatches')
        //        .then((res) => res.json())
        //        .then(data => this.matches = data)
        //        .catch(err => console.log(err.message))
        //},
        created() {

            let username = jwt_decode(window.sessionStorage.getItem("token")).username
            axios.get(`${URLS.api.matching.displayMatches}/${username}`)
                .then(response => this.matches = response.data);
            console.log(this.matches);

        },
        computed: {
            //hasNextPage() {

            //    return this.totalRecipes >= 4
            //}
        },
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

/*    .pagination {
        display: flex;
        width: 290px;
    }

        .pagination a {
            flex: 1;
            text-decoration: none;
            color: #2c3e50;
        }

    #page-prev {
        text-align: left;
    }

    #page-next {
        text-align: right;
    }*/

    .d1 {
        width: 20px;
        position: absolute;
        align-content: center;
        margin-top: 10px;
    }
</style>