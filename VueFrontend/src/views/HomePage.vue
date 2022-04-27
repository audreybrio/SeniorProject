<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Hello {{ id }}! :)
        </div>
<<<<<<< HEAD:VueFrontend/src/views/HomePage.vue
        <button @click="onAC">Access Control</button>
        <button @click="onSB">Schedule Builder</button>
        <button @click="onAM">Automated Moderating</button>
        <button @click="onBS">Book Selling</button>
        <button @click="onUSD">User Analysis Dashboard</button>
        <button @click="onSD">Student Discounts</button>
        <button @click="onR">Recipe</button>
        <button @click="onSubmit">Logout</button>
=======
        <div>
            <button @click="onAC">Access Control</button>
            <button @click="onScheduleBuilder">Schedule Builder</button>
            <button @click="onScheduleComparison">Schedule Comparison</button>
        </div>
        <div>
            <button @click="onAM">Automated Moderating</button>
            <button @click="onBS">Book Selling</button>
            <button @click="onUSD">User Analysis Dashboard</button>
        </div>
        <div>
            <button @click="onAid">Aid Eligibility Estimates</button>
            <button @click="onSD">Student Discounts</button>
            <button @click="onSubmit">Logout</button>
        </div>
>>>>>>> main:VueFrontend/src/Views/HomePage.vue
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
<<<<<<< HEAD:VueFrontend/src/views/HomePage.vue
            onSB() {
                router.push({ name: "EmailVue" });
=======
            onScheduleBuilder() {
                router.push({ name: "SelectForBuilder", params: { user: this.id }});
            },
            onScheduleComparison() {
                router.push({ name: "SelectForComparison", params: { user: this.id }});
>>>>>>> main:VueFrontend/src/Views/HomePage.vue
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
<<<<<<< HEAD:VueFrontend/src/views/HomePage.vue
            },onR(){
                router.push({name: "MyRecipe"});
            }

=======
            },
            onAid() {
                router.push({ name: "studentInformation" });
            }
>>>>>>> main:VueFrontend/src/Views/HomePage.vue
        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>