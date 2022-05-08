<template>
    <h1>Establishment Discount Details</h1>
    <div class="box">
        <div class="backButton">
            <button @click="$router.go(-1)">Back</button>
        </div>
        <br /><br />
        <div v-for="discount in discounts" :key="discount.id">
            <h2 class="title">{{discount.title}}</h2>
            <div class="date">Date Posted: {{discount.date}}</div>
            <br />
            <br />
            <div class="details">
                Address: {{discount.address}}
                <br />
                Description:
                <br />
                {{discount.description}}
            </div>
            <br />
            <ShowMap :latitud="parseFloat(discount.lat)" :longitud="parseFloat(discount.lng)" />
        </div>
        <br />
        <br />
    </div>
</template>

<script>
    import ShowMap from './ShowMap.vue'
    import URLS from '../../variables'
    import axios from 'axios'
    export default {
        props: ['id'],
        components: {
            ShowMap
        },
        data() {
            return {
                discounts: {},
            }
        },
        created() {
            this.getDetails()

        },
        methods: {
            getDetails() {
                axios.get(URLS.api.studentDiscounts.getEstDetails + "/" + this.id,
                    { timeout: 5000 })
                    .then(response => {
                        this.discounts = response.data
                    })
                    .catch(e => {
                        console.error("There was an error", e)
                    })
            }


        }
    }
</script>
<style scoped>
    .date {
        text-align: left;
    }


    .details {
        text-align: left;
        width: 500px;
        height: 100px;
    }

    .box {
        background-color: rgb(212, 200, 210);
        margin: auto;
    }

    .backButton {
        margin-right: 20px;
        float: right;
    }

    button {
        font-size: 16px;
        background-color: crimson;
        border-bottom-color: crimson;
        border-radius: 8px;
        color: white;
        box-shadow: 6px;
    }
</style>
