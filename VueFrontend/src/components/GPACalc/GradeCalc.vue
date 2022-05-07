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

        <div>
            Insert A Name And Section for this course (only need if saving)&nbsp;&nbsp;
            <input id="course" name="course" v-model="course" placeholder="Course Name">&nbsp;
            <input id="section" name="section" v-model="section" placeholder="Section #">
        </div>

        <!-- User can add up to 20 assignments-->
        <div>

            <br>
            How Many Assignements do you have? (#)
            <select v-model="calcGrade">
                <option disabled value="">Number of Assignments</option>
                <option v-for="num in totalAssignments" :key="num">{{ num }}</option>
            </select>
            &nbsp;
            <button @click="go">Enter </button>
        </div>


        <div v-if="isVisible == true">
            <span v-for="n in assignments" :key="n">
                <br />
                Points Received: &nbsp;
                <input id="assign" name="assign" v-model="assign[n-1]" placeholder="Assignment">&nbsp;
                Out of: &nbsp;
                <input id="value" name="value" v-model="value[n-1]" placeholder="Total Points">
            </span>
        </div>

  
    <div>
        <br />
        <div v-if="grade!=0 && grade!=null" :key="index" class="grade">Grade is: {{grade}}</div>
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
    import URLS from '../../variables'

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                errors: "",
                total: null, course: null,
                grade: 0, section: null,
                calcGrade: null, isVisible: false,
                totalAssignments: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"],
                assignments: 0, assign: [], value: [],

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
                this.errors = ""
                let assignments = [];
                let points = [];

                console.log("assign: ", this.assign)
                console.log("pints: ", this.value)

                let i = 0
                let j = 0

                for (i; i < this.assign.length; i++) {
                    if (this.assign[i] != "") {
                        assignments.push(this.assign[i])
                    }
                    
                }

                for (j; j < this.value.length; j++) {
                    if (this.value[j] != "") {
                        points.push(this.value[j])
                    }
                   
                }

                if (assignments.length != points.length) {
                    this.errors = "Error: Missing Information"
                }
                // Calculates grade
                else {
                    console.log(assignments)
                    let data = {
                        assignments: assignments,
                        points: points
                    }
                    console.log("Data: ", data)
                    fetch(
                        `${URLS.api.gradeCalc.calculateGrade}`, {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data),

                    }).then((response) => {
                        if (!response.ok) {

                            console.log("error")
                            this.errors = "Calculation Error"
                        }
                        else {
                            console.log("ajax success")
                            return response.json()

                        }
                    }).then((result) => {
                        console.log(result)
                        this.grade = JSON.stringify(result);
                        if (this.grade != undefined && this.grade != 0) {
                            console.log("grade", this.grade)
                        }
                        else {
                            this.errors = "ERROR: Input Error"
                            console.log("error")
                        }

                    })
                }
            },

            save() {
                this.errors = ""
                let assignments = [];
                let points = [];
                let i = 0
                let j = 0
                for (i; i < this.assign.length; i++) {
                    if (this.assign[i] != "") {
                        assignments.push(this.assign[i])
                    }
                }
                for (j; j < this.value.length; j++) {
                    if (this.value[j] != "") {
                        points.push(this.value[j])
                    }
                }
                if (this.course == null) {
                    this.errors = "Error: Missing Course"
                }
                if (this.section == null) {
                    this.errors = "Error: Missing Section"
                }
                else if (assignments.length != points.length) {
                    this.errors = "Error: Missing Information"
                }
                else {
                    console.log(assignments)
                    let data = {
                        assignments: assignments,
                        points: points
                    }
                    console.log("Data: ", data)
                    fetch(
                        `${URLS.api.gradeCalc.saveGrade}/${this.id}/${this.course}/${this.section}`, {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data),

                    }).then((response) => {
                        if (!response.ok) {

                            console.log("error")
                            this.errors = "Error: Something went wrong"
                        }
                        else {
                            console.log("ajax success")
                            return response.json()

                        }
                    }).then((result) => {
                        console.log(result)
                        this.grade = JSON.stringify(result);
                        if (this.grade != undefined && this.grade != 0) {
                            console.log("grade", this.grade)
                        }
                        else {
                            this.errors = "ERROR: Input Error"
                            console.log("error")
                        }

                    })
                    
                }

            },

            go() {
                this.errors=""
                this.isVisible = true
                this.assign = []
                this.value = []
                if (this.calcGrade == "1") { this.assignments = 1 }
                if (this.calcGrade == "2") { this.assignments = 2 }
                if (this.calcGrade == "3") { this.assignments = 3 }
                if (this.calcGrade == "4") { this.assignments = 4 }
                if (this.calcGrade == "5") { this.assignments = 5 }
                if (this.calcGrade == "6") { this.assignments = 6 }
                if (this.calcGrade == "7") { this.assignments = 7 }
                if (this.calcGrade == "8") { this.assignments = 8 }
                if (this.calcGrade == "9") { this.assignments = 9 }
                if (this.calcGrade == "10") { this.assignments = 10 }
                if (this.calcGrade == "11") { this.assignments = 11 }
                if (this.calcGrade == "12") { this.assignments = 12 }
                if (this.calcGrade == "13") { this.assignments = 13 }
                if (this.calcGrade == "14") { this.assignments = 14 }
                if (this.calcGrade == "15") { this.assignments = 15 }
                if (this.calcGrade == "16") { this.assignments = 16 }
                if (this.calcGrade == "17") { this.assignments = 17 }
                if (this.calcGrade == "18") { this.assignments = 18 }
                if (this.calcGrade == "19") { this.assignments = 19 }
                if (this.calcGrade == "20") { this.assignments = 20 }

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
    .grade {
        margin: auto;
        width: 440px;
        text-align: center;
        font-size: 22px;
        font-weight: bold;
    }
</style>