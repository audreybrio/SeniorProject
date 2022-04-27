<template>
    <div>
        <h3>Search Establishments</h3>
    </div>
    <div v-for="discount in discounts" :key="discount.id" class="discount">
        <router-link :to="{name:'EstablishmentDetails', params: {id: discount.id}}">
            <h2>{{discount.title}}</h2>
        </router-link>
    </div>
    <br />
    <br />

</template>

<script>
    import * as $ from 'jquery'
    //const baseURL = "https://localhost:5002";
    import URLS from '../../variables'
//import Map1 from './ShowMap.vue'

export default {
  data () {
        return {
            discounts: {},
            displayMap: false,
    }
  },
  created(){
      this.getEstDiscounts()
    },
  methods:{
      getEstDiscounts() {
          //this.isAccountCreated = false;
          //this.resetValidateValues;
          $.ajax({
              // set the HTTP request URL
              url: `${URLS.apiRoot}studentdiscounts/getDiscountsEstablishment`,
              // set the context object to the vue component
              // this line tells vue to update its components
              // when the success or error objects complete!
              // if it's not set, the components don't update!
              context: this,
              // HTTP method
              method: 'GET',
              // On a successful AJAX request:
              success: function (data) {
                  this.discounts = data
                  // log that we've completed

                  return true;
              },
              // On an unsuccessful AJAX request:
              error: function (error) {
                  // log the error
                  console.log(error);
                  this.items = null;
                  return false;
              }
          });
      }
  }
}
</script>

<style scoped>
    .ui.button,
    .dot.circle.icon{
    background-color: #ff5a5f;
    color:white;
    }

    .alignLeft {
        text-align: left;
        margin-left: 50px;
        font-weight: bold;
    }
    .discount h2 {
        background-color: #f4f4f4;
        padding: 20px;
        border-radius: 10px;
        margin: 10px auto;
        max-width: 600px;
        cursor: pointer;
        color: #444;
        text-decoration: none;
    }

        .discount h2:hover {
            background: #ddd;
        }

    .discount .a {
        text-decoration: none;
    }
</style>