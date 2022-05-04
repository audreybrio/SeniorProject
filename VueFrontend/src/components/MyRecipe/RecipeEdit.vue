<template>
    <div class="edit">
        <h2> Edit your recipe details</h2>

        <div class="editrecipe">

            <form @submit.prevent="onEditPost">
                <div class="group">
                    <label>Title</label>
                    <input type="text" v-model="title" />
                </div>
                <div class="group">
                    <label>Category</label>
                    <input type="text" v-model="category" />
                </div>
                <div class="group">
                    <label>Calorie Value</label>
                    <input type="text" v-model="calorieValue" />

                </div>
                <div class="group">
                    <label>Overall Price</label>
                    <input type="text" v-model="overallPrice" />

                </div>
                <div class="group">
                    <label>Date</label>
                    <input type="text" v-model="datePosted" />

                </div>
                <div class="group">
                    <label>Meal for People</label>
                    <input type="text" v-model="mealForPeople" />

                </div>
                <div class="group">
                    <label>Step Description</label>
                    <textarea type="text" v-model="description"></textarea>
                </div>
                <button @click="add"> EDIT RECIPE</button>
                <button @click="close"> CLOSE </button>
            </form>

        </div>
    </div>
</template>


<script>
    import AccessService from '/src/variables/AccessService.js'

	export default {
        props: ['recipe'],
        data() {
            return {
                title: this.recipe.title,
                category: this.recipe.category,
                calorieValue: this.recipe.calorieValue,
                overallPrice: this.recipe.overallPrice,
                datePosted: this.recipe.datePosted,
                mealForPeople: this.recipe.mealForPeople,
                description: this.recipe.description,
            }
        },
        methods: {
            onEditPost() {

               
                AccessService.editRecipe(this.recipe.id, {
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
            }
        }


    }
</script>


<style scoped>

    .edit {
        align-content: center;
        display: flex;
        flex-direction: column
    }
    .editrecipe {
        background-color: #0f79f1;
        padding: 2rem;
        border-radius: 2rem;
    }

        .editrecipe h2 {
            font-size: 2rem;
            margin-bottom: 1rem;
        }

        .editrecipe .group {
            margin-bottom: 1rem;
        }

            .editrecipe .group label {
                display: block;
                margin-bottom: 0.5rem;
            }

            .editrecipe .group input,
            .editrecipe .group textarea {
                display: block;
                width: 100%;
                padding: 0.5rem;
                border: 1px solid #ccc;
                border-radius: 5px;
                margin-bottom: 1rem;
            }

            .editrecipe .group textarea {
                height: 100px;
                resize: none;
            }

    button[type="submit"] {
        margin-right: 1rem;
    }

    .editrecipe button[type="button"] {
        margin-right: 1rem;
    }
</style>