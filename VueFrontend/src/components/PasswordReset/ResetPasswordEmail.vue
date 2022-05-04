<template>
    <div class="reset">
        <h5> FORGOT PASSWORD? </h5>
        <form @submit.prevent="onSend">
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
    import AccessService from '/src/variables/AccessService.js'

    export default {
        
        data() {
            return {

               username: '',
               email: ''
            }
        },
        methods: {
            
            onSend() {
                AccessService.postResetEmail({
                        username: this.username, email: this.email
                    })
                .then(function (response) {
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
    font-display: alig;
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
