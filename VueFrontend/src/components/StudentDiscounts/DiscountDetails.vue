<template>
    <h1>Discount Details</h1>
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
                Description:
                <br />
                {{discount.description}}
            </div>
            <button @click="redirectToWebsite(discount.website)">Website</button>
        </div>
        <br />
        <br />
    </div>
</template>

<script>
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        props: ['id'],
        data() {
            return {
                discounts: {},
            }
        },
        created() {
            this.getDetails()
        },
        methods: {
            redirectToWebsite(url) {
                window.location.href = "https://" + url
            },
            getDetails() {
                axios.get(URLS.api.studentDiscounts.getWebDetails + "/" + this.id,
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
