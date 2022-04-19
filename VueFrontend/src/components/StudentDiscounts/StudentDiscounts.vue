<!--<script setup>
  import PostDiscounts from './components/PostDiscounts.vue'
  import SearchDiscounts from './components/SearchDiscounts.vue'
    // import HelloWorld from './components/HelloWorld.vue'
</script>-->

<template>
    <div class="page">
        <div class="header">
            <h1>Student Discounts</h1>
            <br />
        </div>
        <!--<HelloWorld />-->

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
        <!-- <EmployeeList /> -->
    </div>
    <router-view />
</template>

<script>
    import PostDiscounts from './components/PostDiscounts.vue';
    import SearchDiscounts from './components/SearchDiscounts.vue';

    export default {
        name: 'App',
        data(){
          return{
            // showDialog: false,
            // showDialogUser: false,
            isSearch: false,
            isPost: true
            // form:{
            //     name: '',
            //     email: '',
            //     gender: '',
            // }
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
        Components: {
          PostDiscounts,
          SearchDiscounts,
          // HelloWorld
        }
    }</script>


<style>
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
</style>
