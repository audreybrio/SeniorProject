import { createRouter, createWebHistory } from 'vue-router';
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
import EmailVue from '../components/Authentication/EmailVue.vue'
import LoginVue from '../components/Authentication/LoginVue.vue'
import HomePage from '../Views/HomePage.vue'


const routes = [
    {
        path: '/',
        name: 'EmailVue',
        component: EmailVue
    },
    {
        path: '/login',
        name: 'LoginVue',
        component: LoginVue

    },
    {
        path: '/home',
        name: 'HomePage',
        component: HomePage

    },
    {
        path: '/registration',
        name: 'RegistrationForm',
        component: RegistrationForm
    }
    // add more here 
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router