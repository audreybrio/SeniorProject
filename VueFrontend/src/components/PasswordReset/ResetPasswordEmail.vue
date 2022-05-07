<template>
    <div class="reset">
        <h5> FORGOT PASSWORD? </h5>
        <br />
        <div class="warning">
            <ul>
                <li v-for="(error, index) in errors" :key="index" class="warning">{{error}}</li>
            </ul>
        </div>
        <form @submit.prevent="validateUserInput">

            <div class="user">
                <input name="username" v-model="username" placeholder="Enter Username">
            </div>
            <div class="email">
                <input name="email" v-model="email" placeholder="Enter Email Address">
            </div>

            <button @click="reset">Reset</button>
            <button @click="cancel">Cancel</button>
        </form>
    </div>

</template>




<script>
    import AccessService from '/src/variables/index.js'

    export default {
        data() {
            return {
                username: '',
                email:'',
                errors:[],
                validate:{

                    usernameExist: false,
                    emailExist: false

                }
            }
        },
        computed: {
            areValidInputs(){
                if (this.validate.usernameExist == true && this.validate.emailExist == true) {
                    return true
                }
                else{
                    return false
                }
            }
        },
        methods: {
            onSend() {
                AccessService.postResetEmail({
                    username: this.username, email: this.email
                })
                .then(function (response) {
                    this.resetInputValues()
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });

                this.$router.push({
                    name: 'EmailSendMessage'
                })
            },
            cancel(){
                this.$router.push({
                    name: 'EmailVue'
                })
            },
            errorMessagges()
            {

                this.errors = []

                if (this.validate.usernameExist == false && this.validate.emailExist == false) {

                    this.errors.push("Username does not exists. chances inserting wrong username or email, or you need to create an account.")

                } else
                {
                    this.usernameExist = true,
                    this.emailExist = true
                }

               

            },
            validateUserInput() {
                this.resetValidateValues;

                AccessService.validateUserExist(this.username, this.email).then(response => {
                    this.validate.usernameExist = response.data;
                    this.validate.emailExist = response.data;
                    // log that we've completed
                    this.errorMessagges()
                    if (this.areValidInputs) {
                        // Creates a new user account if user inputs are valid

                        this.onSend()
                    }
                    return true
                })
                    .catch(error => {
                        console.log(error)
                        return false;

                    })
            },
            resetValidateValues(){
                this.validate.usernameExist = false
                this.validate.emailExist = false
            },
            resetInputValues(){
                this.username = '',
                this.email = ''
            }
         
        }
    }
</script>

<style scoped>


.reset {
    border-style: double;
    align-content: center;
    margin-left: 25rem;
    margin-right: 25rem;
    box-sizing: border-box;
}

h5 {
    font-size: 2em;
    font-display: auto;
}
label{
    margin-bottom: 0.5rem;

}
.reset .user {
    margin-bottom: 2rem;
}

.reset .email {
    margin-top: 2rem;
    margin-bottom: 2rem;
}

input{
    
    width: 50%;
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 5px;
    margin-bottom: 1rem;
}

button {
    margin: 2rem;
}


.cancel {
    align-content: right;
}

</style>
