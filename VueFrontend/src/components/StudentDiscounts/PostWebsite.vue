<template>
    <div class="page">
        <h3>Post Website</h3>
        <ul>
            <li v-for="(error, index) in errors" :key="index" class="warning">{{error}}</li>
        </ul>
        <div v-if="isDiscountPosted" class="discountPostedStyle">STUDENT DISCOUNT POSTED</div>
        <form @submit.prevent="submitButtonPressed">
            <label for="title">Discount Title:</label>
            <br />
            <input type="text" id="title" v-model="discountInfo.title" maxlength="30" required>
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
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        data() {
            return {
                isDiscountPosted: false,
                errors: [],
                discountInfo: {
                    title: '',
                    website: '',
                    description: ''
                },
                validInputWeb: {
                    validTitle: false,
                    validWebsite: false,
                    validDescription: false
                }
            }
        },
        methods: {
            submitButtonPressed() {
                this.resetValidInput()
                this.errorMessages()
                if (this.isValid()) {
                    this.postDiscount()
                    this.isDiscountPosted = true
                    this.resetInputFields()
                }
                
            },
            errorMessages() {
                this.errors = []

                if (this.discountInfo.title.length < 4) {
                    this.errors.push('Discount title must be 4 or more characters') 
                }
                else {
                    this.validInputWeb.validTitle = true
                }

                if (!this.validateWebsite()) {
                    this.errors.push('Please enter a valid website URL')
                }
                else {
                    this.validInputWeb.validWebsite = true
                }

                if (this.discountInfo.description.length < 4) {
                    this.errors.push('Discount description must be 4 or more characters')
                }
                else {
                    this.validInputWeb.validDescription = true
                }
            },
            validateWebsite() {
                if (new RegExp("([a-zA-Z0-9]+://)?([a-zA-Z0-9_]+:[a-zA-Z0-9_]+@)?([a-zA-Z0-9.-]+\\.[A-Za-z]{2,4})(:[0-9]+)?(/.*)?").test(this.discountInfo.website)) {
                    return true
                }
                else {
                    return false
                }
            },
            isValid() {
                if (this.validInputWeb.validTitle && this.validInputWeb.validWebsite && this.validInputWeb.validDescription) {
                    return true
                }
                else {
                    return false
                }
            },
            resetValidInput() {
                this.validInputWeb.validTitle = false
                this.validInputWeb.validWebsite = false
                this.validInputWeb.validDescription = false
            },
            resetInputFields() {
                this.discountInfo.title = ''
                this.discountInfo.website = ''
                this.discountInfo.description = ''
            },
            postDiscount() {
                axios.get(URLS.api.studentDiscounts.postWebsite + this.discountInfo.title +
                    "/" + this.discountInfo.website + "/" + this.discountInfo.description,
                    { timeout: 5000 })
                    .then(response => {
                        console.log(response)
                    })
                    .catch(e => {
                        console.log(e)
                    })
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