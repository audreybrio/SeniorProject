<template>
 <GoogleMap api-key="AIzaSyCzpBhiWzAnVHY7-Es0IUuqm9NSEMTYtY0" style="width: 100%; height: 500px" :center="this.center" :zoom="15">
        <Marker :options="{ position: this.center }" />
    </GoogleMap>
</template>

<script>
    import { GoogleMap, Marker } from "vue3-google-map";

    export default {
        components: { GoogleMap, Marker },
        props: {
            'latitud': Number, 'longitud': Number
        },
        data() {
            return {
                center: { lat: 33.7838, lng: -118.1141 }
            }
        },
        created() {
            this.center.lat = this.latitud
            this.center.lng = this.longitud
        }
    }
</script>
