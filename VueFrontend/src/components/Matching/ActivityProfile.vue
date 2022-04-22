<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Activity Profile !!!!! Select Activities to be matched for !!!
            <br />
            <br />

        </div>
        <div>To Select Multiple hold Ctrl key and Click On The Name:</div>
        <select v-model="mychoice" multiple>
            <option value="Studying">Studying</option>
            <option value="Exercising">Exercising</option>
            <option value="Get food (on/off campus)">Get food (on/off campus</option>
            <option value="Get food (dining hall)">Get food (dining hall)</option>
            <option value="Go off campus">Go off campus</option>
            <option value="Go to specific location on campus">Go to specific location on campus</option>
            <option value="Hang out">Hang out</option>
            <option value="Go to event">Go to event</option>
            <option value="Other">Other</option>
        </select>
        <div class="output">{{mychoice}}</div>
        <button @click="cleardata">Clear Data</button>

        <div>
            <input type="checkbox" id="study" value="study" v-model="checkedNames">
            <label for="study">Studying</label>
            <br />
            <input type="checkbox" id="exercise" value="exercise" v-model="checkedNames">
            <label for="exercise">Exercising</label>
            <br />
            <input type="checkbox" id="food_one" value="food_one" v-model="checkedNames">
            <label for="food_one">Get food (on/off campus)</label>
            <br />
            <input type="checkbox" id="food_two" value="food_two" v-model="checkedNames">
            <label for="food_two">Get food (dining hall)</label>
            <br />
            <input type="checkbox" id="offcampus" value="offcampus" v-model="checkedNames">
            <label for="offcampus">Go off campus</label>
            <br />
            <input type="checkbox" id="oncampus" value="oncampus" v-model="checkedNames">
            <label for="oncampus">Go to specific location on campus</label>
            <br />
            <input type="checkbox" id="hang" value="hang" v-model="checkedNames">
            <label for="hang">Hang out</label>
            <br />
            <input type="checkbox" id="event" value="event" v-model="checkedNames">
            <label for="event">Go to event</label>
            <br />
            <input type="checkbox" id="other" value="other" v-model="checkedNames">
            <label for="other">Other</label>
            <br>
            <span>Checked names: {{ checkedNames }}</span>
        </div>

        <div>
            <br>
            <button @click="save">Save</button>
            <button @click="onSubmit">Back</button>
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
                mychoice: [],
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
                this.mychoice = []
            },



        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>