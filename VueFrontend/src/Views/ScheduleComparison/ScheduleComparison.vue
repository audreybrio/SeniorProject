<template>
    <h2>
        Schedule Comparison
    </h2>
    <br />
    <div>
        <Schedules
                   :items="items"
                   :editableItems="true"
                   @item-updated="updateItem"
                   @item-deleted="deleteItem" />
    </div>
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
                schedules: []
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
                axios.get(`${URLS.api.scheduleComparison.getComparison}`, {
                    params: {
                        user: this.user,
                        scheduleIds: JSON.stringify(this.schedules)
                    }
                })
                    .then(response => {
                        this.items = response.data
                    })
            },
        },
    }
</script>

<style>
</style>
