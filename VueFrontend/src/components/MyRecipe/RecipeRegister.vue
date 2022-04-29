<template>
    <div class="new">
        <h2> ADD NEW RECIPE</h2>
        <div class="newrecipe">

            <form @submit.prevent ="onCreatePost">
                <div class="group">
                    <label>Title</label>
                    <input type="text" placeholder="Recipe Name" v-model="title" />
                </div>
                <div class="group">
                    <label>Category</label>
                    <input type="text" placeholder="Category (vegetarian, vegan, breakfast, dinner, etc�)" v-model="category" />
                </div>
                <div class="group">
                    <label>Calorie Value</label>
                    <input type="text" placeholder="Total Calorie of your recipe" v-model="calorieValue"/>

                </div>
                <div class="group">
                    <label>Overall Price</label>
                    <input type="text" placeholder="Total cost of recipe price" v-model="overallPrice" />

                </div>
                <div class="group">
                    <label>Date</label>
                    <input type="text" placeholder="Today is Date" v-model="datePosted" />

                </div>
                <div class="group">
                    <label>Meal for People</label>
                    <input type="text" placeholder="Enter number of people can eat this recipe" v-model="mealForPeople"/>

                </div>
                <div class="group">
                    <label>Step Description</label>
                    <textarea type="text" placeholder="Describe your steps of recipe less than 5000 characters." v-model="description"></textarea>
                </div>
                <button @click="add"> ADD NEW RECIPE</button>

                <button @click="close"> CLOSE </button>
            </form>

        </div>
    </div>
    <router-view />
</template>


<script>

    import RecipesService from '/src/variables/RecipesService.js'

    export default {
        data() {
            return {

                title: '',
                category: '',
                calorieValue:'',
                overallPrice:'',
                datePosted:'',
                mealForPeople:'',
                description:'',
            }
        },
        methods: {
            onCreatePost() {
                    RecipesService.postRecipe({
                        title: this.title, category: this.category, calorieValue: this.calorieValue, overallPrice: this.overallPrice,
                        datePosted: this.datePosted, mealForPeople: this.mealForPeople, description: this.description
                    })
                .then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });

                this.$router.push({
                    name: 'RecipeView',
                })
            },
            close() {

                this.$router.push({
                    name: 'RecipeView',
                })

            }
        }
    }
</script>


<style scoped>
    .new{
        align-content:center;
        display:flex;
        flex-direction:column
    }
    .newrecipe {
        background-color: #0f79f1;
        padding: 2rem;
        border-radius: 2rem;
    }
       
        .newrecipe h2 {
            font-size: 2rem;
            margin-bottom: 1rem;
        }

        .newrecipe .group {
            margin-bottom: 1rem;
        }

            .newrecipe .group label {
                display: block;
                margin-bottom: 0.5rem;
            }

            .newrecipe .group input,
            .newrecipe .group textarea {
                display: block;
                width: 100%;
                padding: 0.5rem;
                border: 1px solid #ccc;
                border-radius: 5px;
                margin-bottom: 1rem;
            }

            .newrecipe .group textarea {
                height: 100px;
                resize: none;
            }

    .newrecipe .button[type="add"] {
        padding-right: 10em;
    }
        .newrecipe .button[type="close"] {

            padding-left: 10em;
        }
</style>