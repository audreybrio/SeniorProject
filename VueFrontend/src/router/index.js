import { createRouter, createWebHistory } from 'vue-router';
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
import EmailVue from '../components/Authentication/EmailVue.vue'
import LoginVue from '../components/Authentication/LoginVue.vue'
import HomePage from '../Views/HomePage.vue'
import ScheduleBuilder from '../components/ScheduleBuilder/ScheduleBuilder.vue'
import ScheduleSelection from '../components/ScheduleBuilder/ScheduleSelection.vue'
import StudentDiscounts from '../components/StudentDiscounts/StudentDiscounts.vue'
import PostDiscount from '../components/StudentDiscounts/components/PostEstablishment.vue'
import SearchDiscount from '../components/StudentDiscounts/components/SearchEstablishment.vue'
const routes = [
    {
        // path: '/',
        path: '/emailvue',
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
        path: '/schedulebuilder',
        name: 'ScheduleBuilder',
        component: ScheduleBuilder
    },
    {
        path: '/scheduleselection',
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
        // path: '/studentDiscounts',
        path: '/',
        name: 'studentDiscounts',
        component: StudentDiscounts,
        children: [
            {
                path: 'studentDiscounts/PostDiscount',
                component: PostDiscount
            },
            {
                path: 'studentDiscounts/SearchDiscount',
                component: SearchDiscount
            }
        ]
    },
    // add more here 
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router