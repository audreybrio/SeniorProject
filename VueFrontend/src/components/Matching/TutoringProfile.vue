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
            <input type="radio" id="group" value="Group" v-model="picked">
            <label for="group">Group</label>
            <input type="radio" id="individual" value="individual" v-model="picked">
            <label for="individual">Individual</label>
            <br> 
        </div>

        <div>
            <br >
            Are you looking for a tutor or offering tutoring?

        </div>

        <div>
            <input type="radio" id="offer" value="offer" v-model="chosen">
            <label for="offer">Offering Tutoring</label>
            <input type="radio" id="requires" value="requires" v-model="chosen">
            <label for="requires">Looking For a Tutor</label>
            <br>
           
        </div>

        <div>
            <br >
            Add up to six classes
        </div>
        <div>
            <input id="course1" v-model="input" placeholder="Course One">
        </div>
        <div>
            <input id="couse2" v-model="input" placeholder="Course Two">
        </div>
        <div>
            <input id="course3" v-model="input" placeholder="Course Three">
        </div>
        <div>
            <input id="course4" v-model="input" placeholder="Course Four">
        </div>
        <div>
            <input id="course5" v-model="input" placeholder="Course Five">
        </div>
        <div>
            <input id="course6" v-model="input" placeholder="Course Six">
        </div>

        <button @click="save">Save</button>
        <button @click="onSubmit">Back</button>


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
                picked: null,
                chosen: null

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




        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>