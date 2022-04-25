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
                Description:
                <br />
                {{discount.description}}
            </div>
            <button>Website</button>
        </div>
        <br />
        <br />
    </div>
</template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
        props: ['id'],
        data() {
            return {
                discounts: {}
            }
        },
        created() {
            this.getDetails()
        },
        methods: {
            getDetails() {
                console.log('getting establishments')
                $.ajax({
                    // set the HTTP request URL
                    url: `${baseURL}/api/studentdiscounts/getEstDetails/${this.id}`,
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
                        console.log(data)
                        // log that we've completed
                        //this.discounts = data
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
