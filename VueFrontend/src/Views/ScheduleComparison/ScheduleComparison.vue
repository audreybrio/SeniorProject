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
    export default {
        name: 'ScheduleComparison',
        components: {
            Schedules,
        },
        data() {
            return {
                items: [],
                schedules: [],
                link: ""
            }
        },
        computed: {
            ajaxContext() {
                return this;
            }
        },
        created() {
            // get the parameters from the router to load the comparison
            this.user = this.$route.params.user;
            this.schedules = this.$route.params.selection;
            this.loadComparison();
        },
        methods: {
            // load the comparison
            loadComparison() {
                let params = {
                    user: this.user,
                    //scheduleIds: JSON.stringify(this.schedules)
                    scheduleIds: this.schedules
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
            updateItem() {},
            deleteItem() {}

        },
    }
</script>

<style>
</style>
