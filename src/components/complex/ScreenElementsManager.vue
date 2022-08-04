<template>
  <div class="element-box" >
    <huge> Layers </huge>
    <div class="element-list" >
      <div v-for="element in screenElements" :key="element.id">
        {{ element.description }}
      </div>
    </div>
  </div>  
</template>

<script>

export default {
  name: 'ScreenElementsManager',
  props: ['screenElements'],
  mounted() {
    this.screenElements.sort((a,b) => a.depth - b.depth);
  }
}
</script>
<style scoped>
.element-box {
  background-color: aqua;
}

.element-list {
  overflow-y: scroll;
}
</style>