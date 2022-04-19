<template >
  <div style="text-align:center;">
    <h3>Search Establishments</h3>
    direction: {{this.address}}
  </div>
  
  <section class="ui one column centered grid" style="position:relative; z-index:1;">
      <div class="column">
         <form class="ui segment large form">
           <div class="ui message red" v-show="error">{{ error }}</div>
            <div class="field">
               <div class="ui right icon input large" :class="{loading:spinner}">
                  <input
                     type="text"
                     placeholder="Enter the address"
                     v-model="address"
                     ref="autocomplete"
                     />
                  <i class="dot circle link icon" @click="locatorButtonPressed"></i>
               </div>
            </div>

         </form>
      </div>
   </section>
  <section id="map"></section>
    <button @click="getGeocode(this.address); displayMap=true" class="ui button">Go</button>
    address: {{this.location}}
        
    <dev v-if="displayMap">
      <Map1 :latitude="locLat" :longitude="locLng"/>
    </dev>
</template>

<script>
// import axios from 'axios'
import Map1 from './ShowMap.vue'

export default {
  data () {
    return {
      spinner: false,
      displayMap: false,
      address: '',
      error: '',
      location:{
        lat: 33.7838,
        lng: -118.1141
      },
    }
  },
  components:{
    Map1
  },
  computed:{
    locLat(){
      return this.location.lat;
    },
    locLng(){
      return this.location.lng;
    }
  },
  methods:{
    async getGeocode(inputAddress){
      try{
      var geocoder = new google.maps.Geocoder;
      geocoder.geocode({'address': inputAddress}).
      then((result) => {
        if(result.results[0]){
        const {results} = result;
        this.location.lat = results[0].geometry.bounds.Ab.h;
        this.location.lng = results[0].geometry.bounds.Ua.j;
        console.log(this.location);
        }
        else{
          this.error = 'invalid address';
          console.log(this.error);
        }
      })      
      }
      catch(error){
        this.error = 'Invalid address';
      }
    },
    locatorButtonPressed() {
      this.spinner = true;
      if(navigator.geolocation){
        navigator.geolocation.getCurrentPosition(
            position => {
              this.location.lat = position.coords.latitude;
              this.location.lng = position.coords.longitude;
              console.log('lat: ' + this.location.lat);
              console.log('lng: ' + this.location.lng);
              this.getStreetAddressFrom(position.coords.latitude, position.coords.longitude);
              // this.showUserLocationOnTheMap(position.coords.latitude, position.coords.longitude);
            },
            error => {
              this.error = "Locator is unable to find your address. Please type your address manually";
              this.spinner = false;
            },
        )
      }
      else{
        console.log("Your browser does not support geolocation API")
        this.spinner = false;
      }
    },

     async getStreetAddressFrom(lat, long) {
      try {
          const latlng = {
          lat: parseFloat(lat),
          lng: parseFloat(long),
          };
          var geocoder = new google.maps.Geocoder();
          geocoder
          .geocode({ location: latlng })
          .then((response) => {
            if (response.results[0]) {
              this.address = response.results[0].formatted_address;
              this.spinner = false;
              console.log(response.results[0]);
              }
            }).then((error_message) => {
              // console.log('error message');
              this.error = error_message;
              this.spinner = false;
            })

          // var {
          //   data
          // } = await axios.get(
          //   "https://maps.googleapis.com/maps/api/geocode/json?latlng=" +
          //   lat +
          //   "," +
          //   long +
          //   "&key=AIzaSyCzpBhiWzAnVHY7-Es0IUuqm9NSEMTYtY0"
          // );
          // if (data.error_message) {
          //   this.error = error.message;
          //   this.spinner = false;
          // } else {
          //   this.address = data.results[0].formatted_address;
          //   this.spinner = false;
          // }
      } catch (error) {
          this.error = error.message;
          this.spinner = false;
      }
  },
  // showUserLocationOnTheMap(latitude, longitude){
  //   let map = new google.maps.Map(document.getElementById("map", {
  //     zoom:15,
  //     center: new google.maps.LatLng(latitude, longitude),
  //     mapTypeId: google.maps.MapTypeId.ROADMAP
  //   }))
  // }
  },
  mounted() {
  new google.maps.places.Autocomplete(
    this.$refs["autocomplete"], this.$forceUpdate()
  );
},
}
</script>

<style scoped>
    .ui.button,
    .dot.circle.icon{
    background-color: #ff5a5f;
    color:white;
    }

    .alignLeft {
        text-align: left;
        margin-left: 50px;
        font-weight: bold;
    }
/* #map{
  position:absolute;
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
} */
</style>