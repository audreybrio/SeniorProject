<template>
    <h> Hello </h>
    <div class= "recipe-view" v-if = "recipe in recipes" :key="recipe.id">
        <h1> {{ recipe.title}} </h1>
    </div>
    <router-view/>
</template>

<script>
import axios from 'axios'

export default ({
    data(){
        return{
            recipes: null
        }
    },
    created(){
        axios.get('https://localhost:5002/api/schedule/getschedule/getlist')
        .then(response => {
            this.recipes = response.data
        })
        .catch(error => {console.log(error)
        })


    }
})
</script>

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
