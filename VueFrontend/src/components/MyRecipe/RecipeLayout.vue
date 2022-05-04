<template>
    <div v-if="recipe">
        <h1>{{ recipe.title }}</h1>
        <div id="nav">
            <router-link :to="{ name: 'RecipeDetails'}"><button>Details</button></router-link>
            |
            <router-link :to="{ name: 'RecipeDelete'}"><button @click="deleteRecipe">Delete</button></router-link>
            |
            <router-link :to="{ name: 'RecipeEdit'}"><button>Edit</button></router-link>
        </div>
        <router-view :recipe="recipe" />
    </div>
</template>
<script>
    import AccessService from '/src/variables/AccessService.js'


    export default {
      props: ['id'],
      data() {
        return {
          recipe: null
        }
      },
      created() {
        AccessService.getRecipe(this.id).then(response => {
            this.recipe = response.data})
            .catch(error => {
                console.log(error)
            })
      },
      methods: {

          deleteRecipe() {
              AccessService.deleteRecipe(this.id).then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });
          }

      }


    }
</script>