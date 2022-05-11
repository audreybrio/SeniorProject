<template>
    <div class="message">

        <h1> Account Recovery Confirmation</h1>
        <hr>
        <p>The email has been sent successfully. Check your email to update your Password or Reactivate an account.</p>
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
                name: 'authenticateUser'
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