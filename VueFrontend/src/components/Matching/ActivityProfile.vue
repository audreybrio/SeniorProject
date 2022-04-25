<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Activity Profile !!!!! Select Activities to be matched for !!!
            <br />
            <br />

        </div>

        <div>
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
            <!--<span>Checked names: {{ checkedNames }}</span>-->
        </div>

       
        <!--<div v-for="itm in items" :key="itm.name">
            <input type="checkbox" class="checkbox" value=itm.name v-model="checkedNames" :disabled="checkedNames.length >= 5 && checkedNames.indexOf(itm.name) == -1" />
            {{ itm.name }}
            <br />
        </div>
        <span>Checked names: {{ checkedNames }}</span>-->
    
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
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";



    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                checkedNames: [],
                items: [
                    { name: "Studying" },
                    { name: "Exercising" },
                    { name: "Get food (on/off campus)" },
                    { name: "Get food (dining hall)" },
                    { name: "Go off campus" },
                    { name: "Go to specific location on campus" },
                    { name: "Hang out" },
                    { name: "Go to event" },
                    { name: "Other" }
                ],
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

                //fetch('https://studentmultitool.me:5001/weatherforecast')
                //    .then(r => r.json())
                //    .then(json => {
                //        this.post = json;
                //        this.loading = false;
                //        return;
                //    });

            },
          
            onSubmit() {
                router.push({name: "matchingMain"})
            },

            save() {
                // var act = new Array();
                //$("input[name='checkedNames']:checked").each(function () {
                ////    this.checkedNames.push(this.value);
                //});
                $.ajax({
                    /*url: `${baseURL}/api/activityProfile/update/${jwt_decode(window.sessionStorage.getItem("token")).username}/${JSON.stringify(this.checkedNames)}/${true}`,*/
                    url: `${baseURL}/api/activityProfile/update}`,
                    context: this,
                    method: 'POST',
                    data: JSON.stringify(this.checkedNames),
                    success: function () {
                        console.log("ajax success");
                        router.push({name: "matchingMain"})

                    },
                    error: function () {
                        console.log("error");
                    }
                })

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