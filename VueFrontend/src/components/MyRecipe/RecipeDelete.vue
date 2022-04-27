<template>
    <p> Recipe is deleted successfully</p>
    <button @click="backToRecipe"> BACK TO RECIPE LIST </button>
</template>


<script>
	export default {
        props: ['recipe'],
        methods: {
            backToRecipe() {
                this.$router.push({
                    name: 'RecipeView',
                })
            }
        }
    }
    
</script>