<template>
    <div class="ranking">
        <h4>Rank: {{rank.id}} </h4>
        <p> Course: {{ rank.course}} </p>
        <p> Section: {{rank.section }}</p>
        <p> Grade: {{ rank.grade }} </p>
    </div>
</template>

<script>
    export default {
        props: {
            rank: {
                type: Object,
                required: true
            }
        }
    }
</script>

<style scoped>
    .ranking {
        padding: 20px;
        width: 500px;
        cursor: pointer;
        border: 1px solid #39495c;
        margin-bottom: 18px;
        align-items: center;
    }
</style>