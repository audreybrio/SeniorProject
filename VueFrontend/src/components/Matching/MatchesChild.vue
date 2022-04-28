<template>
    <router-link class="recipe-link" :to="{ name: 'RecipeLayout', params:{ id: recipe.id} }">
        <div class="recipe-card">
            <h4>{{recipe.id}}: {{recipe.title}} </h4>
            <p> {{ recipe.category }} </p>
            <p> {{ recipe.calorieValue }} </p>
            <p>{{ recipe.datePosted }} </p>
        </div>
    </router-link>
</template>

<script>
    export default {
        props: {
            recipe: {
                type: Object,
                required: true
            }
        }
    }
</script>

<style scoped>
    .recipe-card {
        padding: 20px;
        width: 250px;
        cursor: pointer;
        border: 1px solid #39495c;
        margin-bottom: 18px;
    }

        .recipe-card:hover {
            transform: scale(1.01);
            box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2);
        }

    .recipe-link {
        color: black;
        text-decoration: none
    }
</style>