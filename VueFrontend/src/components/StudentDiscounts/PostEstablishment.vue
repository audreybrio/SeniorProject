<!--<template>
    <div class="page">
        <div>
            <h3>Post Establishment</h3>
        </div>
        <br />
        <div class="warning">
            <div v-for="(error, index) in errors" :key="index" class="warning">{{index + 1}}. {{error}}</div>
        </div>
        <div v-if="isDiscountPosted" class="discountPostedStyle">STUDENT DISCOUNT POSTED</div>
        <form @submit.prevent="submitButtonPressed">
            <div>
                <label for="discountName">Discount Title:</label>
                <br />
                <input type="text" id="discountName" v-model="discountInfo.title" maxlength="30" required>
                <br />
                <label for="establishment">Establishment:</label>
                <br />
                <input type="text" id="establishment" v-model="discountInfo.name" maxlength="30" required>
                <br />
                <label for="address"> Address: </label>
                <div> style="all:initial; font-size: 20px; color:blue;">{{discountInfo.address}}</div>
                <br />
                <vue-google-autocomplete ref="discountInfo.address"
                                         id="map"
                                         classname="form-control"
                                         placeholder="Enter a valid address"
                                         v-on:placechanged="getAddressData"
                                         country="us"></vue-google-autocomplete>
                <br />
                <label for="description">Discount description:</label>
                <br />
                <textarea name="description" id="description" rows="3" v-model="discountInfo.description" maxlength="160" required></textarea>
                <br />
                <input type="submit" value="Post Discount" class="submitButton" />
            </div>
        </form>
    </div>
</template>

<script>
    import VueGoogleAutocomplete from "vue-google-autocomplete";
    import * as $ from 'jquery'
    //const baseURL = "https://localhost:5002";
    import URLS from '../../variables'
    export default {

        components: { VueGoogleAutocomplete },
        data() {
            return {
                isDiscountPosted: false,
                mapAddress: "",
                errors: [],
                addressEntered: false,
                discountInfo: {
                    title: '',
                    name: '',
                    address: '',
                    lat: '',
                    lng: '',
                    description: ''
                },
                validInput: {
                    validTitle: false,
                    validName: false,
                    validDescription: false
                }
            }
        },
        methods: {
            submitButtonPressed() {
                this.resetValidInput()
                this.isDiscountPosted = false
                this.errorMessages()
                if (this.areValidInputs()) {
                    this.postDiscount()
                    this.isDiscountPosted = true
                    this.resetInputFields()
                }
            },
            postDiscount() {
                $.ajax({
                    // set the HTTP request URL
                    url: `${URLS.apiRoot}studentdiscounts/postEstablishment/${this.discountInfo.title}
                           /${this.discountInfo.name}/${this.discountInfo.address}/${this.discountInfo.lat}
                           /${this.discountInfo.lng}/${this.discountInfo.description}`,
                    // set the context object to the vue component
                    // this line tells vue to update its components
                    // when the success or error objects complete!
                    // if it's not set, the components don't update!
                    context: this,
                    // HTTP method
                    method: 'POST',
                    // On a successful AJAX request:
                    success: function () {
                        
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
            },
            resetInputFields() {
                this.discountInfo.title = ''
                this.discountInfo.name = ''
                this.discountInfo.address = ''
                this.discountInfo.description = ''
            },
            getAddressData: function (addressData, placeResultData) {
                this.discountInfo.address = placeResultData.formatted_address
                this.discountInfo.lat = addressData.latitude
                this.discountInfo.lng = addressData.longitude
                this.addressEntered = true
            },
            errorMessages() {
                this.errors = []

                if (this.addressEntered == false) {
                    this.errors.push("Please, enter a valid address");
                }

                if (this.discountInfo.title.length < 4) {
                    this.errors.push("Discount title must be 4 or more characters");
                }
                else {
                    this.validInput.validTitle = true
                }

                if (this.discountInfo.name.length < 4) {
                    this.errors.push("Discount Establishment must be 4 or more characters");
                }
                else {
                    this.validInput.validName = true
                }

                if (this.discountInfo.description.length < 4) {
                    this.errors.push("Discount Description must be 4 or more characters");
                }
                else {
                    this.validInput.validDescription = true
                }

            },
            areValidInputs() {
                if (this.validInput.validTitle && this.validInput.validName &&
                    this.addressEntered && this.validInput.validDescription) {
                    return true
                }
                else {
                    return false
                }
            },
            resetValidInput() {
                this.validInput.validTitle = false
                this.validInput.validName = false
                this.validInput.validDescription = false
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
</style>-->
