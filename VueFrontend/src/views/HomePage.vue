<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Hello {{ id }}! :)
        </div>
        <button @click="onAC">Access Control</button>
        <button @click="onSB">Schedule Builder</button>
        <button @click="onAM">Automated Moderating</button>
        <button @click="onBS">Book Selling</button>
        <button @click="onUSD">User Analysis Dashboard</button>
        <button @click="onSD">Student Discounts</button>
        <button @click="onR">Recipe</button>
        <button @click="onSubmit">Logout</button>
    </div>
    <router-view />
</template>

<script lang="js">
    import router from '../router'
    import jwt_decode from "jwt-decode"

    export default ({
        data() {
            return {
                loading: false,
                post: null,
                id: jwt_decode(window.sessionStorage.getItem("token")).username
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
            },
            onSubmit() {
                const token = window.sessionStorage.getItem("token");
                var isJWT = jwt_decode(token);
                console.log(isJWT);
                window.sessionStorage.removeItem("token");
                router.push({ name: "EmailVue" });

            },

             onAC() {
                router.push({ name: "EmailVue" });
            },
            onSB() {
                router.push({ name: "EmailVue" });
            },
            onAM() {
                router.push({ name: "EmailVue" });
            },
            onBS() {
                router.push({ name: "bookSelling" });
            },
            onUSD() {
                router.push({ name: "NewRecipe" });
            },
            onSD() {
                router.push({ name: "studentDiscounts" });
            },onR(){
                router.push({name: "MyRecipe"});
            }

        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>