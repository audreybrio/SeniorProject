<template>
    <div id="doNotSellMyPersonalInformation">
        <h3>Do Not Sell My Personal Information</h3>
        While we do not sell any of your personal information, it is your right under the <a href="https://oag.ca.gov/privacy/ccpa">California Consumer Privacy Act of 2018</a> to opt out of any such sales.
        In the event that our privacy policy changes in the future, your choices will be remembered.
        You may view our privacy policy below:
        <br />
        <label for="optform">Sell My Personal Information</label>
        <input type="checkbox" v-model="sellMyInfo" id="optform" />
        <button @click="confirm">Confirm My Choices</button>
    </div>
    <br />
    <div id="delete">
        <h3>Delete My Personal Information</h3>
        It is your right to opt out of the sale of your personal information, as well as to delete any of your personal information that we have collected.
        You may delete your account and personal information here: <button @click="onDelete">Delete My Personal Information</button>
    </div>
    <br />
    <div id="privacyPolicy">
        <h3>Privacy Policy</h3>
        Under the <a href="https://oag.ca.gov/privacy/ccpa">California Consumer Privacy Act of 2018</a>, California consumers have the following rights:
        <ul>
            <li>The right to know about the personal information a business collects about them and how it is used and shared,</li>
            <li>The right to delete personal information collected from them,</li>
            <li>The right to opt-out of the sale of their personal information,</li>
            <li>And the right to non-discrimination for exercising their rights under the CCPA.</li>
        </ul>
        <br />
        We collect the following information about all of our users who create an account with us:
        <ul>
            <li>Name</li>
            <li>Email Address</li>
            <li>The school you attend</li>
        </ul>
        <br />
        To use various features of our website, you may need to provide us with additional personal information, such as:
        <ul>
            <li>Personal schedule information (schedule builder, schedule comparison, matching),</li>
            <li>Hobbies and/or interests (matching),</li>
            <li>Grades and/or GPA (GPA calculator),</li>
            <li>Messages (messaging),</li>
            <li>Financial aid status (aid eligibility),</li>
            <li>Resumes and/or resume-related informaiton (career opportunities)</li>
        </ul>
        <br />
        The following information is collected anonymously:
        <ul>
            <li>Student discounts,</li>
            <li>Preferred foods/recipes (recipe sharing)</li>
        </ul>
        We collect this information strictly through user input. This means that we only collect information that you give us.
        We store this data in databases and files owned and operated by us.
        We only share data with other users when allowed by the specific functionalities of the features in which the data was gathered and/or used.
        We do NOT share this data with third parties, and we do NOT sell this data.
        We only use this data to allow the features of StudentMultiTool to function.
        <br />
        Last updated: May 2022
    </div>
    <router-view />
</template>

<script lang="js">
    import axios from 'axios'
    import router from '../../router'
    import URLS from '../../variables'
    import jwt_decode from 'jwt-decode'
    export default {
        name:'UserPrivacy',
        data() {
            return {
                sellMyInfo: true,
                user: jwt_decode(window.sessionStorage.getItem("token")).username
            }
        },
        created() {
            this.user = jwt_decode(window.sessionStorage.getItem("token")).username
            this.getCurrentOptions()
        },
        methods: {
            getCurrentOptions() {
                let user = this.user
                axios.get(`${URLS.api.userPrivacy.getOptions}?username=${user}`)
                    .then(response => {
                        console.log(response)
                        this.sellMyInfo = response.sellMyInfo
                    })
                    .catch(error => {
                        console.log(error)
                        alert(error)
                    })
            },
            confirm(){
                let options = {
                    UserName: this.user,
                    SellMyInfo: this.sellMyInfo
                }
                console.log(options)
                axios.post(`${URLS.api.userPrivacy.setOptions}`, options)
                    .then(response => {
                        console.log(response)
                        options.SellMyInfo = response.SellMyInfo
                    })
                    .catch(error => {
                        console.log(error)
                    })
                this.SellMyInfo = options.SellMyInfo
            },
            onDelete() {
                router.push({ name: 'AccountDeletion' })
            }
        }
    };
</script>