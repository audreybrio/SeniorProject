<template>
    <div class="page">
        <div>
            <h2 class="row1">Student Career Opportunities</h2>
        </div>
        <div class="container">
            <div class="homepage">
                <router-link to="{name: 'HomePage'}">HomePage</router-link>
            </div>
            <form @submit.prevent="pressMatch">
                <label for="status">Student status: &nbsp;&nbsp;</label>
                <select v-model="status">
                    <option disabled value="">Select</option>
                    <option>Freshman</option>
                    <option>Sophomore</option>
                    <option>Junior</option>
                    <option>Senior</option>
                </select>
                <br />

                <label for="units">Completed units:</label>
                <input type="number" v-model.number="units" class="units" />
                <br />

                <label for="major">Major:</label>
                <input type="text" name="major" v-model="major" class="major" maxlength="50">
                <br />

                <div class="left-half">
                    <label for="certifications">Certifications?</label>
                </div>
                <div class="right-half">
                    <input type="checkbox" v-model="hasCert" />
                    <div v-if="hasCert" style="all:initial">
                        <input type="text" name="certifications" v-model="cert" placeholder="Title" maxlength="30">
                        <button @click="addCertification">Add</button>
                    </div>
                </div>
                <br />

                <div class="left-half">
                    <label for="internships">Internships?</label>
                </div>
                <div class="right-half">
                    <input type="checkbox" v-model="hasIntern" />
                    <div v-if="hasIntern" style="all:initial">
                        <input type="text" name="internships" v-model="intern" placeholder="Title" maxlength="30">
                        <button @click="addInternship">Add</button>
                    </div>
                </div>
                <br />

                <div class="left-half">
                    <label for="research">Research programs?</label>
                </div>
                <div class="right-half">
                    <input type="checkbox" v-model="hasResearch" />
                    <div v-if="hasResearch" style="all:initial">
                        <input type="text" name="research" v-model="research" placeholder="Title" maxlength="30">
                        <button @click="addResearch">Add</button>
                    </div>
                </div>
                <br />
                <div style="text-align: center;">
                    <input type="submit" value="Search" class="submitButton" />
                </div>
            </form>
            {{done}}
            <!-- {{ status }} {{units}} {{major}} <br/>
            hasCert: {{hasCert}}
            hasIntern: {{hasIntern}} <br/>
            hasResearch: {{hasResearch}} <br/>
            cert: {{certifications}}<br/>
            internships: {{internships}}<br/>
            researches: {{researches}} -->
            <br />
        </div>
        <router-view class="row3" />
    </div>
</template>

<script>
    import axios from 'axios'
    import URLS from '../../variables'
    export default {
        name: 'CareerOpportunities',
        components: {
            // PostDiscounts,
            // SearchDiscounts
        },
        data(){
          return{
            done: "",
            status: "",
            units: 0.0,
            major: "",
            hasCert: false,
            hasIntern: false,
            hasResearch: false,
            cert: "",
            certifications: [],
            intern: "",
            internships: [],
            research: "",
            researches: []
          }

        },
        methods: {
          pressMatch(){
              this.done = "Done"
              this.getOpportunities()
          },
          getOpportunities(){
              axios.get(URLS.api.careerOpportunities.getOpportunities)
                  .then(response => {
                      console.log(response.data.searchResult)
                      console.log(response.data.searchResult.searchResultItems)
                      console.log(response.data.searchResult.searchResultItems[0])
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.positionTitle)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.organizationName)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.positionLocationDisplay)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.positionUri)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.qualificationSummary)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.userArea.details)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.userArea.details.jobSummary)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.userArea.details.requiredDocuments)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.positionEndDate)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.positionLocation[0].latitude)
                      console.log(response.data.searchResult.searchResultItems[0].matchedObjectDescriptor.positionLocation[0].longitude)
                  // console.log(response.data.searchResultItems[0].MatchedObjectDescriptor)
                  })
                  .catch(e => {
                      console.error("There was an error", e)
                  })
          },
          addCertification(){
              if(this.cert != ""){
                this.certifications.push(this.cert)
                this.cert = ""
              }
          },
          addInternship(){
              if(this.intern != ""){
                this.internships.push(this.intern)
                this.intern = ""
              }
          },
          addResearch(){
              if(this.research != ""){
                this.researches.push(this.research)
                this.research = ""
              }
          }

        },
    }</script>


<style scoped>
    .submitButton {
        font-size: 30px;
        margin-top: 20px;
        width: 60%;
        color: white;
        background-color: cornflowerblue;
        border-radius: 10px;
        height: 40px;
    }

    input[type=checkbox] {
        transform: scale(1.2);
    }

    .left-half {
        text-align: end;
        float: left;
        width: 38%;
        height: 25px;
    }

    .right-half {
        float: right;
        width: 62%;
        height: 25px;
    }

    input {
        margin-bottom: 5px;
    }

    select {
        font-size: 15px;
        margin-bottom: 5px;
    }

    .units {
        width: 94px;
    }

    .major {
        width: 170px;
    }

    form {
        text-align: left;
    }

    .container {
        margin: auto;
        display: table;
        height: 100%;
        width: 100%;
        width: 400px;
        height: 200px;
        background-color: rgb(212, 200, 210);
    }

    .homepage {
        text-align: end;
    }

    .page {
        margin: auto;
    }

    input[type=text]:focus, input[type=number]:focus, select:focus {
        background-color: lightblue;
        border: 2px solid red;
        border-radius: 4px;
    }
</style>
