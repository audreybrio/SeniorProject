<template>
  <div class="box">
    <div class="form-group">
      <label for="formControlRange">Search Discount:</label><br/>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="searchType" id="type-establishment" value="establishment" v-model="form.searchType">
        <label class="form-check-label" for="searchType">Establishment</label>
      </div>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="searchType" id="type-website" value="website" v-model="form.searchType">
        <label class="form-check-label" for="searchType">Website</label>
      </div>
    </div>
    <hr />
    <div v-if="this.form.searchType==='establishment'">
      <SearchEstablishment/>
    </div>
    <div v-if="this.form.searchType==='website'">
      <SearchWebsite/>
    </div>

  </div>
</template>

<script>
import SearchEstablishment from "./SearchEstablishment.vue"
import SearchWebsite from "./SearchWebsite.vue"

export default {
  data() {
    return {
      form:{
        searchType: ''
      }
    }
  },
  components:{
    SearchEstablishment,
    SearchWebsite
  }
}
</script>


<style scoped>
  .box{
    background-color:rgb(212, 200, 210);
  }
  hr {
        margin-left: auto;
        margin-right: auto;
      }
</style>