import { createRouter, createWebHistory } from 'vue-router';

// Import your views here
// Core Features
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
import EmailVue from '../components/Authentication/EmailVue.vue'
import LoginVue from '../components/Authentication/LoginVue.vue'
import HomePage from '../Views/HomePage.vue'
import EmailVerification from '../components/Registration/EmailVerification.vue'

// Application features
// StudentDiscounts
import StudentDiscounts from '../components/StudentDiscounts/StudentDiscounts.vue'
import DiscountDetails from '../components/StudentDiscounts/DiscountDetails.vue'

// Defining the routes
const routes = [
    // Core Features
    {
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
        name: 'Registrationform',
        component: RegistrationForm
    },
    {
        path: '/Registrationform/:username/:token',
        name: 'EmailVerification',
        component: EmailVerification
    },

    // ScheduleBuilder & ScheduleComparison
    // ScheduleBuilder
    {
        path: '/schedule/builder/:user/:scheduleId',
        name: 'ScheduleBuilder',
        component: () => import('../components/ScheduleBuilder/ScheduleBuilder.vue')
    },
    {
        path: '/schedule/builder/select/:user',
        name: 'SelectForBuilder',
        component: () => import('../components/ScheduleBuilder/SelectForBuilder.vue')
    },
    // ScheduleComparison
    {
        path: '/schedule/comparison/:user/:selection',
        name: 'ScheduleComparison',
        component: () => import('../components/ScheduleComparison/ScheduleComparison.vue')
    },
    {
        path: '/schedule/comparison/select/:user',
        name: 'SelectForComparison',
        component: () => import('../components/ScheduleComparison/SelectForComparison.vue')
    },

    // BookSelling
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

    // StudentDiscounts
    {
        path: '/',
        // path: '/',
        name: 'studentDiscounts',
        component: StudentDiscounts,
    },
    {
        path: '/aidEligibility/info',
        name: 'studentInformation',
        component: () => import('../Views/AidEligibility/Info.vue')
    },
    {
        path: '/studentDiscounts/discountDetails/:id',
        name: 'discountDetails',
        component: DiscountDetails,
        props: true
    },
    // ADD MORE HERE! DON'T ADD AFTER not-found!

    // Not found; don't move this one or place anything after it
    {
        path: '/:pathMatch(.*)*',
        name: 'not-found',
        component: () => import('../Views/NotFound')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

// Resolve not found
router.resolve({
    name: 'not-found',
    params: { pathMatch: ['not', 'found'] },
}).href

export default router