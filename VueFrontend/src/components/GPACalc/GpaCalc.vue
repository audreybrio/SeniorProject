<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Calculate your GPA !!!!
            <br>
            <br>
        </div>
        <!-- For Errors-->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- Course 1 grade and units-->
        <div>
            Course 1
        </div>
        <div>
            Grade:&nbsp;
            <select v-model="one">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit1" name="unit1" v-model="unit1" placeholder="Units">
            <br>
        </div>
        <!-- Course 2 grade and units-->
        <div>
            Course 2
        </div>
        <div>
            Grade:&nbsp;

            <select v-model="two">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit2" name="unit2" v-model="unit2" placeholder="Units">
            <br>
        </div>

        <!-- Course 3 grade and units-->
        <div v-if="show3 == true">
            Course 3
            <br />
            Grade:&nbsp;
            <select v-model="three">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit3" name="unit3" v-model="unit3" placeholder="Units">
            <br>
        </div>
        <!-- Course 4 grade and units-->
        <div v-if="show4 == true">
            Course 4
            <br />
            Grade:&nbsp;
            <select v-model="four">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit4" name="unit4" v-model="unit4" placeholder="Units">
            <br>
        </div>
        <!-- Course 5 grade and units-->
        <div v-if="show5 == true">
            Course 5
            <br />
            Grade:&nbsp;
            <select v-model="five">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit5" name="unit5" v-model="unit5" placeholder="Units">
            <br>
        </div>
        <!-- Course 6 grade and units-->
        <div v-if="show6 == true">
            Course 6
            <br />
            Grade:&nbsp;

            <select v-model="six">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit6" name="unit6" v-model="unit6" placeholder="Units">
            <br>
        </div>

        <!-- Course 7 grade and units-->
        <div v-if="show7 == true">
            Course 7
            <br />
            Grade:&nbsp;

            <select v-model="seven">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit7" name="unit7" v-model="unit7" placeholder="Units">
            <br>
        </div>

        <!-- Course 8 grade and units-->
        <div v-if="show8 == true">
            Course 8
            <br />
            Grade:&nbsp;

            <select v-model="eight">
                <option disabled value="">Select Grade</option>
                <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>
            </select>
            &nbsp;
            <input id="unit8" name="unit8" v-model="unit8" placeholder="Units">
            <br>
        </div>

        <!-- Add/Remove classes-->
        <div>
            <br />
            <button @click="showMore">Add Another Class</button>
            <button @click="showLess">Remove a Class</button>
        </div>
        <!-- Dislay GPA-->
        <div>
            <br />
            <div v-if="gpa!=0 && gpa!=null" :key="index" class="gpa">GPA is: {{gpa}}</div>
        </div>

        <br />
        <!-- Calculate GPA and go back-->
        <button @click="save">Calculate GPA!</button>
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
                one: null,
                two: null,
                three: null,
                four: null,
                five: null,
                six: null,
                errors: "",
                unit1: null, unit2: null, unit3: null, unit4: null, unit5: null, unit6: null, unit7: null, unit8: null,
                gpa: 0,
                show3: true, show4: true, show5: true,
                show6: false, show7: false, show8: false,
                possibleGrades: ["A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "D-", "F"]

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
            // Go back to main calculator main page
            onSubmit() {
                router.push({name: "calculatorMain"})
            },

            // Get information entered from user
            save() {
                this.errors = ""
                console.log(this.test)
                let unit = []

                if (this.unit1 != null && this.unit1 != '') { unit.push(this.unit1) }
                if (this.unit2 != null && this.unit2 != '') { unit.push(this.unit2) }
                if (this.unit3 != null && this.unit3 != '') { unit.push(this.unit3) }
                if (this.unit4 != null && this.unit4 != '') { unit.push(this.unit4) }
                if (this.unit5 != null && this.unit5 != '') { unit.push(this.unit5) }
                if (this.unit6 != null && this.unit6 != '') { unit.push(this.unit6) }
                if (this.unit7 != null && this.unit7 != '') { unit.push(this.unit7) }
                if (this.unit8 != null && this.unit8 != '') { unit.push(this.unit8) }

                let grade = []
                if (this.one != null) { grade.push(this.one) }
                if (this.two != null) { grade.push(this.two) }
                if (this.three != null) { grade.push(this.three) }
                if (this.four != null) { grade.push(this.four) }
                if (this.five != null) { grade.push(this.five) }
                if (this.six != null) { grade.push(this.six) }
                if (this.seven != null) { grade.push(this.seven) }
                if (this.eight != null) { grade.push(this.eight) }
                // Make sure grades and unit are both inputed
                if (unit.length != grade.length) {
                    this.errors = "Error: Missing Information"
                }
                // Calculates gpa
                else {
                    console.log(unit)
                    let data = {
                        unit: unit,
                        grade: grade
                    }
                    console.log("Data: ", data)

                    fetch(
                        `${URLS.api.gpaCalc.calculateGPA}`, {
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
                        this.gpa = JSON.stringify(result);
                        if (this.gpa != undefined) {
                            console.log("gpa", this.gpa)
                        }
                        else {
                            console.log("error")
                        }

                    })
                }


            },
            // Show more classes
            showMore() {
                this.errors = ""
                if (this.show3 == false) { this.show3 = true; }
                else if (this.show4 == false) { this.show4 = true; }
                else if (this.show5 == false) { this.show5 = true; }
                else if (this.show6 == false) { this.show6 = true; }
                else if (this.show7 == false) { this.show7 = true; }
                else if (this.show8 == false) { this.show8 = true }
                else { this.errors = "Max Classes Allowed" }
            },
            // Show less classes
            showLess() {
                this.errors = ""
                if (this.show8 == true) { this.show8 = false; this.eight = null; this.unit8 = null }
                else if (this.show7 == true) { this.show7 = false; this.seven = null; this.unit7 = null }
                else if (this.show6 == true) { this.show6 = false; this.six = null; this.unit6 = null }
                else if (this.show5 == true) { this.show5 = false; this.five = null; this.unit5 = null }
                else if (this.show4 == true) { this.show4 = false; this.four = null; this.unit4 = null }
                else if (this.show3 == true) { this.show3 = false; this.three = null; this.unit3 = null }
                else { this.errors = "Cannot remove any more courses" }
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

    .gpa {
        margin: auto;
        width: 440px;
        text-align: center;
        font-size: 22px;
        font-weight: bold;
    }
</style>