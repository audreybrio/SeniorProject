<template>
    <div>
        <h2 class="row1">Student Discounts</h2>
    </div>
    <div class="flex-container row2">
        Please select the the action you want to perform for Student Discounts:
        <div class="action">
            <div>
                <router-link to="/studentDiscounts/SearchDiscount" class="ui button">Search</router-link>
            </div>
            <div>
                <router-link to="/studentDiscounts/PostDiscount" class="ui button">Post</router-link>
            </div>
        </div>
    </div>
    <router-view class="row3"/>
</template>

<script>

    export default {
        name: 'StudentDiscounts',
        data(){
          return{
            isSearch: false,
            isPost: true
          }

        },
        methods: {
          searchSwitch(){
            if (!this.isSearch){
              this.isSearch = true
              this.isPost = false
              console.log("search: " + this.isSearch + " post: " + this.isPost)
            }
          },
          postSwitch(){
            if (!this.isPost){
              this.isSearch = false
              this.isPost = true
              console.log("search: " + this.isSearch + " post: " + this.isPost)
            }
          }

        },
    }</script>


<style scoped>
    .action {
        align-content: center;
        display: flex;
        align-items: center;
        justify-content: center;
        border: 5mm;
    }

    .ui.button,
    .dot.circle.icon {
        background-color: teal;
        color: white;
    }
</style>
