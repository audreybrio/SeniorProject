<template>
    <div>
        <h3>Search Establishments</h3>
    </div>
    <div v-for="discount in discounts" :key="discount.id" class="discount">
        <router-link :to="{name:'discountDetails', params: {id: discount.id}}">
            <h2>{{discount.title}}</h2>
        </router-link>
    </div>
    <br />
    <br />

</template>

<script>
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";
//import Map1 from './ShowMap.vue'

export default {
  data () {
        return {
            discounts: {},
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
    //Map1
        },
  created(){
      this.getWebDiscounts()
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
      getWebDiscounts() {
          //this.isAccountCreated = false;
          //this.resetValidateValues;
          $.ajax({
              // set the HTTP request URL
              url: `${baseURL}/api/studentdiscounts/getDiscountsEstablishment`,
              // set the context object to the vue component
              // this line tells vue to update its components
              // when the success or error objects complete!
              // if it's not set, the components don't update!
              context: this,
              // HTTP method
              method: 'GET',
              // On a successful AJAX request:
              success: function (data) {
                  console.log(data)
                  this.discounts = data
                  // log that we've completed

                  return true;
              },
              // On an unsuccessful AJAX request:
              error: function (error) {
                  // log the error
                  console.log(error);
                  this.items = null;
                  return false;
              }
          });
      },
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
    this.$refs["autocomplete"]
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
    .discount h2 {
        background-color: #f4f4f4;
        padding: 20px;
        border-radius: 10px;
        margin: 10px auto;
        max-width: 600px;
        cursor: pointer;
        color: #444;
        text-decoration: none;
    }

        .discount h2:hover {
            background: #ddd;
        }

    .discount .a {
        text-decoration: none;
    }
</style>