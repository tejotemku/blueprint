<template>
  <div 
  :class="isMinimised? 'tool-box' : 'tool-box border'"
  :style="getStyles()"
  >
    <v-btn @click="onClick" :style="'float:' + buttonFloat">
      <font-awesome-icon :icon="isMinimised? 'maximize' : 'minimize'" style="fontSize: 1.5rem"/>
    </v-btn>
    <slot v-if="!isMinimised" />
  </div>
</template>

<script>

export default {
  name: 'MinimiseComponentBtn',
  props: ['buttonFloat', 'right', 'bottom'],
  data() {
    return {
      isMinimised: false,
    }
  },
  methods: {
    onClick() {
      this.isMinimised = !this.isMinimised;
    },
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
  border: 1px solid black;
}
</style>