<template>
    <header id = "header">
        Student Mulit-Tool
        </header>
    <div id="app"></div>
    <router-view/>
</template>

<script>
    import HomePage from '@/Views/HomePage.vue'
    export default {
        name: 'App',
        components: HomePage
    }

</script>

<style>
    #header{
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: left;
        text-size-adjust: 20;
        text-decoration-color: aqua;
        
    }
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
