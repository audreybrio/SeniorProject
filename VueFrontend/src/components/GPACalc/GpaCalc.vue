<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Calculate your GPA !!!!
            <br>
            <br>
        </div>

        <div class="warning">
            <div v-if="gpa!=0 &&gpa!=null" :key="index" class="warning">{{errors}}</div>
        </div>

        <!--<div>
        Enter number of classes took this semester:
        <input id="sem" name="sem" v-model="ssm" placeholder="Number of courses">
    </div>-->
        <div>
            Course 1
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="4" v-model="one">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="3" v-model="one">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="2" v-model="one">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="1" v-model="one">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="0" v-model="one">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>

            <input id="unit1" name="unit1" v-model="unit1" placeholder="Units">
            <br>
        </div>
        <div>
            Course 2
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="4" v-model="two">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="3" v-model="two">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="2" v-model="two">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="1" v-model="two">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="0" v-model="two">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit2" name="unit2" v-model="unit2" placeholder="Units">
            <br>
        </div>

        <div>
            Course 3
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="4" v-model="three">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="3" v-model="three">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="2" v-model="three">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="1" v-model="three">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="0" v-model="three">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit3" name="unit3" v-model="unit3" placeholder="Units">
            <br>
        </div>
        <div>
            Course 4
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="4" v-model="four">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="3" v-model="four">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="2" v-model="four">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="1" v-model="four">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="0" v-model="four">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit4" name="unit4" v-model="unit4" placeholder="Units">
            <br>
        </div>

        <div>
            Course 5
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="4" v-model="five">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="3" v-model="five">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="2" v-model="five">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="1" v-model="five">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="0" v-model="five">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit5" name="unit5" v-model="unit5" placeholder="Units">
            <br>
        </div>
        <div>
            Course 6
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="4" v-model="six">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="3" v-model="six">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="2" v-model="six">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="1" v-model="six">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="0" v-model="six">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit6" name="unit6" v-model="unit6" placeholder="Units">
            <br>
        </div>
        
        <select v-model="test">
            <option disabled value="">Select Grade</option>
            <option v-for="grade in possibleGrades" :key="grade">{{ grade }}</option>

        </select>

        <div>
            <br />
            <div v-if="gpa.length" :key="index">GPA is: {{gpa}}</div>
        </div>

        <br />

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
                unit1: null, unit2: null, unit3: null, unit4: null, unit5: null, unit6: null,
                gpa: 0,
                test: null,
                possibleGrades: ["A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-"]

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
                router.push({name: "calculatorMain"})
            },

            // Save tutoring profile information
            save() {
                let unit = []

                if (this.unit1 != null && this.unit1 != '') { unit.push(this.unit1) }
                if (this.unit2 != null && this.unit2 != '') { unit.push(this.unit2) }
                if (this.unit3 != null && this.unit3 != '') { unit.push(this.unit3) }
                if (this.unit4 != null && this.unit4 != '') { unit.push(this.unit4) }
                if (this.unit5 != null && this.unit5 != '') { unit.push(this.unit5) }
                if (this.unit6 != null && this.unit6 != '') { unit.push(this.unit6) }

                let grade = []
                if (this.one != null) { grade.push(this.one) }
                if (this.two != null) { grade.push(this.two) }
                if (this.three != null) { grade.push(this.three) }
                if (this.four != null) { grade.push(this.four) }
                if (this.five != null) { grade.push(this.five) }
                if (this.six != null) { grade.push(this.six) }

                if (unit.length != grade.length) {
                    this.errors = "Error: Missing Information"
                }
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