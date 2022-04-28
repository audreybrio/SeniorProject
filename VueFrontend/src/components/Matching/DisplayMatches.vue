<template>
    <h1> You've been matched!'</h1>


    <li></li>
    <div class="matches">
        <MatchesChild v-for="match in match" :key="match.id" :match="match" />
        <div class="pagination">

            <router-link id="page-prev" :to="{ name: 'displayMatches', query: {page: page - 1}}"
                         rel="prev" v-if="page != 1"><button>&#60;Previous Page</button></router-link>
            <router-link id="page-next" :to="{ name: 'displayMatches', query: {page: page + 1}}"
                         rel="next" v-if="hasNextPage"><button>Next Page &#62; </button></router-link>
        </div>
    </div>

        <div id="d1">
            <button @click="onSubmit"> Return</button>

        </div>
    <router-view />
</template>

<script>
    import MatchesChild from '../Matching/MatchesChild.vue'
    // import { watchEffect } from 'vue'

    export default ({
        name: 'displayMatches',
        props: ['page'],
        components: {
            MatchesChild
        },
        data() {
            return {
                post: null,
                matches: null,
                //totalRecipes: 0
            }
        },
        created() {
            //watchEffect(() => {
            //    this.matches = null
            //    RecipesService.getRecipes( 4, this.page)
            //    .then((response) => {
            //        this.recipes = response.data
            //        //this.totalRecipes = response.headers['x-total-count']
            //        // this.totalRecipes = this.recipes.length
            //        //console.log('Recipes', this.recipes.length)
            //    })
            //    .catch((error) => {
            //         console.log(error)
            //    })
            //})
        },
        computed: {
            //hasNextPage() {

            //    return this.totalRecipes >= 4
            //}
        },
        methods: {
            onSubmit() {
                this.$router.push({name: 'matchingMain'})
            }
        }

    })
</script>

<style scoped>
    .recipes {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .pagination {
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
    }

    .d1 {
        width: 20px;
        position: absolute;
        align-content: center;
        margin-top: 10px;
    }
</style>