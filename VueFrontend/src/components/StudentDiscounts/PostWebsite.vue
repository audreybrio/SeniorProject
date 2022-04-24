<template>
    <div class="page">
        <h3>Post Website</h3>
        <div v-for="(error, index) in errors" :key="index" class="warning">{{index + 1}}. {{error}}</div>
        <div v-if="isDiscountPosted" class="discountPostedStyle">STUDENT DISCOUNT POSTED</div>
        <form @submit.prevent="submitButtonPressed">
            <label for="title">Discount Title:</label>
            <br />
            <input type="text" id="title" v-model="discountInfo.title" ref="postWeb" maxlength="30" required>
            <br />
            <label for="website">Website:</label>
            <br />
            <input type="text" id="website" v-model="discountInfo.website" maxlength="50" required>
            <br />
            <label for="description">Discount description:</label>
            <br />
            <textarea name="description" id="description" v-model="discountInfo.description" maxlength="160" required></textarea>
            <input type="submit" value="Post Discount" class="submitButton" />
        </form>
    </div>
</template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
    export default {
        data() {
            return {
                isDiscountPosted: false,
                errors: [],
                discountInfo: {
                    title: '',
                    website: '',
                    description: ''
                }
            }
        },
        mounted() {
            this.$refs.postWeb.focus();
        },
        methods: {
            submitButtonPressed() {
                this.isDiscountPosted = true
                this.errorMessages()
                this.postDiscount()
            },
            errorMessages() {
                this.errors.push('Error')
                this.errors.push('Error')
                this.errors.push('Error')
                this.errors.push('Error')
            },
            postDiscount() {
                console.log('descr: ' + this.discountInfo.description)
                $.ajax({
                    // set the HTTP request URL
                    url: `${baseURL}/api/studentdiscounts/postWebsite/${this.discountInfo.title}/${this.discountInfo.website}/
                            ${this.discountInfo.description}`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    method: 'GET',
                    // On a successful AJAX request:
                    success: function () {
                        console.log('success')
                        // log that we've completed
                        return true;
                    },
                    // On an unsuccessful AJAX request:
                    error: function (error) {
                        // log the error
                        console.log(error);
                        return false;
                    }
                });
            }
        }
    }
</script>

<style scoped>
    .discountPostedStyle {
        font-size: 11px;
        background-color: gold;
        border: 1px solid gold;
        border-radius: 5px 4px;
    }

    .warning {
        color: red;
        margin: auto;
        width: 500px;
        text-align: left;
        font-size: 15px;
    }

    input[type=text]:focus, input[type=password]:focus, textarea:focus {
        background-color: lightblue;
        border: 2px solid red;
        border-radius: 4px;
    }

    .submitButton {
        width: 92%;
        color: white;
        background-color: red;
        border-radius: 10px;
        height: 40px;
    }

    .page {
        margin: auto;
    }

    form {
        text-align: left;
        margin: auto;
        width: 500px;
    }

    input {
        width: 90%;
        margin-bottom: 7px;
        font-size: 20px;
    }

    textarea {
        width: 90%;
        max-width: 90%;
        height: 6em;
        font-size: 20px;
    }
</style>