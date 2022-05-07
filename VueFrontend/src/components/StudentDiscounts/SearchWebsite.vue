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
    import axios from 'axios'
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
            async getWebDiscounts() {
                const response = await axios.get(URLS.api.studentDiscounts.getWebsites,
                    { timeout: 5000 });
                this.discounts = response.data;

                /*axios.get(URLS.api.studentDiscounts.getWebsites,
                    { timeout: 5000 })
                    .then(response => {
                        this.discounts = response.data
                    })
                    .catch(e => {
                        console.error("There was an error", e)
                    })*/
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
