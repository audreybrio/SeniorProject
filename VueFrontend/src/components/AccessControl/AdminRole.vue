<template>
    <div class = "post">
        <div v-if="loading" class="loading">
            <p>This is the Administration. </p>
        </div>

        <button @click="AD">Admin</button>
        
    </div>
    <router-view/>
</template>

<script lang="js">
    import router from '@/router'
    export default ({
        data() {
            return {
                loading: false,
                post: null,
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
            },AD() {
            router.push({ name: "HomePage" });
            }

        },
        
    });
    
</script>

<style scoped>
    button {
        font-weight: bold;
    }
</style>
