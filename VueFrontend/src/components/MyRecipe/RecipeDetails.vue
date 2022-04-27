<template>
	<div v-if="recipe">
		<h4> {{recipe.id}} for {{recipe.title}} </h4>
		<p> {{ recipe.category }} </p>
		<p> {{ recipe.calorieValue }} </p>
		<p> {{ recipe.overallPrice }} </p>
		<p>{{ recipe.datePosted }} </p>
		<p> {{ recipe.mealForPeople }}</p>
		<p>{{ recipe.description }}</p>
	</div>
</template>


<script>
	export default {
		props: ['recipe']
    }
</script>


<!--<script>
    import RecipesService from '/src/services/RecipesService.js'
	export default {
		props: ['id'],
		data() {
			return {
				recipe: null
			}
		},
		created() {
			RecipesService.getRecipe(this.id)
				.then((response) => {
                    this.recipe = response.data
                    //console.log('recipe', this.recipe)
                })
                .catch((error) => {
                    console.log(error);
                });


		}
		
	}
</script>

<style scoped>
    .recipe {
        padding: 20px;
        width: 250px;
        cursor: pointer;
        border: 1px solid #39495c;
        margin-bottom: 18px;
    }

        .recipe:hover {
            transform: scale(1.01);
            box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2);
        }
</style>-->