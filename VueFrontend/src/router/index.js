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
import MatchingMain from '../components/Matching/MatchingMain.vue'
import ActivityProfile from '../components/Matching/ActivityProfile.vue'
import TutoringProfile from '../components/Matching/TutoringProfile.vue'
import DisplayMatches from '../components/Matching/DisplayMatches.vue'

const routes = [
    // Core Features
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
        path: '/studentDiscounts',
        // path: '/',
        name: 'studentDiscounts',
        component: StudentDiscounts,
    },

    // matching
    {
        path: '/matchingMain',
        name: 'matchingMain',
        component: MatchingMain
    },

    {
        path: '/activityprofile',
        name: 'activityProfile',
        component: ActivityProfile
    },

    {
        path: '/tutoringprofile',
        name: 'tutoringProfile',
        component: TutoringProfile
    },

    {
        path: '/displaymatches',
        name: 'displayMatches',
        component: DisplayMatches

    },

    {
        path: '/aidEligibility/info',
        name: 'studentInformation',
        component: () => import('../Views/AidEligibility/Info.vue')
    },
    // ADD MORE HERE! DON'T ADD AFTER not-found!

    // Not found; don't move this one or place anything after it
    {
        path: '/:pathMatch(.*)*',
        name: 'not-found',
        component: () => import('../Views/NotFound')
    }
    // add more here 
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