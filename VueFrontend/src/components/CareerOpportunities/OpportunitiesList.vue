<template>
    <div class="backButton">
        <button @click="$router.go(-1)">Back</button>
    </div>
    <h1>Search results page</h1>
    <div v-if="loading">
        Loading...
    </div>
    <div v-else>
        <div v-if="results.length > 0" >
            <div v-for="(opportunities, index) in results" :key="opportunities[index]" class="opportunities">
                <router-link :to="{name: 'OpportunityDetails',
                             params:{
                                title: opportunities.matchedObjectDescriptor.positionTitle,
                                organizationName: opportunities.matchedObjectDescriptor.organizationName,
                                jobSummary: opportunities.matchedObjectDescriptor.userArea.details.jobSummary,
                                applicationCloseDate: opportunities.matchedObjectDescriptor.applicationCloseDate,
                                url: opportunities.matchedObjectDescriptor.positionUri,
                                location: opportunities.matchedObjectDescriptor.positionLocationDisplay
                             }}">
                    <h2>{{opportunities.matchedObjectDescriptor.positionTitle}}</h2>
                </router-link>
            </div>
        </div>
        <div v-else>
            <h1>No results found</h1>
        </div>
        
    </div>

    
</template>

<script>
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        props:['keywords'],
        data(){
            return {
            results:{},
            loading: true
            }
        },
        created(){
            this.getOpportunities()
            
        },
        methods: {
            // getOpportunities method sends a GET request to the backend to retrieve data from USAJobs API
            async getOpportunities() {

                const response = await axios.get(URLS.api.careerOpportunities.getOpportunities + "/" + this.keywords)
                this.results = response.data.searchResult.searchResultItems
                this.loading = false
            },
        }
    }
</script>

<style>
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
    .opportunities h2 {
        background-color: #f4f4f4;
        padding: 20px;
        border-radius: 10px;
        margin: 10px auto;
        max-width: 600px;
        cursor: pointer;
        color: #444;
        text-decoration: none;
    }

    .opportunities h2:hover {
        background: #ddd;
    }

    .opportunities .a {
        text-decoration: none;
    }
</style>