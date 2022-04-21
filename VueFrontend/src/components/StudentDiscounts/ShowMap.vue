<template>
    <h3>Map</h3>
    <div>
        <!-- Lat: {{ latitude }}  and lng: {{ longitude }} -->
        center: {{this.center.lat}}, {{this.center.lng}}
        
    </div>
    <GoogleMap api-key="AIzaSyCzpBhiWzAnVHY7-Es0IUuqm9NSEMTYtY0Alb" style="width: 100%; height: 500px" :center="center" :zoom="15">
    <Marker :options="{ position: this.center }" />
    </GoogleMap>
</template>

<script>
import { GoogleMap, Marker } from "vue3-google-map";

export default {
    
    props:{'latitude':Number, 'longitude': Number},
    components: { GoogleMap, Marker },
    data() {
        return {
            center: {
                lat: 0,
                lng: 0
            }
        }
    },
    created(){
        this.center.lat = this.latitude;
        this.center.lng = this.longitude;
    },
    updated(){
        this.center.lat = this.latitude;
        this.center.lng = this.longitude;
    }
         
    
}
</script>
