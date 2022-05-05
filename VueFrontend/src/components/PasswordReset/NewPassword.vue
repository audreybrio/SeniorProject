<template>
    <div class="new">
        <h5> Reset Password </h5>
        <form @submit.prevent="onSubmit">
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
               password: '',
               confirmpassword: ''
            }
        },
        methods: {
            onSubmit() {
                    AccessService.newPassword({
                        password: this.password, confirmpassword: this.confirmpassword
                    })
                .then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });

                this.$router.push({
                    name: 'EmailVue',
                })
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

input{
    width: 50%;
}

button {
    margin: 2rem;
}

</style>
