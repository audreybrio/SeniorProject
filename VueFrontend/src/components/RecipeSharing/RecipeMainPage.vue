<template>
  <div class="recipe">
    <h1>My Recipes</h1>
	<button @click="togglePopup">Add new Recipe</button>
   <div class = "recipes">
	<div class= "card" v-for="recipe in $store.state.recipes" :key="recipe.slug">
		<h2>{{ recipe.title }}</h2>
          <p>{{ recipe.description }}</p>
          <router-link :to="`/recipe/${recipe.slug}`">
            <button>View Recipe</button>
          </router-link>
	</div>
	</div>
	<div class="add-recipe-popup" v-if="popupOpen">
		<div class="popup-content">
			<h2> Add New Recipe</h2>
			<form @submit.prevent="addNewRecipe">
		<div class="group">
			<label>Title</label>
			<input type="text" v-model="newRecipe.title" />
		</div>
		<div class="group">
            <label>Description</label>
            <textarea v-model="newRecipe.description"></textarea>
        </div>

		<div class="group">
            <label>Ingredients</label>
            <div class="ingredient" v-for="i in newRecipe.ingredientRows" :key="i">
              <input type="text" v-model="newRecipe.ingredients[i - 1]" />
            </div>
            <button type="button" @click="addNewIngredient">Add Ingredient</button>
            
            <button type="button" @click="removeNewIngredient">Remove Ingredient</button>
        </div>
		
		<div class="group">
            <label>Method Steps</label>
            <div class="method" v-for="i in newRecipe.methodRows" :key="i">
              <textarea v-model="newRecipe.method[i - 1]"></textarea>
            </div>
            <button type="button" @click="addNewStep">Add step</button>
            <button type="button" @click="removeNewStep">Remove step</button>
          </div>

          <button type="submit" >Add Recipe</button>
          <button type="button" @click="togglePopup">Close</button>
		</form>
		</div>
		
	</div>
  <button @click="GoBack"> Back To Main Page </button>
  </div>
  <router-view/>

</template>

<script lang= "js">
import { ref } from 'vue';
import { useStore } from 'vuex';
import router from '@/router';

export default {
  name: 'RecipeMainPage',
  setup () {
    const newRecipe = ref({
      title: '',
      description: '',
      ingredients: [],
      method: [],
      ingredientRows: 1,
      methodRows: 1
    });
    const popupOpen = ref(false);
    const store = useStore();
    const togglePopup = () => {
      popupOpen.value = !popupOpen.value;
    }
    const addNewIngredient = () => {
      newRecipe.value.ingredientRows++;
    }
    const removeNewIngredient = () => {
      newRecipe.value.ingredientRows--;
    }
    const addNewStep = () => {
      newRecipe.value.methodRows++;
    }
    const removeNewStep = () => {
      newRecipe.value.methodRows--;
    }
    const addNewRecipe = () => {
      newRecipe.value.slug = newRecipe.value.title.toLowerCase().replace(/\s/g, '-');

      if (!newRecipe.value.slug) {
        alert("Please enter a title");
        return;
      }
      store.commit('ADD_RECIPE', { ...newRecipe.value });
      newRecipe.value = {
        title: '',
        description: '',
        ingredients: [],
        method: [],
        ingredientRows: 1,
        methodRows: 1
      };
      togglePopup();
    }
    const GoBack = () => {
      router.push({ name: "HomePage" });
    }

    return {
      newRecipe,
      addNewRecipe,
      togglePopup,
      popupOpen,
      addNewStep,
      addNewIngredient,
      removeNewIngredient,
      removeNewStep,
      GoBack,
    }
  }
}
</script>

<style>
.recipe {
  padding: 1rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow: auto;
}
h1 {
  font-size: 1rem;
  margin-bottom: 32px;
}
.recipes {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
}
.recipes .card {
  padding: 1rem;
  border-radius: 5px;
  margin: 1rem;
  background-color: #06eb24;
}
.recipes .card h2 {
  font-size: 2rem;
  margin-bottom: 1rem;
}
.recipes .card p {
  font-size: 1.125rem;
  line-height: 1.4;
  margin-bottom: 1rem;
}
.add-recipe-popup {
  position: absolute;
  display: flex;
  top: 50;
  left: 50;
  width: 80%;
  height: 80%;
  justify-content: center;
  align-items: center;
  overflow: flex;
}
.add-recipe-popup .popup-content {
  background-color: #06eb24;
  padding: 2rem;
  border-radius: 1rem;
  width: 125%;
  max-width: 700px;
}
.popup-content h2 {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  font: bold;
}
.popup-content label {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  font: bold;
}
.popup-content .group {
  margin-bottom: 1rem;
}

.popup-content .group label {
  display: block;
  margin-bottom: 0.5rem;
}
.popup-content .group input,
.popup-content .group textarea {
  display: block;
  width: 100%;
  padding: 0.5rem;
  border: 1px solid rgb(12, 6, 6);
  border-radius: 5px;
  margin-bottom: 1rem;
}
.popup-content .group textarea {
  height: 100px;
  resize: none;
}
.popup-content button[type="submit"] {
  margin-right: 1rem;
  background-color: rgb(251, 191, 11);
  border-color: black;
}
.popup-content button[type="button"] {
  margin-right: 1rem;
  background-color: rgb(251, 191, 11);
  border-color: black;

}

</style>