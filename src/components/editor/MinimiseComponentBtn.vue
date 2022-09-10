<template>
  <div 
  class="tool-box border"
  :style="getStyles()"
  >
    <slot v-if="!isMinimised" />
  </div>
</template>

<script>

export default {
  name: 'MinimiseComponentBtn',
  props: ['buttonFloat', 'right', 'bottom'],
  methods: {
    getStyles() {
      let styles = {};
      styles[this.right ? 'right': 'left'] = '0px';
      styles[this.bottom ? 'bottom': 'top'] = this.bottom ? '0px': '75px';
      return styles;
    }
  }
  
}
</script>

<style scoped>
.tool-box {
  z-index: 99999;
  position: fixed;
  width: 15%
}

.border {
  border: 1px solid #dee2e6;
}
</style>