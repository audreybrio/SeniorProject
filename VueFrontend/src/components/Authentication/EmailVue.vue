<template>
<header>         <h2>Login Form</h2>
 </header>
    <body>
        <form method="post">
        <div class="welcome">
            <h>Welcome to Student Multi-Tool</h>
        </div>

        <div class="container">
            <label for="uname"><b>Email</b></label>
            <input id="email" v-model="email" placeholder="Enter Email" name="Email" required>

            <label for="psw"><b>Password</b></label>
            <input id="passcode" v-model="passcode" placeholder="Enter Password" name="psw" required>
                
            <button type="submit" @click="onSubmit">Login</button>
                <div>
                    <router-link to="/registration"><button>Registration</button></router-link>
                </div>
        </div>
        <!-- <div class="container" style="background-color:#f1f1f1">
            <button type="button" class="cancelbtn">Cancel</button>
            <span class="psw">Forgot <a href="#">password?</a></span>
        </div> -->
        </form>

    </body>
    <!-- <div class="post">
        <div v-if="loading" class="loading">
            Welcome! Please enter email and passcode!
        </div>
        <input id="email" v-model="email" placeholder="Email">
        <input id="passcode" v-model="passcode" placeholder="Passocde">
        <button id="button" @click="onSubmit">Submit</button>
        <div>
            <router-link to="/registration">Registration</router-link>
        </div>
    </div> -->
    <router-view />
</template>

<script> // <!--lang="js"-->
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
                this.email = '';
                this.passcode = '';
            },
            onSubmit() {
                if (this.email == "audrey.brio@student.csulb.edu" && this.passcode == "hello world") {
                    router.push({name: "LoginVue"});

                }
                else if (this.email == "bradley.nickle@student.csulb.edu" && this.passcode == "marvel fan") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "joseph.cutri@student.csulb.edu" && this.passcode == "number one") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "albert.toscano01@student.csulb.edu" && this.passcode == "happy meal") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "jacob.delgado01@student.csulb.edu" && this.passcode == "super man") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "szeman.tang@student.csulb.edu" && this.passcode == "wonder woman") {
                    router.push({ name: "LoginVue" });

                }
                else if (this.email == "devarsh.patel@student.csulb.edu" && this.passcode == "pizza guy") {
                    router.push({ name: "LoginVue" });

                }
                else {
                    alert("Incorect email/passcode");
                }
            }

        },
    });
</script>
<style scoped>

body {
    font-family: Arial, Helvetica, sans-serif; 
    display: float, center;
    vertical-align: middle;    
    height:100%;
 }
form {border: 3px solid #f1f1f1; align-items: center; padding: 5rem;}

input[id=email], input[id=passcode] {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  box-sizing: border-box;
}

button {
  background-color: #04AA6D;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
}

button:hover {
  opacity: 0.8;
}

.cancelbtn {
  width: auto;
  padding: 10px 18px;
  background-color: #f44336;
}

.welcome {
  text-align: center;
  margin: 24px 0 12px 0;
}

.container {
  padding: 16px;
}

span.psw {
  float: right;
  padding-top: 16px;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
  span.psw {
     display: block;
     float: none;
  }
  .cancelbtn {
     width: 100%;
  }
}
    button {
        font-weight: bold;
    }
</style>