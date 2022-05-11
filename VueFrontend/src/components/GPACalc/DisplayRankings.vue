<template>
    <div class="rankings">
        <!-- Warning for errors-->
        <div class="warning">
            <div v-if="errors.length" :key="index" class="warning">{{errors}}</div>
        </div>

        <!-- User enters course and section to get ranking of -->
        <div>
            <input id="course" name="course" v-model="course" placeholder="Course">&nbsp;
            <input id="section" name="section" v-model="section" placeholder="Section #">&nbsp;
            <button @click="generateRankings"> Generate Rankings</button>
        </div>

        <!-- Displays rankings -->
        <RankingsChild v-for="rank in rankings" :key="rank.id" :rank="rank" id="classRank">
            <h5>{{rank.id}}</h5>
        </RankingsChild>

        <div>
            <button @click="onSubmit"> Return</button>

        </div>
        <router-view />
    </div>
</template>

<script>
    // Imports
    import axios from "axios"
    // import jwt_decode from "jwt-decode"
    import RankingsChild from '../GPACalc/RankingsChild.vue'
    import URLS from '../../variables'



    export default ({
        name: 'displayRankings',
        props: ['rank'],
        components: {
            RankingsChild
        },
        data() {
            return {
                post: null,
                rankings: [],
                course: null,
                errors: "",
                section: null
            }
        },

        created() {

        },
        computed: {
        },

        // Returns to main calculator page
        methods: {
            onSubmit() {
                this.$router.push({name: 'calculatorMain'})
            },

            // Generates Rankings
            generateRankings() {
                this.errors = ""
                axios.get(`${URLS.api.gradeCalc.displayRanking}/${this.course}/${this.section}`)
                    .then(response => this.rankings = response.data)
                console.log(this.rankings);
            },


        }

    })
</script>

<style scoped>
    .rankings {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .warning {
        color: red;
        margin: auto;
        width: 440px;
        text-align: center;
        font-size: 11px;
    }


    .d1 {
        width: 20px;
        position: absolute;
        align-content: center;
        margin-top: 10px;
    }
</style>