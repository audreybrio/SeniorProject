<!--<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Calculate your GPA !!!!
            <br>
            <br>
        </div>-->

        <!-- Warning Error for if something goes wrong -->
        <!--<div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <div>
            Enter number of classes took this semester:
            <input id="sem" name="sem" v-model="ssm" placeholder="Number of courses">
        </div>-->


        <!-- Radio buttons for user to select which type of setting they would want for tutoring-->
        <!--<div>
            Course 1
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="a" v-model="one">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="b" v-model="one">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="c" v-model="one">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="d" v-model="one">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="f" v-model="one">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>

            <input id="unit1" name="unit1" v-model="unit1" placeholder="Units">
            <br>
        </div>
        <div>
            Course 2
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="a" v-model="two">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="b" v-model="two">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="c" v-model="two">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="d" v-model="two">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="f" v-model="two">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit2" name="unit2" v-model="unit2" placeholder="Units">
            <br>
        </div>

        <div>
            Course 3
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="a" v-model="three">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="b" v-model="three">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="c" v-model="three">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="d" v-model="three">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="f" v-model="three">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit3" name="unit3" v-model="unit3" placeholder="Units">
            <br>
        </div>
        <div>
            Course 4
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="a" v-model="four">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="b" v-model="four">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="c" v-model="four">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="d" v-model="four">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="f" v-model="four">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit4" name="unit4" v-model="unit4" placeholder="Units">
            <br>
        </div>

        <div>
            Course 5
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="a" v-model="five">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="b" v-model="five">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="c" v-model="five">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="d" v-model="five">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="f" v-model="five">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit5" name="unit5" v-model="unit5" placeholder="Units">
            <br>
        </div>
        <div>
            Course 6
        </div>
        <div>
            Grade:&nbsp;
            <input type="radio" id="a" value="a" v-model="six">
            <label for="a">A&nbsp;&nbsp;</label>
            <input type="radio" id="b" value="b" v-model="six">
            <label for="b">B&nbsp;&nbsp;</label>
            <input type="radio" id="c" value="c" v-model="six">
            <label for="c">C&nbsp;&nbsp;</label>
            <input type="radio" id="d" value="d" v-model="six">
            <label for="d">D&nbsp;&nbsp;</label>
            <input type="radio" id="f" value="f" v-model="six">
            <label for="f">F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input id="unit6" name="unit6" v-model="unit6" placeholder="Units">
            <br>
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
   // import URLS from '../../variables'

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
                let grade = []
                if (unit1 != null) { unit.push(this.unit1) }
                if (unit2 != null) { unit.push(this.unit2) }
                if (unit3 != null) { unit.push(this.unit3) }
                if (unit4 != null) { unit.push(this.unit4) }
                if (unit5 != null) { unit.push(this.unit5) }
                if (unit6 != null) { unit.push(this.unit6) }


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
</style>-->