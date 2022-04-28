<template>
    <router-link class="match-link" :to="{ name: 'displayMatches', params:{ id: match.id} }">
        <div class="match-card">
            <h4>{{match.id}}: {{match.match}} </h4>
            <p> {{ match.reason}} </p>
            <p> {{ match.overlap }} </p>
        </div>
    </router-link>
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

        .recipe-card:hover {
            transform: scale(1.01);
            box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2);
        }

    .match-link {
        color: black;
        text-decoration: none
    }
</style>