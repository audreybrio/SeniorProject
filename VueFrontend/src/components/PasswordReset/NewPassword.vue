<template>
    <div class="new">
        <h5> Reset Password </h5>
        <br />
        <div class="warning">
            <ul>
                <li v-for="(error, index) in errors" :key="index" class="warning">{{error}}</li>
            </ul>
        </div>
        <form @submit.prevent="validatePassword">
            <div class="email">
                <input name="email" v-model="email" placeholder="Enter Email Address">
            </div>
            <div class="pass">
                <label for="password"></label>
                <input name="password" v-model="password" placeholder="Password">
            </div>
            <div class="confPass">
                <label for="confirmpassword"></label>
                <input name="confirmpassword" v-model="confirmpassword" placeholder="ConfirmPassword">
            </div>

            <button @click="submit">Submit</button>

        </form>
    </div>
    <router-view />
</template>

<script>
    import AccessService from '/src/variables/index.js'
    
    export default {
        data() {
            return {
               email: '',
               password: '',
               confirmpassword: '',
               errors:[],
               validate:{
                   passwordCheck: false,
               }
            }
        },
        computed: {
            areValidPass(){
                if (this.validate.passwordCheck == true) {
                    return true
                }
                else{
                    return false
                }
            }

        },
        methods: {
            onSubmit() {
                AccessService.newPassword({
                    email: this.email, password: this.password, confirmpassword: this.confirmpassword
                })
                .then(function (response) {
                    this.resetInputValues()
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });

                this.$router.push({
                    name: 'authenticateUser',
                })
            },
            errorMessagges() {

                this.errors = []
                if (this.validate.passwordCheck == false) {

                    this.errors.push("Check password and confirm password or both enteries has to be same.")
                    this.errors.push("Password must be 8 or more characters, but it can include integers and uppercase and lowercase characters.")

                } else {

                    this.passwordCheck = true
                }

            },
            validatePassword() {
                this.resetValidateValues;

                AccessService.validatePassExist(this.email, this.password, this.confirmpassword).then(response => {

                    this.validate.passwordCheck = response.data;
                    // log that we've completed
                    this.errorMessagges()
                    if (this.areValidPass) {
                        // Creates a new user account if user inputs are valid
                        this.onSubmit()
                    }
                    return true
                })
                    .catch(error => {
                        console.log(error)
                        return false;

                    })
            },
            resetValidateValues(){
                this.validate.passwordCheck = false
            },
            resetInputValues() {
                this.email = '',
                this.password = '',
                this.confirmpassword = ''
            }
         
        }
    }
</script>

<style scoped>


.new {
    border-style: double;
    align-content: center;
    margin-left: 20rem;
    margin-right: 20rem;
    box-sizing: border-box;
}

h5 {
    font-size: 2em;
}

.new .pass {
    margin-bottom: 2rem;
}

.new .confPass {
    margin-top: 2rem;
    margin-bottom: 2rem;
}

    .new .user {
        margin-bottom: 2rem;
    }

    .new .email {
        margin-top: 2rem;
        margin-bottom: 2rem;
    }


input{
    width: 50%;
}

button {
    margin: 2rem;
}

</style>
