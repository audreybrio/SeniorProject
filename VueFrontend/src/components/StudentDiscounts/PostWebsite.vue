<template >
  <div style="text-align:center">
    <h3>Post Website</h3>
  </div>

  <div class="row">
    <div id="app" class="row">
      <label for="discountName">Discount Name:</label>
      <br />
      <input type="text" class="form-control" id="discountName" v-model="form.discountName">
      <br />
      <label for="website">Website:</label>
      <br />
      <input type="text" class="form-control" id="website" v-model="form.website">
      <br />
      <label for="description">Discount description:</label>
      <br />
      <textarea name="description" class="description-form" id="description" rows="3" v-model="form.description"></textarea>
    </div>
  </div>

</template>

<script>
export default {
  data () {
    return {
      form: {
        discountName: '',
        website: '',
        description: ''
      }
    }
  }
}
</script>

<style scoped>
.row{
  margin: auto;
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