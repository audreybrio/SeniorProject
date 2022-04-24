<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Activity Profile !!!!! Select Activities to be matched for !!!
            <br />
            <br />

        </div>

        <div>
            <input type="checkbox" id="study" value="Studying" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Studying') == -1" >
            <label for="study">Studying</label>
            <br />
            <input type="checkbox" id="exercise" value="Exercising" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Exercising') == -1">
            <label for="exercise">Exercising</label>
            <br />
            <input type="checkbox" id="food_one" value="Get food (on/off campus)" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Get food (on/off campus)') == -1">
            <label for="food_one">Get food (on/off campus)</label>
            <br />
            <input type="checkbox" id="food_two" value="Get food (dining hall)" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Get food (dining hall)') == -1">
            <label for="food_two">Get food (dining hall)</label>
            <br />
            <input type="checkbox" id="offcampus" value="Go off campus" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Go off campus') == -1">
            <label for="offcampus">Go off campus</label>
            <br />
            <input type="checkbox" id="oncampus" value="Go to specific location on campus" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Go to specific location on campus') == -1">
            <label for="oncampus">Go to specific location on campus</label>
            <br />
            <input type="checkbox" id="hang" value="Hang out" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Hang out') == -1">
            <label for="hang">Hang out</label>
            <br />
            <input type="checkbox" id="event" value="Go to event" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Go to event') == -1">
            <label for="event">Go to event</label>
            <br />
            <input type="checkbox" id="other" value="Other" v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf('Other') == -1">
            <label for="other">Other</label>

            <br>
            <span>Checked names: {{ checkedNames }}</span>
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
    import router from '@/router'
    import jwt_decode from "jwt-decode"



    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                checkedNames: []
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

                fetch('https://studentmultitool.me:5001/weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });

            },
          
            onSubmit() {
                router.push({name: "matchingMain"})
            },

            save() {

            },

            cleardata() {
                console.log("Clear Data")
                this.checkedNames = []
            },



        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>