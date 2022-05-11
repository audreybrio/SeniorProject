<template>
    <h2>
        Schedule Comparison
    </h2>
    <br />
    <div>
        <h4 v-if="link.length > 0">{{ link }}</h4>
        <!--<router-link v-if="link.length > 0">{{link}}</router-link>-->
        <br />
        <button @click="getLink">Show Link</button>
    </div>
    <br />
    <div>
        <Schedules
                   :items="items"
                   :editableItems="false"
                   @item-updated="updateItem"
                   @item-deleted="deleteItem" />
    </div>
    <router-view />
</template>

<script>
    //import * as $ from 'jquery'
    import Schedules from '../../components/ScheduleBuilder/Schedules'
    import URLS from '../../variables'
    import axios from 'axios'
    import jwt_decode from 'jwt-decode'
    export default {
        name: 'ScheduleComparisonLink',
        components: {
            Schedules,
        },
        data() {
            return {
                items: [],
                schedules: [],
                link: "",
                user: jwt_decode(window.sessionStorage.getItem("token")).username
                //user: this.$route.params.user
            }
        },
        computed: {
            ajaxContext() {
                return this;
            }
        },
        created() {
            // get the parameters from the router to load the comparison
            this.schedules = this.$route.params.selection;

            this.loadComparison();
        },
        methods: {
            // load the comparison
            loadComparison() {
                let params = {
                    user: this.user,
                    scheduleIds: JSON.stringify(this.schedules)
                    //scheduleIds: this.schedules
                }
                console.log(params)
                axios.get(`${URLS.api.scheduleComparison.getComparison}`, {
                    params: params
                })
                    .then(response => {
                        this.items = response.data
                    })
            },
            getLink() {
                URLS.domain === URLS.LOCALHOST ?
                    this.link = "https://localhost:5002/" + `schedule/comparison/user/${this.user}/selection/${this.schedules}` :
                    this.link = `${URLS.root}schedule/scomparison/user/${this.user}/selection/${this.schedules}`;
            },

            // explicitly left blank; these shouldn't do anything but the subcomponents for this template expect 
            // the methods to be here.
            updateItem() {},
            deleteItem() {}

        },
    }
</script>

<style>
</style>
