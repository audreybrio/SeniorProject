<template>
    <!-- Display Matches child, displays name of match reason and overlap-->
        <div class="match-card">
            <h4>Match: {{match.match}} </h4>
            <p> Reason: {{ match.reason}} </p>
            <p> Overlap: {{ match.overlap }} </p>
        </div>
</template>

<script>
    export default {
        props: {
            match: {
                type: Object,
                required: true
            }
        }
    }
</script>

<style scoped>
    .match-card {
        padding: 20px;
        width: 500px;
        cursor: pointer;
        border: 1px solid #39495c;
        margin-bottom: 18px;
    }
</style>