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
    import * as $ from 'jquery'
    import Schedules from '../ScheduleBuilder/Schedules'
    export default {
        name: 'ScheduleComparison',
        components: {
            Schedules,
        },
        data() {
            return {
            }
        },
        computed: {
            ajaxContext() {
                return this;
            }
        },
        created() {
            this.user = this.$route.params.user;
            this.scheduleId = this.$route.params.scheduleId;
        },
        methods: {
            loadSchedules() {
                $.ajax({});
            },
        },
    }
</script>

<style>
</style>
