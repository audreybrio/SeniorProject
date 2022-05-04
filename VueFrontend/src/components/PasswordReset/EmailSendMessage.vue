<template>
    <div class="message">

        <h1> Forgot Passowrd Confirmation</h1>
        <hr>
        <p>The email has been sent successfully. Check your email for new Password Update.</p>
    </div>
    <button @click="Back"> GO BACK TO LOGIN PAGE</button>

    <router-view/>
</template>

<script>

export default {
    name: 'EmailSendMessage',
    methods:{
        Back() {
            this.$router.push({
                    name: 'EmailVue'
                })
        }
    }
}

</script>


<style scoped>

h1{
    font-size: 2em;
}

p{
    font-size: 0.9em;
}
</style>