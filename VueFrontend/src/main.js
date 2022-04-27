import { createApp } from '@vue/runtime-dom'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'

createApp(App).use(router,axios).use(store).mount('#app')
