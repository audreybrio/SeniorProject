<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Activity Profile !!!!! Select Activities to be matched for !!!
            <br />
            <br />

        </div>

        <div>
            <!-- Checkboxes for the different activities a user can select; user can only select up to 5-->
            <input type="checkbox" id="study" name="activity" value="Studying" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Studying') == -1">
            <label for="study">Studying</label>
            <br />
            <input type="checkbox" id="exercise" name="activity" value="Exercising" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Exercising') == -1">
            <label for="exercise">Exercising</label>
            <br />
            <input type="checkbox" id="food_one" name="activity" value="Get food (on/off campus)" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Get food (on/off campus)') == -1">
            <label for="food_one">Get food (on/off campus)</label>
            <br />
            <input type="checkbox" id="food_two" name="activity" value="Get food (dining hall)" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Get food (dining hall)') == -1">
            <label for="food_two">Get food (dining hall)</label>
            <br />
            <input type="checkbox" id="offcampus" name="activity" value="Go off campus" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Go off campus') == -1">
            <label for="offcampus">Go off campus</label>
            <br />
            <input type="checkbox" id="oncampus" name="activity" value="Go to specific location on campus" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Go to specific location on campus') == -1">
            <label for="oncampus">Go to specific location on campus</label>
            <br />
            <input type="checkbox" id="hang" name="activity" value="Hang out" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Hang out') == -1">
            <label for="hang">Hang out</label>
            <br />
            <input type="checkbox" id="event" name="activity" value="Go to event" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Go to event') == -1">
            <label for="event">Go to event</label>
            <br />
            <input type="checkbox" id="other" name="activity" value="Other" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Other') == -1">
            <label for="other">Other</label>

            <br>
        </div>

        <div>
            <!-- Gets schedule selection-->
            <br />
            Select a schedule
        </div>
        <div>
            <input type="radio" id="one" value="one" v-model="picked">
            <label for="one">Schedule A</label>
        </div>

         <div>
            <br>
            <button @click="save">Save</button>
            <button @click="onSubmit">Back</button>
            <button @click="cleardata">Clear Selections</button>
        </div>

    </div>
    <router-view />
</template>

<script lang="js">
    // Imports
    import router from '@/router'
    import jwt_decode from "jwt-decode"
    import * as $ from 'jquery'
    import URLS from '../../variables'

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                checkedNames: [],
                picked: null
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
           // Goes back to main matching page
            onSubmit() {
                router.push({name: "matchingMain"})
            },
            // Saves the selected data a user has entered 
            save() {
                // Gets selected activities
                let activities = [];
                $("input[name='activity']:checked").each(function () {
                    activities.push(this.value);
                });

                let data = {
                    activities: activities
                }
                console.log("data: ", data)
                console.log("activities: ", activities)
                // Connection to backend 
                fetch(
                    `${URLS.api.activityProfile.update}/${jwt_decode(window.sessionStorage.getItem("token")).username}/${true}`, {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                        body: JSON.stringify(data),

                }).then(() => { router.push({name: "matchingMain"}) });
                

            },

            cleardata() {
                console.log("Clear Data")
                this.checkedNames = []
                this.picked = null
            },



        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>