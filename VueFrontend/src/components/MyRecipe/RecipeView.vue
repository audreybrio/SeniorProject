<template>
    <h1> List of Recipes</h1>

    <div id="d1">
        <button @click="register"> ADD NEW RECIPE</button>

    </div>

   <li></li>
    <div class="recipes">
        <RecipeCard v-for="recipe in recipes" :key="recipe.id" :recipe="recipe" />
        <div class="pagination">

            <router-link id="page-prev" :to="{ name: 'RecipeView', query: {page: page - 1}}"
                         rel="prev" v-if="page != 1"><button>&#60;Previous Page</button></router-link>
            <router-link id="page-next" :to="{ name: 'RecipeView', query: {page: page + 1}}"
                         rel="next" v-if="hasNextPage"><button>Next Page &#62; </button></router-link>
        </div>
    </div>
    <router-view />
</template>


<script>
    import RecipeCard from '../MyRecipe/RecipeCard.vue'
    import AccessService from '/src/variables/index.js'
    import {watchEffect } from 'vue'

    export default ({
        name: 'RecipeView',
        props: ['page'],
        components: {
            RecipeCard
        },
        data() {
            return {
                recipes: null,
                totalRecipes: 0
            }
        },
        created() {

            watchEffect(() => {
                this.recipes = null
                AccessService.getRecipes( 4, this.page)
                .then((response) => {
                    this.recipes = response.data
                    //this.totalRecipes = response.headers['x-total-count']
                    this.totalRecipes = this.recipes.length
                    //console.log('Recipes', this.recipes.length)
                })
                .catch((error) => {
                     console.log(error)
                })
            })

        },
        computed: {

            hasNextPage() {
               
                return this.totalRecipes >= 4
            }

        },
        methods: {
            register() {

                this.$router.push({
                    name: 'RecipeRegister',
                })

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

    .pagination a{
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
