import { createRouter, createWebHistory } from 'vue-router';
import RegistrationForm from '../components/Registration/RegistrationForm.vue'
import EmailVue from '../components/Authentication/EmailVue.vue'
import LoginVue from '../components/Authentication/LoginVue.vue'
import HomePage from '../views/HomePage.vue'
import ScheduleBuilder from '../components/ScheduleBuilder/ScheduleBuilder.vue'
import ScheduleSelection from '../components/ScheduleBuilder/ScheduleSelection.vue'
import StudentDiscounts from '../components/StudentDiscounts/StudentDiscounts.vue'
import MyRecipe from '../components/RecipeSharing/RecipeMainPage.vue'
import NewRecipe from '../components/MyRecipe/RecipeView.vue'
import DiscountDetails from '../components/StudentDiscounts/DiscountDetails.vue'
import EstablishmentDetails from '../components/StudentDiscounts/EstablishmentDetails.vue'


// Defining the routes
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
        component: () => import('../views/BookSelling/BookSelling.vue')
    },
    {
        path: '/bookDisplay',
        name: 'bookDisplay',
        component: () => import('../views/BookSelling/BookDisplay.vue')
    },
    {
        path: '/bookPost',
        name: 'bookPost',
        component: () => import('../views/BookSelling/PostBook.vue')
    },
    {
        path: '/studentDiscounts',
        name: 'studentDiscounts',
        component: StudentDiscounts
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
    
    // ADD MORE HERE! DON'T ADD AFTER not-found!

    // Not found; don't move this one or place anything after it
    {
        path: '/Recipe',
        name: 'MyRecipe',
        component: MyRecipe
    },
    {
        path: '/recipe/:slug',
        name: 'Upload',
        component: () => import('../components/RecipeSharing/UploadRecipe.vue')
    },
    {
        path: '/newrecipe',
        name: 'NewRecipe',
        component: NewRecipe
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router