<template>
    <div class="page">
        <div class="backButton">
            <button @click="$router.go(-1)">Back</button>
        </div>
        <div>
            <h2 class="row1">Opportunities Details</h2>
        </div>
        <router-link to="/home" class="homepage">HomePage</router-link>
        <div class="container">
            Job Title: {{title}} <br /> <br />
            Location: {{ location }} <br /> <br />
            Organization Name: {{ organizationName }} <br /> <br />
            Application Close Date: {{ applicationCloseDate }} <br /> <br />
            Job Summary: <br /> 
            {{ jobSummary}} <br/>
            <button @click="redirectToWebsite()">Apply</button>
        </div>
        <router-view class="row3" />
    </div>
</template>

<script>

    export default {
        name: 'DisplayOpportunities',
        props: ['title', 'organizationName', 'jobSummary', 'applicationCloseDate', 'url', 'location'],
        methods: {
            // redirectToWebsite method to redirect the user to the website
            redirectToWebsite() {
                window.location.href = this.url
            }

        },
    }</script>


<style scoped>
    button {
        font-size: 16px;
        background-color: crimson;
        border-bottom-color: crimson;
        border-radius: 8px;
        color: white;
        box-shadow: 6px;
    }
    .backButton {
        margin-right: 20px;
        float: right;
    }
    .left-half {
        float: left;
        background-color: blue;
        width: 35%;
        height: 280px;
    }

    .right-half {
        float: right;
        background-color: blueviolet;
        width: 65%;
        height: 280px;
    }

    .container {
        text-align: left;
        display: table;
        height: 100%;
        width: 100%;
        height: 300px;
        background-color: rgb(212, 200, 210);
    }

    .homepage {
        margin-right: 20px;
        float: right;
    }

    .page {
        margin: auto;
        background-color: red;
    }

    .header {
        font-size: large;
        text-align: center;
    }

    .btn {
        width: 5em;
        text-align: center;
    }

    .active {
        background-color: teal;
    }

    .deactive {
        background-color: silver;
    }

    .action {
        display: flex;
        align-items: center;
        justify-content: center;
        border: 5mm;
        padding-bottom: 0.5em;
    }
</style>
