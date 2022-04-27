<template>
    <h> Hello </h>
    <div class="recipe-view" v-for="recipe in recipes" :key="recipe.id">
        <p> {{ recipe.id}} </p>
    </div>
    <router-view />
</template>
<script>
    export default {
        name: 'RecipeView',
        data() {
            return {
                recipes:
                    [
                        {
                            id: 1,
                            title: "Chicken Alfredo Pasta",
                            category: "dinner",
                            calorieValue: "144 calories",
                            overallPrice: "$18.00",
                            datePosted: "4/24/2022",
                            mealForPeople: 2,
                            description: "Chicken Alfredo Pasta is awasome food."
                        },
                        {
                            id: 2,
                            title: "Beef Alfredo Pasta",
                            category: "dinner",
                            calorieValue: "144 calories",
                            overallPrice: "$18.00",
                            datePosted: "4/24/2022",
                            mealForPeople: 1,
                            description: "Beef Alfredo Pasta is awasome food."
                        },
                        {
                            id: 3,
                            title: "Shrimp Alfredo Pasta",
                            category: "dinner",
                            calorieValue: "156 calories",
                            overallPrice: "$21.00",
                            datePosted: "4/28/2022",
                            mealForPeople: 1,
                            description: "Shrimp Alfredo Pasta is awasome food."
                        },
                        {
                            id: 4,
                            title: "Ramen noodle soup",
                            category: "Dinner",
                            calorieValue: "150 calorie",
                            overallPrice: "$22.22",
                            datePosted: "4/22/2022",
                            mealForPeople: 1,
                            description: "A delicious curry made with chiicken, potatoe, and a special sauce"
                        }
                    ]
            };
        },
    }
</script>
    <!--
<script>
    import * as $ from 'jquery'

    export default ({
        data() {
            return {
                recipes: []
            }
        },
        mounted: function () {
            this.getRecipes();
        },
        methods: {
            getRecipes() {
                //URLS.api.recipeBuilder.getRecipe()
                $.ajax({
                    method: "GET",
                    url: "https://localhost:5003/api/recipe/getRecipe",
                    dataType: "application/json"
                })
                    .done(function (data) {
                        this.recipes = data
                        console.log(this.recipes)
                    }).catch(function (error) {
                        console.log(error)
                    });

            }
        }
    })
</script>-->

<style scoped>
    .recipe-view {
        padding: 20px;
        width: 300px;
        cursor: pointer;
        border: 1px solid #39495c;
        margin-bottom: 18px;
    }

        .recipe-view:hover {
            transform: scale(1.01);
            box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2);
        }
</style>
