<template>
    <div style="text-align:center">
        <h3>Search Website</h3>
    </div>
    <div v-for="discount in discounts" :key="discount.id" class="discount">
        <router-link :to="{name:'discountDetails', params: {id: discount.id}}">
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
    export default {
        data() {
            return {
                discounts: {},
                info: {
                    id: '',
                    title: '',
                    website: '',
                    description: '',
                    dateCreated: '',
                    likes: 0,
                    dislikes: 0
                }
            }
        },
        components: {

        },
        created() {
            this.getWebDiscounts()
        },
        methods: {
            getWebDiscounts() {
                //this.isAccountCreated = false;
                //this.resetValidateValues;
                $.ajax({
                    // set the HTTP request URL
                    url: `${URLS.apiRoot}studentdiscounts/getDiscountsWebsite`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    method: 'GET',
                    // On a successful AJAX request:
                    success: function (data) {
                        // log that we've completed
                        this.discounts = data
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
