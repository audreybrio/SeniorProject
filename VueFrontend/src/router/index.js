import { createRouter, createWebHistory } from 'vue-router';
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
import EmailVue from '../components/Authentication/EmailVue.vue'
import LoginVue from '../components/Authentication/LoginVue.vue'
import HomePage from '../Views/HomePage.vue'
import ScheduleBuilder from '../components/ScheduleBuilder/ScheduleBuilder.vue'
import ScheduleSelection from '../components/ScheduleBuilder/ScheduleSelection.vue'
import StudentDiscounts from '../components/StudentDiscounts/StudentDiscounts.vue'
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
    },
    {
        path: '/schedulebuilder/:user/:scheduleId',
        name: 'ScheduleBuilder',
        component: ScheduleBuilder
    },
    {
        path: '/scheduleselection/:user',
        name: 'ScheduleSelection',
        component: ScheduleSelection
    },
    {
        path: '/bookSelling',
        name: 'bookSelling',
        component: () => import('../Views/BookSelling/BookSelling.vue')
    },
    {
        path: '/bookDisplay',
        name: 'bookDisplay',
        component: () => import('../Views/BookSelling/BookDisplay.vue')
    },
    {
        path: '/bookPost',
        name: 'bookPost',
        component: () => import('../Views/BookSelling/PostBook.vue')
    },
    {
        path: '/studentDiscounts',
        name: 'studentDiscounts',
        component: StudentDiscounts
    },
    // add more here 
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router