<template>
    <h1>This is the email verification page</h1>
    the user name is {{$route.params.username}} and token: {{$route.params.token}}
</template>

<script>
    export default {
        //props: ["name"],
        data() {
            return {

            }
        }
    }
</script>