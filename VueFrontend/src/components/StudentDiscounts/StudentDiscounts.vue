<template>
    <div>
        <h2 class="row1">Student Discounts</h2>
    </div>
    <div>
        Please select the the action you want to perform for Student Discounts:
        <br />
        <div class="action">
            <div :class="[isSearch ? 'active' : 'deactive']">
                <button class="btn" @click="searchSwitch" :class="{active:isSearch, }">Search</button>
            </div>
            <div :class="[isPost ? 'active' : 'deactive']">
                <button class="btn" @click="postSwitch" :class="{active:isPost}"> Post </button>
            </div>
        </div>
        <PostDiscounts v-if="isPost" />
        <SearchDiscounts v-if="isSearch" />
    </div>
        <router-view class="row3" />
</template>

<script>
    import PostDiscounts from './PostDiscounts.vue'
    import SearchDiscounts from './SearchDiscounts.vue'
    export default {
        name: 'StudentDiscounts',
        components: {
            PostDiscounts,
            SearchDiscounts
        },
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
    .page {
        margin: auto;
    }

    .header {
        font-size: large;
        text-align: center;
    }

    .btn {
        width: 5em;
        text-align: center;
    }

    .active {
        background-color: teal;
    }

    .deactive {
        background-color: silver;
    }

    .action {
        display: flex;
        align-items: center;
        justify-content: center;
        border: 5mm;
        padding-bottom: 0.5em;
    }

    /*#topButtons{
        padding: 30px;
    }
    #topButtons a {
        font-weight: bold;
        color: #2c3e50;
    }
    #topButtons a.router-link-exact-active{
        color: white;
        background-color: crimson;
        padding: 10px;
        border-radius: 5px;
    }
    .action {
        align-content: center;
        display: flex;
        align-items: center;
        justify-content: center;
        border: 10mm;
    }

    .ui.button,
    .dot.circle.icon {
        background-color: silver;
        color: white;
        width: 20mm;
    }*/
</style>
