<template>
    <div class="post">


        <h1> You've been matched!'</h1>


        <!--<li></li>
    <div class="recipes">
        <MatchesChild v-for="recipe in recipes" :key="recipe.id" :recipe="recipe" />
        <div class="pagination">

            <router-link id="page-prev" :to="{ name: 'RecipeView', query: {page: page - 1}}"
                         rel="prev" v-if="page != 1"><button>&#60;Previous Page</button></router-link>
            <router-link id="page-next" :to="{ name: 'RecipeView', query: {page: page + 1}}"
                         rel="next" v-if="hasNextPage"><button>Next Page &#62; </button></router-link>
        </div>
    </div>-->

        <div id="d1">
            <button @click="onSubmit"> Return</button>

        </div>
    </div>
    <router-view />
</template>

<!--<template>
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
            <button @click="displayeMatches">Display Matches</button>
        </div>
        <button @click="onSubmit">Return to Homepage</button>

    </div>
    <router-view />
</template>-->

<script>

    //import {watchEffect } from 'vue'
    export default ({
        //name: 'RecipeView',
        //props: ['page'],
        //components: {
        //    RecipeCard
        //},
        data() {
            return {
                post: null
                //recipes: null,
                //totalRecipes: 0
            }
        },
        created() {
            //watchEffect(() => {
            //    this.recipes = null
            //    RecipesService.getRecipes( 4, this.page)
            //    .then((response) => {
            //        this.recipes = response.data
            //        //this.totalRecipes = response.headers['x-total-count']
            //        this.totalRecipes = this.recipes.length
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