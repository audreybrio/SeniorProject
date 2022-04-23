<template >
  <div class="box">
    <div class="alignLeft">
      <label for="formControlRange">Post Discount:</label><br/>
        the username is {{ username}}<br />
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
    import * as $ from 'jquery'
    const baseURL = "https://localhost:5002";

    export default {
      data () {
        return {
          form: {
            postType: ''
            },
          username: this.$route.params.username
        }
      },
      components:{
        PostEstablishment,
        PostWebsite
        },
        created() {
            this.postDiscount()
        },
    methods: {
        postDiscount() {
            $.ajax({
                // set the HTTP request URL
                url: `${baseURL}/api/studentdiscounts/verification/${this.username}`,
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
        }
    }
    }
</script>

<style scoped>
    .alignLeft{
        text-align:left;
        margin-left:50px;
        font-weight:bold;
    }
  .box{
    background-color:rgb(212, 200, 210);
  }
  
hr {
        margin-left: auto;
        margin-right: auto;
      }
</style>