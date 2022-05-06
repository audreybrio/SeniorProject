<template>
    <div class="new">
        <h5> ACTIVATE YOUR ACCOUNT</h5>
        <br />
        <form @submit.prevent="accountActivate">
            <div class="activate">
                <div class="user">
                    <input name="username" v-model="username" placeholder="Enter Username">
                </div>

                <select v-model="activate" required>
                    <option disabled value="">Please select an option. </option>
                    <option value=true>Activate</option>
                    <option value=false>Diactivate</option>
                </select>


            </div>
            <button >Submit</button>
        </form>
    </div>
    <router-view />
</template>

<script>
    import AccessService from '/src/variables/index.js'
    
    export default {
        data() {
            return {
                username: '',
                activate: false
            }
        },
        methods:
        {
            accountActivate() {
                AccessService.recoveryAccount({ username: this.username, activate: this.activate })
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
select {
        margin: 2rem;
}


button {
    margin: 2rem;
}

</style>
