<template >
  <div style="text-align:center">
    <h3>Post Establishment</h3>
  </div>

  <form @submit.prevent="submitPost">
    <div class="alignLeft">
      <label for="discountName">Discount Name:</label>
      <br />
      <input type="text" class="form-control" id="discountName" v-model="form.discountName" maxlength="64" required>
      <br />
      <label for="establishment">Establishment:</label>
      <br />
      <input type="text" class="form-control" id="establishment" v-model="form.establishment" maxlength="64" required>
      <br />
      <label for="address">Address:</label>
      <br />
      <input type="text" class="form-control" id="address" v-model="form.address" maxlength="64" required>
      <br />
      <label for="description">Discount description:</label>
      <br />
      <textarea name="description" class="description-form" id="description" rows="3" v-model="form.description" maxlength="255" required></textarea>
      <input type="submit" value="Post Discount"/>
    </div>
  </form>
    the username is {{ this.username }}
</template>

<script>
export default {
  data () {
    return {
        username: this.$route.params.username,
      form: {
        discountName: '',
        establishment: '',
        address: '',
        description: ''
      }
    }
        },
        methods: {
            submitPost() {
                console.log('post submitted')
            }
        }
}
</script>

<style scoped>
.alignLeft {
    text-align: left;
    margin-left: 50px;
    font-weight: bold;
}
.row{
  margin: auto;
  text-align:left;
}
.form-control{
  width:90%;
}
.description-form{
  width:90%;
  max-width: 90%;
  height:6em;
}

</style>
