<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Tutoring Profile !!!!! Complete Tutoring information to be matched for !!!
            <br>
            <br>
        </div>

        <!-- Warning Error for if something goes wrong -->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- Radio buttons for user to select which type of setting they would want for tutoring-->
        <div>
            Looking For Group or Individual Tutoring?
        </div>
        <div>
            <input type="radio" id="group" value="group" v-model="picked">
            <label for="group">Group</label>
            <input type="radio" id="individual" value="individual" v-model="picked">
            <label for="individual">Individual</label>
            <br>
        </div>

        <!-- Radio buttons for user to select if a user is a tutor or looking for a tutor-->
        <div>
            <br>
            Are you looking for a tutor or offering tutoring?

        </div>

        <div>
            <input type="radio" id="offers" value="offers" v-model="chosen">
            <label for="offer">Offering Tutoring</label>
            <input type="radio" id="requires" value="requires" v-model="chosen">
            <label for="requires">Looking For a Tutor</label>
            <br>

        </div>

        <!-- User can add up to 6 courses-->
        <div>
            <br>
            Add up to six classes
        </div>
        <div>
            <input id="course1" name="tutoring" v-model="input1" placeholder="Course One">
        </div>
        <div>
            <input id="course2" name="tutoring" v-model="input2" placeholder="Course Two">
        </div>
        <div>
            <input id="course3" name="tutoring" v-model="input3" placeholder="Course Three">
        </div>
        <div>
            <input id="course4" name="tutoring" v-model="input4" placeholder="Course Four">
        </div>
        <div>
            <input id="course5" name="tutoring" v-model="input5" placeholder="Course Five">
        </div>
        <div>
            <input id="course6" name="tutoring" v-model="input6" placeholder="Course Six">
        </div>

        <!-- Selecting a schedule-->
        <div>
            <br />
            Select a schedule
        </div>
        <div>
            <input type="radio" id="one" value="one" v-model="select">
            <label for="one">Schedule A</label>
            <br>
        </div>

        <br />
        <button @click="save">Save</button>
        <button @click="onSubmit">Back</button>
    </div>
    <router-view />
</template>

<script lang="js">
    // Imports
    import router from '@/router'
    import jwt_decode from "jwt-decode"
    import URLS from '../../variables'

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                picked: null,
                chosen: null,
                select: null,
                input1: null,
                input2: null,
                input3: null,
                input4: null,
                input5: null,
                input6: null,
                errors: "",

            };
        },
        created() {
            this.fetchData();
        },
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

            },
            // Go back to main matching page
            onSubmit() {
                router.push({name: "matchingMain"})
            },

            // Save tutoring profile information
            save() {
                // Gets entered course information
                let courses = [];
                if (this.input1 != null) { courses.push(this.input1); }
                if (this.input2 != null) { courses.push(this.input2); }
                if (this.input3 != null) { courses.push(this.input3); }
                if (this.input4 != null) { courses.push(this.input4); }
                if (this.input5 != null) { courses.push(this.input5); }
                if (this.input6 != null) { courses.push(this.input6); }


                if (courses.length == 0 || this.picked == null || this.select == null || this.chosen == null) {
                    this.errors = "Missing Information"
                }

                else {
                    let requires = true;
                    let individual = true;
                    // Gets individual and requires values (if individual is true, they want individual,
                    // if individual is false they want group; if requres is true, they are looking for 
                    // a tutor, if requires is false, they are a tutor
                    if (this.chosen == "offers") { requires = false; }
                    if (this.picked == "group") { individual = false; }
                    let data = {
                        courses: courses
                    }
                    console.log("data: ", data)
                    console.log("activities: ", courses)
                    console.log("group", this.picked)
                    console.log("requires: ", this.chosen)
                    // Connection to backend
                    fetch(
                        `${URLS.api.tutoringProfile.update}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${individual}/${requires}/${true}`, {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data),

                    }).then((response) => {
                        if (!response.ok) {

                            console.log("error")
                            this.errors = "Error";
                        }
                        else {
                            console.log("success")
                            router.push({ name: "matchingMain" })
                        }
                    })
                }

            },
        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
    .warning {
        color: red;
        margin: auto;
        width: 440px;
        text-align: center;
        font-size: 11px;
    }
</style>