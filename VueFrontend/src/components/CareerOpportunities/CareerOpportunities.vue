<template>
    <div class="page">
        <div>
            <h2 class="row1">Student Career Opportunities</h2>
        </div>
        <div class="container">
            <div class="homepage">
                <router-link to="/home">HomePage</router-link>
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
                <input type="number" v-model="units" class="units" step="0.1" />
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

                <div class="left-half">
                    <label for="graduate">Graduate student?</label>
                </div>
                <div class="right-half">
                    <input type="checkbox" v-model="graduated" />
                </div>
                <br />

                <div class="left-half">
                    <label for="searchInternship">Looking for internships?</label>
                </div>
                <div class="right-half">
                    <input type="checkbox" v-model="searchInternship" />
                </div>
                <br />

                <div style="text-align: center;">
                    <input type="submit" value="Search" class="submitButton" />
                </div>
            </form>
            <br />
        </div>
        <router-view class="row3" />
    </div>
</template>

<script>
    export default {
        name: 'CareerOpportunities',
        data(){
          return{
            status: "",
            units: (0).toFixed(1),
            major: "",
            hasCert: false,
            hasIntern: false,
            hasResearch: false,
            cert: "",
            certifications: [],
            intern: "",
            internships: [],
            research: "",
            researches: [],
            graduated: false,
            searchInternship: false,
            keywords: ""
          }

        },
        methods: {
          pressMatch(){
              this.combineFields()
              // It sends the keywords string to the OpportunitiesList component
              this.$router.push({ name: 'OpportunitiesList', params: { keywords: this.keywords } })
          },
          // combineFields method combines all filds to create the keywords string
          combineFields(){
              this.keywords = this.status
              if(this.major.length > 0){
                  this.major = this.major.replaceAll(" ", "%20")
                  if(this.keywords.length > 0){
                      this.keywords = this.keywords + "%20" + this.major
                  }
                  else{
                      this.keywords = this.major
                  }
              }

              if(this.cert.length > 0){
                  this.cert = this.cert.replaceAll(" ", "%20")
                  if(this.keywords.length > 0){
                       this.keywords = this.keywords + "%20" + this.cert
                  }
                  else{
                      this.keywords = this.cert
                  }
              }
              
              if(this.intern.length > 0){
                  this.intern = this.intern.replaceAll(" ", "%20")
                  if(this.keywords.length > 0){
                      this.keywords = this.keywords + "%20" + this.intern
                  }
                  else{
                      this.keywords = this.intern
                  }
              }
              

              if(this.research.length > 0){
                  this.research = this.research.replaceAll(" ", "%20")
                  if(this.keywords.length > 0){
                      this.keywords = this.keywords + "%20" + this.research
                  }
                  else{
                      this.keywords = this.research
                  }
              }
              

              if(this.graduated == true){
                  if(this.keywords.length > 0){
                      this.keywords = this.keywords + "%20" + "graduate"
                  }
                  else{
                      this.keywords = "graduate"
                  }
              }

              if(this.searchInternship == true){
                  if(this.keywords.length > 0){
                      this.keywords = this.keywords + "%20" + "computer%20science"
                  }
                  else{
                      this.keywords = "computer%20science"
                  }
              }

              if(this.keywords.length == 0){
                  this.keywords = "computer%20science"
              }
              
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
        width: 43%;
        height: 25px;
    }

    .right-half {
        float: right;
        width: 57%;
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
        width: 404px;
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
