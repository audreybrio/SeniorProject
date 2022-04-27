<template >
  <div class="box">
    <div class="alignLeft">
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
    
  </div>
</template>

<script>
    import PostEstablishment from "./PostEstablishment.vue"
    import PostWebsite from "./PostWebsite.vue"

    export default {
      data () {
        return {
          form: {
            postType: ''
            },
          
        }
      },
      components:{
        PostEstablishment,
        PostWebsite
        }
    }
</script>

<style scoped>
    .alignLeft {
        text-align: left;
        margin: auto;
        width: 500px;
        font-weight: bold;
    }
    .box {
        background-color: rgb(212, 200, 210);
        margin: auto;
    }
  
hr {
        margin-left: auto;
        margin-right: auto;
      }
</style>