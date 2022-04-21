<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Hello {{ id }}! :)
        </div>
        <button @click="onAC">Access Control</button>
        <button @click="onScheduleBuilder">Schedule Builder</button>
        <button @click="onAM">Automated Moderating</button>
        <button @click="onBS">Book Selling</button>
        <button @click="onUSD">User Analysis Dashboard</button>
        <button @click="onSD">Student Discounts</button>
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

                fetch('https://studentmultitool.me:5001/weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });

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
            onScheduleBuilder() {
                router.push({ name: `ScheduleSelection`, params: { user: this.id }});
            },
            onAM() {
                router.push({ name: "EmailVue" });
            },
            onBS() {
                router.push({ name: "bookSelling" });
            },
            onUSD() {
                router.push({ name: "EmailVue" });
            },
            onSD() {
                router.push({ name: "studentDiscounts" });
            },

        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>