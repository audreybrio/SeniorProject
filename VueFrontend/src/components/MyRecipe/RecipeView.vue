<template>
    <h> Hello </h>
    <div class= "recipe-view" v-if="recipe in recipes" :key="recipe.id">
        <h1> {{ recipe.id}} </h1>
    </div>
    <router-view/>
</template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default ({
        data() {
        return{
            loading: false,
            recipes: []
        }
        },
        created() {
            this.loading = this.getrecipe();
        },
        methods: {
            getrecipe() {

                $.ajax({
                    // set the HTTP request URL
                    url: `${baseURL}/api/recipe`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    contentType: 'application/json',

                    method: 'GET',
                    // On a successful AJAX request:
                    success: function(data) {
                        this.recipes = data;
                        this.loading = false;
                        // TODO: delete console.logs
                        console.log("this.list:")
                        console.log(this.list)
                        console.log("this.loading:")
                        console.log(this.loading)
                        // log that we've completed
                        console.log("ajax Success")
                        return true;
                    },
                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(error);
                        this.items = null;
                        this.loading = false;
                        return false;
                    }
                })
            }


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
