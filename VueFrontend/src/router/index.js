import { createRouter, createWebHistory } from 'vue-router';

// Import your views here
// Core Features
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
//import EmailVue from '../components/Authentication/EmailVue.vue'
//import LoginVue from '../components/Authentication/LoginVue.vue'
import HomePage from '../Views/HomePage.vue'
import EmailVerification from '../components/Registration/EmailVerification.vue'
import AuthenticateUser from '../components/Authentication/AuthenticateUser'

// Application features
// StudentDiscounts
import StudentDiscounts from '../components/StudentDiscounts/StudentDiscounts.vue'

import MatchingMain from '../components/Matching/MatchingMain.vue'
import ActivityProfile from '../components/Matching/ActivityProfile.vue'
import TutoringProfile from '../components/Matching/TutoringProfile.vue'
import DisplayMatches from '../components/Matching/DisplayMatches.vue'
import MatchesChild from '../components/Matching/MatchesChild.vue'

import DiscountDetails from '../components/StudentDiscounts/DiscountDetails.vue'
import EstablishmentDetails from '../components/StudentDiscounts/EstablishmentDetails.vue'

import RecipeView from '../components/MyRecipe/RecipeView.vue'
import RecipeDetails from '../components/MyRecipe/RecipeDetails.vue'
import RecipeLayout from '../components/MyRecipe/RecipeLayout.vue'
import RecipeRegister from '../components/MyRecipe/RecipeRegister.vue'
import RecipeEdit from '../components/MyRecipe/RecipeEdit.vue'
import RecipeDelete from '../components/MyRecipe/RecipeDelete.vue'

import CalculatorMain from '../components/GPACalc/CalculatorMain.vue'
import GradeCalc from '../components/GPACalc/GradeCalc.vue'
import GpaCalc from '../components/GPACalc/GpaCalc.vue'
import DisplayRankings from '../components/GPACalc/DisplayRankings.vue'
import RankingsChild from '../components/GPACalc/RankingsChild.vue'

const routes = [
    // Core Features
    {
        path: '/',
        name: 'AuthenticateUser',
        component: AuthenticateUser
    },
    //{
    //    path: '/login',
    //    name: 'LoginVue',
    //    component: LoginVue

    //},
    {
        path: '/home',
        name: 'HomePage',
        component: HomePage

    },
    //{
    //    path: '/authenticate',
    //    name: 'authenticateUser',
    //    component: AuthenticateUser
    //},
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
        path: '/schedule/builder/',
        name: 'ScheduleBuilder',
        component: () => import('../Views/ScheduleBuilder/ScheduleBuilder.vue')
    },
    {
        path: '/schedule/builder/select/',
        name: 'SelectForBuilder',
        component: () => import('../Views/ScheduleBuilder/SelectForBuilder.vue')
    },
    // ScheduleComparison
    {
        path: '/schedule/comparison/',
        name: 'ScheduleComparison',
        component: () => import('../Views/ScheduleComparison/ScheduleComparison.vue')
    },
    {
        path: '/schedule/comparison/select/',
        name: 'SelectForComparison',
        component: () => import('../Views/ScheduleComparison/SelectForComparison.vue')
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
        path: '/matcheschild',
        name: 'displayChild',
        component: MatchesChild

    },



    {
        path: '/studentDiscounts/discountDetails/:id',
        name: 'discountDetails',
        component: DiscountDetails,
        props: true
    },
    {
        path: '/studentDiscounts/establishmentDetails/:id',
        name: 'EstablishmentDetails',
        component: EstablishmentDetails,
        props: true
    },
    {
        path: '/aidEligibility/info',
        name: 'studentInformation',
        component: () => import('../Views/AidEligibility/Info.vue')
    },
    // Recipe Sharing
    {
        path: '/recipeview',
        name: 'RecipeView',
        component: RecipeView,
        props: route => ({ page: parseInt(route.query.page) || 1 })
    },
    {
        path: '/register',
        name: 'RecipeRegister',
        component: RecipeRegister
    },
    {
        path: '/recipeview/:id',
        name: 'RecipeLayout',
        props: true,
        component: RecipeLayout,
        children: [
            {
                path: '',
                name: 'RecipeDetails',
                component: RecipeDetails,
            },
            {
                path: 'delete',
                name: 'RecipeDelete',
                component: RecipeDelete
            },
            {
                path: 'edit',
                name: 'RecipeEdit',
                component: RecipeEdit
            }
        ]
    },

    // Gpa calc
    {
        path: '/calculatorMain',
        name: 'calculatorMain',
        component: CalculatorMain
    },

    {
        path: '/gradecalc',
        name: 'gradeCalc',
        component: GradeCalc
    },

    {
        path: '/gpacalc',
        name: 'gpaCalc',
        component: GpaCalc
    },

    {
        path: '/displayrankings',
        name: 'displayRankings',
        component: DisplayRankings

    },

    {
        path: '/rankingschild',
        name: 'rankingsChild',
        component: RankingsChild

    },
    
    //EventPlanning
    //{
    //    path: '/eventPlannning',
    //    name: 'EventPlannning',
    //    component: () => import('../Views/EventPlannning/EventPlannning.vue')
    //},

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