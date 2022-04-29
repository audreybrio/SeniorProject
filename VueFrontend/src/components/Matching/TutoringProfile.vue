<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Tutoring Profile !!!!! Complete Tutoring information to be matched for !!!
            <br>
            <br>
        </div>

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

        <div>
            <br>
            Add up to six classes
        </div>
        <div>
            <input id="course1" name="tutoring" v-model="input1" placeholder="Course One">
        </div>
        <div>
            <input id="couse2" name="tutoring" v-model="input2" placeholder="Course Two">
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
    import router from '@/router'
    import jwt_decode from "jwt-decode"
    // import * as $ from 'jquery'
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
                input6: null

            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

            },

            onSubmit() {
                router.push({name: "matchingMain"})
            },

            save() {

                let courses = [];
                if (this.input1 != null) { courses.push(this.input1); }
                if (this.input2 != null) { courses.push(this.input2); }
                if (this.input3 != null) { courses.push(this.input3); }
                if (this.input4 != null) { courses.push(this.input4); }
                if (this.input5 != null) { courses.push(this.input5); }
                if (this.input6 != null) { courses.push(this.input6); }

                let requires = true;
                let individual = true;

                if (this.chosen == "offers") { requires = false; }
                if (this.picked == "group") { individual = false; }
                //$("input[name='tutoring']:checked").each(function () {
                //    courses.push(this.value);
                //});
                let data = {
                    courses: courses
                }
                console.log("data: ", data)
                console.log("activities: ", courses)
                console.log("group", this.picked)
                console.log("requires: ", this.chosen)
                fetch(
                    `${URLS.api.tutoringProfile.update}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${individual}/${requires}/${true}`, {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data),

                }).then(() => { router.push({ name: "matchingMain" }) });

            },




        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>