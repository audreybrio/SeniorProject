<template>
    <div class="post">
        <div>
            Hello {{ id }}! :)
        </div>
        <div v-if="role === 'admin'">
            <button @click="onUsageAnalysisDashboard">Usage Analysis Dashboard</button>
            <button @click="onUserManagement">User Management</button>
        </div>
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
            <!--<button @click="onAid">Aid Eligibility Estimates</button>-->
            <button @click="onSD">Student Discounts</button>
            <button @click="onMatching">Matching</button>
        </div>

        <div>
            <button @click="onEP">Event Planning</button>
            <button @click="onCalc">GPA/Grade Calculator</button>
        </div>
        <div>
            <button @click="onManageAccount">Manage Account</button>
        </div>
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
                id: jwt_decode(window.sessionStorage.getItem("token")).username,
                role: 'admin'
            };
        },
        created() {
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            onSubmit() {
                const token = window.sessionStorage.getItem("token");
                var isJWT = jwt_decode(token);
                console.log(isJWT);
                window.sessionStorage.removeItem("token");
                router.push({ name: "EmailVue" });

            },

            onUsageAnalysisDashboard() {
                if (this.role === 'admin') {
                    router.push({ name: "not-found" });
                }
                else {
                    alert("You lack the necessary role to access that page.")
                }
            },
            onUserManagement() {
                if (this.role === 'admin') {
                    router.push({ name: "UserManagement" });
                }
                else {
                    alert("You lack the necessary role to access that page.")
                }
            },

             onAC() {
                router.push({ name: "not-found" });
            },
            onScheduleBuilder() {
                router.push({ name: "SelectForBuilder", params: { user: this.id }});
            },
            onScheduleComparison() {
                router.push({ name: "SelectForComparison", params: { user: this.id }});
            },
            onAM() {
                router.push({ name: "not-found" });
            },
            onBS() {
                router.push({ name: "bookSelling" });
            },
            onUSD() {
                router.push({ name: "not-found" });
            },
            onSD() {
                router.push({ name: "studentDiscounts" });
            },
            onMatching() {
                router.push({ name: "matchingMain" })
            },


            onAid() {
<<<<<<< HEAD
                //router.push({ name: "studentInformation" });
                router.push({ name: "EmailVue" });
            }
=======
                router.push({ name: "studentInformation" });
            },

            onCalc() {
                router.push({name: "calculatorMain"})
            },

>>>>>>> d0a03ed95ae02939a52e0eb75b9022e1e6d9bdb1
        },
    });
</script>
<style scoped>
    button {
        font-weight: bold;
    }
</style>