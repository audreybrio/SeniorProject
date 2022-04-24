<template>
    <div class="post">
        <button @click="onSubmit">Save Information</button>
    </div>
    <div>
        <label>Major: </label>
        <input type="text" v-model="major" />
    </div>
    <div>
        <label>GPA: </label>
        <input type="number" step="0.01" v-model="gpa" />
    </div>
    <div>
        <label>Units: </label>
        <input type="number" step="1" v-model="units" />
    </div>
    <div>
        <label>Finanial Aid: </label>
        <input type="checkbox" v-model="financialAid" />
    </div>
    <div>
        <label>School: </label>
        <input type="text" v-model="school" />
    </div>
    <div>
        <label>Member of Student body: </label>
        <input type="checkbox" v-model="studentBody" />
    </div>
    <!--<router-view />-->
</template>

<script lang="js">
    //import router from '../../router'
    import jwt_decode from "jwt-decode"

    export default ({
        name: "studentInformation",
        data() {
            return {
                major: "",
                gpa: -1,
                units: -1,
                financialAid: false,
                school: "",
                studentBody: false,
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username
            };
        },
        created() {
            
            
        },
        watch: {
            
            
        },
        methods: {
            
            onSubmit() {
                console.log(this.major)
                console.log(this.gpa)
                console.log(this.units)
                console.log(this.financialAid)
                console.log(this.school)
                console.log(this.studentBody)
            },

             

        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>