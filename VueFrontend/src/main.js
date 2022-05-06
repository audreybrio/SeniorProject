import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
//import VueGoogleMaps from '@fawmi/vue-google-maps'

createApp(App).use(router).mount('#app')
/*createApp(App)
    .use(VueGoogleMaps, {
        load: {
            key: 'AIzaSyCzpBhiWzAnVHY7-Es0IUuqm9NSEMTYtY0',
            libraries: "places"
        },
    })
    .use(router).mount('#app')*/