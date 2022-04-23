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
    // add more here 
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router