<template>
  <div 
    v-if="isOn" 
    class="background"
    @keydown.esc="hideScreen"
    @click="hideScreen"
    tabindex="0"
  >
    <div @click.stop>
      <slot />
    </div>
  </div>
  
</template>
<script>

export default {
  name: 'Modal',
  props: {
    isOn: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    hideScreen() {
      this.$emit('modal:hide');
    }
  }
}
</script>


<style scoped>
.background {
  position: fixed;
  top: 0px;
  left: 0px;
  width: 100vw;
  height: 100vh;
  background-color: rgba(35, 34, 34, 0.5);
  z-index: 3002;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
}

</style>