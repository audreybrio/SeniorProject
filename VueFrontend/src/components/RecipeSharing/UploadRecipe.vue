<template>
  <div class="popup">
    <div class="popup-inner">
      <slot></slot>
      <button class ="popup-submit" @click =" TogglePopup()"> Submit </button>
    </div>
  </div>
  <router-view/>
</template>

<script>

export default ({
  props: ['TogglePopup']
 
})
</script>

<style lang = "scss" scoped>

  .popup{
    position: fixed;
    top: 0;
    bottom:  0;
    right: 0;
    left: 0;
    z-index: 99;
	background-color: rgba(0, 0, 0, 0.2);
	
	display: flex;
	align-items: center;
	justify-content: center;

  }
  .popup-inner {
		background: #FFF;
		padding: 32px;
	}

</style>