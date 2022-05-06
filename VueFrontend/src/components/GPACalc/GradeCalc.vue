<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Calculate your grade in a class !!!!
            <br>
            <br>
        </div>

        <!-- Warning Error for if something goes wrong -->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- User can add up to 6 courses-->
        <div>
            <br>
            How Many Assignements do you have? (#)
            <input id="total" name="total" v-model="total" placeholder="Total Assingments">
            <button @click="enter">Enter</button>
            <br />
            <input id="course" name="course" v-model="course" placeholder="Course Name">
        </div>


        <div v-if="one == true">
            <input id="assign1" name="assign" v-model="assign1" placeholder="Assignment 1 Points Received">
            <input id="value1" name="value" v-model="value1" placeholder="Total Points">
        </div>
        <div v-if="two == true">
            <input id="assign2" name="assign" v-model="assign2" placeholder="Assignment 2 Points Received">
            <input id="value2" name="value" v-model="value2" placeholder="Total Points">
        </div>
        <div v-if="three == true">
            <input id="assign3" name="assign" v-model="assign3" placeholder="Assignment 3 Points Received">
            <input id="value3" name="value" v-model="value3" placeholder="Total Points">
        </div>
        <div v-if="four == true">
            <input id="assign4" name="assign" v-model="assign4" placeholder="Assignment 4 Points Received">
            <input id="value4" name="value" v-model="value4" placeholder="Total Points">
        </div>
        <div v-if="five == true">
            <input id="assign5" name="assign" v-model="assign5" placeholder="Assignment 5 Points Received">
            <input id="value5" name="value" v-model="value5" placeholder="Total Points">
        </div>
        <div v-if="six == true">
            <input id="assign6" name="assign" v-model="assign6" placeholder="Assignment 6 Points Received">
            <input id="value6" name="value" v-model="value6" placeholder="Total Points">
        </div>

        <div v-if="seven == true">
            <input id="assign7" name="assign" v-model="assign7" placeholder="Assignment 7 Points Received">
            <input id="value7" name="value" v-model="value7" placeholder="Total Points">
        </div>
        <div v-if="eight == true">
            <input id="assign8" name="assign" v-model="assign8" placeholder="Assignment 8 Points Received">
            <input id="value8" name="value" v-model="value8" placeholder="Total Points">
        </div>
        <div v-if="nine == true">
            <input id="assign9" name="assign" v-model="assign9" placeholder="Assignment 9 Points Received">
            <input id="value9" name="value" v-model="value9" placeholder="Total Points">
        </div>
        <div v-if="ten == true">
            <input id="assign10" name="assign" v-model="assign10" placeholder="Assignment 10 Points Received">
            <input id="value10" name="value" v-model="value10" placeholder="Total Points">
        </div>
        <div v-if="eleven == true"> 
            <input id="assign11" name="assign" v-model="assign11" placeholder="Assignment 11 Points Received">
            <input id="value11" name="value" v-model="value11" placeholder="Total Points">
        </div>
        <div v-if="twelve == true">
            <input id="assign12" name="assign" v-model="assign12" placeholder="Assignment 12 Points Received">
            <input id="value12" name="value" v-model="value12" placeholder="Total Points">
        </div>

        <div v-if="thirteen == true">
            <input id="assign13" name="assign" v-model="assign13" placeholder="Assignment 13 Points Received">
            <input id="value13" name="value" v-model="value13" placeholder="Total Points">
        </div>
        <div v-if="fourteen == true">
            <input id="assign14" name="assign" v-model="assign14" placeholder="Assignment 14 Points Received">
            <input id="value14" name="value" v-model="value14" placeholder="Total Points">
        </div>
        <div v-if="fifteen == true">
            <input id="assign15" name="assign" v-model="assign15" placeholder="Assignment 15 Points Received">
            <input id="value15" name="value" v-model="value15" placeholder="Total Points">
        </div>
        <div v-if="sixteen == true">
            <input id="assign16" name="assign" v-model="assign16" placeholder="Assignment 16 Points Received">
            <input id="value16" name="value" v-model="value16" placeholder="Total Points">
        </div>
        <div v-if="seventeen == true">
            <input id="assign17" name="assign" v-model="assign17" placeholder="Assignment 17 Points Received">
            <input id="value17" name="value" v-model="value17" placeholder="Total Points">
        </div>
        <div v-if="eighteen == true">
            <input id="assign18" name="assign" v-model="assign18" placeholder="Assignment 18 Points Received">
            <input id="value18" name="value" v-model="value18" placeholder="Total Points">
        </div>


        <br />
        <button @click="calculate">Calculate Grade!</button>
        <button @click="save">Save Grade</button>
        <button @click="onSubmit">Back</button>
    </div>
    <router-view />
</template>

<script lang="js">
    // Imports
    import router from '@/router'
    import jwt_decode from "jwt-decode"
/*    import URLS from '../../variables'*/

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                assign1: null, assign2: null, assign3: null, assign4: null,
                assign5: null, assign6: null, assign7: null, assign8: null,
                assign9: null, assign10: null, assign11: null, assign12: null,
                assign13: null, assign14: null, assign15: null, assign16: null, assign17: null,  assign18: null,
                value1: null, value2: null, value3: null, value4: null,
                value5: null, value6: null,  value7: null, value8: null,
                value9: null, value10: null, value11: null, value12: null,
                value13: null, value14: null, value15: null, value16: null, value17: null, value18: null,
                errors: "",
                total: null, course: null,
                one: false, two: false, three: false, four: false, five: false, six: false, seven: false, eight: false,
                nine: false, ten: false, eleven: false, twelve: false, thirteen: false, fourteen: false, fifteen: false,
                sixteen: false, seventeen: false, eighteen: false

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
                router.push({ name: "calculatorMain"})
            },

            // Save tutoring profile information
            calculate() {
                // Gets entered course information
                let assignments = [];
                let points = [];
                if (this.assign1 != null) { assignments.push(this.assign1); }
                if (this.assing2 != null) { assignments.push(this.assign2); }
                if (this.assign3 != null) { assignments.push(this.assign3); }
                if (this.assign4 != null) { assignments.push(this.assign4); }
                if (this.assign5 != null) { assignments.push(this.assign5); }
                if (this.assign6 != null) { assignments.push(this.assign6); }
                if (this.assign7 != null) { assignments.push(this.assign7); }
                if (this.assign8 != null) { assignments.push(this.assign8); }
                if (this.assign9 != null) { assignments.push(this.assign9); }
                if (this.assign10 != null) { assignments.push(this.assign10); }
                if (this.assign11 != null) { assignments.push(this.assign11); }
                if (this.assign12 != null) { assignments.push(this.assign12); }
                if (this.assign13 != null) { assignments.push(this.assign13); }
                if (this.assign14 != null) { assignments.push(this.assign14); }
                if (this.assign15 != null) { assignments.push(this.assign15); }
                if (this.assign16 != null) { assignments.push(this.assign16); }
                if (this.assign17 != null) { assignments.push(this.assign17); }
                if (this.assign18 != null) { assignments.push(this.assign18); }
                if (this.value1 != null) { points.push(this.value1); }
                if (this.value2 != null) { points.push(this.value2); }
                if (this.value3 != null) { points.push(this.value3); }
                if (this.value4 != null) { points.push(this.value4); }
                if (this.value5 != null) { points.push(this.value5); }
                if (this.value6 != null) { points.push(this.value6); }
                if (this.value7 != null) { points.push(this.value7); }
                if (this.value8 != null) { points.push(this.value8); }
                if (this.value9 != null) { points.push(this.value9); }
                if (this.value10 != null) { points.push(this.value10); }
                if (this.value11 != null) { points.push(this.value11); }
                if (this.value12 != null) { points.push(this.value12); }
                if (this.value13 != null) { points.push(this.value13); }
                if (this.value14 != null) { points.push(this.value14); }
                if (this.value15 != null) { points.push(this.value15); }
                if (this.value16 != null) { points.push(this.value16); }
                if (this.value17 != null) { points.push(this.value17); }
                if (this.value18 != null) { points.push(this.value18); }

                console.log("assign: ", this.assignments)
                console.log("pints: ", this.points)


                //if (courses.length == 0 || this.picked == null || this.select == null || this.chosen == null) {
                //    this.errors = "Missing Information"
                //}

                //else {
                //    let requires = true;
                //    let individual = true;
                //    // Gets individual and requires values (if individual is true, they want individual,
                //    // if individual is false they want group; if requres is true, they are looking for
                //    // a tutor, if requires is false, they are a tutor
                //    if (this.chosen == "offers") { requires = false; }
                //    if (this.picked == "group") { individual = false; }
                //    let data = {
                //        courses: courses
                //    }
                //    console.log("data: ", data)
                //    console.log("activities: ", courses)
                //    console.log("group", this.picked)
                //    console.log("requires: ", this.chosen)
                //    // Connection to backend
                //    fetch(
                //        `${URLS.api.tutoringProfile.update}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${individual}/${requires}/${true}`, {
                //        method: 'POST',
                //        headers: {
                //            'Accept': 'application/json',
                //            'Content-Type': 'application/json'
                //        },
                //        body: JSON.stringify(data),

                //    }).then((response) => {
                //        if (!response.ok) {

                //            console.log("error")
                //            this.errors = "Error";
                //        }
                //        else {
                //            console.log("success")
                //            router.push({ name: "matchingMain" })
                //        }
                //    })
                //}

            },

            save() {
                if (this.course == null) {
                    this.errors = "Enter a course"
                }
                else {
                    // fetch to db, enter in course and grade 
                }

            },

            enter() {
                if (this.total == "1") {
                    this.one = true; this.two = false; this.three = false; this.four = false; this.five = false; this.six = false;
                    this.seven = false; this.eight = false; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "2") {
                    this.one = true; this.two = true; this.three = false; this.four = false; this.five = false; this.six = false;
                    this.seven = false; this.eight = false; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "3") {
                    this.one = true; this.two = true; this.three = true; this.four = false; this.five = false; this.six = false;
                    this.seven = false; this.eight = false; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "4") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = false; this.six = false;
                    this.seven = false; this.eight = false; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "5") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = false;
                    this.seven = false; this.eight = false; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "6") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = false; this.eight = false; this.nine = false; this.ten = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "7") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = false; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "8") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = false; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "9") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = false; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "10") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = false; this.twelve = false; this.thirteen = false;
                    this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "11") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = false;
                    this.thirteen = false; this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "12") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = false; this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "13") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = true; this.fourteen = false; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "14") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = true; this.fourteen = true; this.fifteen = false; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "15") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = true; this.fourteen = true; this.fifteen = true; this.sixteen = false; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "16") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = true; this.fourteen = true; this.fifteen = true; this.sixteen = true; this.seventeen = false; this.eighteen = false;
                }
                else if (this.total == "17") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = true; this.fourteen = true; this.fifteen = true; this.sixteen = true; this.seventeen = true; this.eighteen = false;
                }
                else if (this.total == "18") {
                    this.one = true; this.two = true; this.three = true; this.four = true; this.five = true; this.six = true;
                    this.seven = true; this.eight = true; this.nine = true; this.ten = true; this.eleven = true; this.twelve = true;
                    this.thirteen = true; this.fourteen = true; this.fifteen = true; this.sixteen = true; this.seventeen = true; this.eighteen = true;
                }
                else {
                    this.errors = "Must be less than 18"
                }

            }
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