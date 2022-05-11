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
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        data() {
            return {
                discounts: {},
                displayMap: false,
            }
        },
        created() {
            this.getEstDiscounts()
        },
        methods: {
            async getEstDiscounts() {
                const response = await axios.get(URLS.api.studentDiscounts.getEstablishments,
                    { timeout: 5000 });
                this.discounts = response.data;

                /*axios.get(URLS.api.studentDiscounts.getEstablishments,
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
    .ui.button,
    .dot.circle.icon {
        background-color: #ff5a5f;
        color: white;
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