<template >
  <div class="box">
    <div class="form-group">
      <label for="formControlRange">Post Discount:</label><br/>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="postType" id="type-establishment" value="establishment" v-model="form.postType">
        <label class="form-check-label" for="postType">Establishment</label>
      </div>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="postType" id="type-website" value="website" v-model="form.postType">
        <label class="form-check-label" for="postType">Website</label>
      </div>
    </div>
    <hr />

    <div v-if="this.form.postType==='establishment'">
      <PostEstablishment/>
    </div>
    <div v-if="this.form.postType==='website'">
      <PostWebsite/>
    </div>
    
    <!-- <div class="row">
      <div id="app" class="form-group">
        <label for="name">Name</label>
        <br />
        <input type="text" class="form-control" id="name" placeholder="Your name" v-model="form.name">
      </div>
    </div> -->
  </div>
</template>

<script>
import PostEstablishment from "./PostEstablishment.vue"
import PostWebsite from "./PostWebsite.vue"
export default {
  // props: ['product', 'index', 'addToCart'],
  data () {
    return {
      form: {
        postType: ''
      }
    }
  },
  components:{
    PostEstablishment,
    PostWebsite
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