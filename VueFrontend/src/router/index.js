import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../Views/HomePage.vue'
import EmailVue from '../components/Authentication/EmailVue.vue'
import LoginVue from '../components/Authentication/LoginVue.vue'
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
import Administration from '../components/AccessControl/AdminRole.vue'
import MyRecipe from '../components/RecipeSharing/RecipeMainPage.vue'

const routes = [
    {
        path: '/',
        name: 'EmailVue',
        component: EmailVue
    },
    {
        path: '/LoginVue',
        name:'LoginVue',
        component:LoginVue

    },
    {
        path: '/registration',
        name: 'RegistrationForm',
        component: RegistrationForm
    },
    {
        path: '/HomePage',
        name: 'HomePage',
        component: HomePage
    },
    {
        path: '/Admin',
        name: 'Administration',
        component: Administration
    },
    {
        path: '/MyRecipe',
        name: 'MyRecipe',
        component: MyRecipe

    },

]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router