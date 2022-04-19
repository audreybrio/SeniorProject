<template>
    <div id="app">
        <h2>Registration Form</h2>
        <form @submit.prevent="postData">
            <div>
                <label for="username"></label>
                <input name="username" v-model="username" placeholder="Username" >
            </div>
            <div>
                <label for="password"></label>
                <input name="password" v-model="password" placeholder="password" type="password">
            </div>
            <div>
                <label for="retype_password"></label>
                <input name="retype_password" v-model="retype_password" placeholder="Re-type password" type="password">
            </div>
            <div>
                <label for="email"></label>
                <input name="email" v-model="email" placeholder="email" type="email">
            </div>
            <div>
                <label for="retype_email"></label>
                <input name="retype_email" v-model="retype_email" placeholder="Re-type email" type="email">
            </div>
            <div>
                <label for="university"></label>
                <input name="university" v-model="university" placeholder="University">
            </div>
            <input type="submit" value="register">

            <div>
                <router-link to="/">Login</router-link>
            </div>
        </form>
    </div>
</template>
<script>
    export default {
        // name: "App",
        data() {
            return {
                username: "",
                password: "",
                retype_password: "",
                email: "",
                retype_email: "",
                university: ""
            };
        },
        methods: {
            postData() {
                console.log('posting data...')
            },

            async login() {
                const { username, password, retype_password, email, retype_email, university } = this;
                let res = await fetch(
                    "https://SomberHandsomePhysics--five-nine.repl.co/register",
                    {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({
                            username,
                            password,
                            retype_password,
                            email,
                            retype_email,
                            university
                        })
                    }
                )
                const data = await res.json();
                console.log(data);
            }
        }
    };
</script>

