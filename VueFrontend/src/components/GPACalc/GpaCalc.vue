<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Calculate your GPA !!!!
            <br>
            <br>
        </div>

        <!-- Warning Error for if something goes wrong -->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- Radio buttons for user to select which type of setting they would want for tutoring-->
        <div>
            Course 1
        </div>
        <div>
            <input type="radio" id="a" value="a" v-model="one">
            <label for="a">A</label>
            <input type="radio" id="b" value="b" v-model="one">
            <label for="b">B</label>
            <input type="radio" id="c" value="c" v-model="one">
            <label for="c">C</label>
            <input type="radio" id="d" value="d" v-model="one">
            <label for="d">D</label>
            <input type="radio" id="f" value="f" v-model="one">
            <label for="f">F</label>
            <br>
        </div>
        <div>
            Course 2
        </div>
        <div>
            <input type="radio" id="a" value="a" v-model="two">
            <label for="a">A</label>
            <input type="radio" id="b" value="b" v-model="two">
            <label for="b">B</label>
            <input type="radio" id="c" value="c" v-model="two">
            <label for="c">C</label>
            <input type="radio" id="d" value="d" v-model="two">
            <label for="d">D</label>
            <input type="radio" id="f" value="f" v-model="two">
            <label for="f">F</label>
            <br>
        </div>

        <div>
            Course 3
        </div>
        <div>
            <input type="radio" id="a" value="a" v-model="three">
            <label for="a">A</label>
            <input type="radio" id="b" value="b" v-model="three">
            <label for="b">B</label>
            <input type="radio" id="c" value="c" v-model="three">
            <label for="c">C</label>
            <input type="radio" id="d" value="d" v-model="three">
            <label for="d">D</label>
            <input type="radio" id="f" value="f" v-model="three">
            <label for="f">F</label>
            <br>
        </div>
        <div>
            Course 4
        </div>
        <div>
            <input type="radio" id="a" value="a" v-model="four">
            <label for="a">A</label>
            <input type="radio" id="b" value="b" v-model="four">
            <label for="b">B</label>
            <input type="radio" id="c" value="c" v-model="four">
            <label for="c">C</label>
            <input type="radio" id="d" value="d" v-model="four">
            <label for="d">D</label>
            <input type="radio" id="f" value="f" v-model="four">
            <label for="f">F</label>
            <br>
        </div>

        <div>
            Course 5
        </div>
        <div>
            <input type="radio" id="a" value="a" v-model="five">
            <label for="a">A</label>
            <input type="radio" id="b" value="b" v-model="five">
            <label for="b">B</label>
            <input type="radio" id="c" value="c" v-model="five">
            <label for="c">C</label>
            <input type="radio" id="d" value="d" v-model="five">
            <label for="d">D</label>
            <input type="radio" id="f" value="f" v-model="five">
            <label for="f">F</label>
            <br>
        </div>
        <div>
            Course 6
        </div>
        <div>
            <input type="radio" id="a" value="a" v-model="six">
            <label for="a">A</label>
            <input type="radio" id="b" value="b" v-model="six">
            <label for="b">B</label>
            <input type="radio" id="c" value="c" v-model="six">
            <label for="c">C</label>
            <input type="radio" id="d" value="d" v-model="six">
            <label for="d">D</label>
            <input type="radio" id="f" value="f" v-model="six">
            <label for="f">F</label>
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