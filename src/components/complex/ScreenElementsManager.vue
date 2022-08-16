<template>
  <v-list>
    <v-subheader> LAYER VIEW </v-subheader>
    <v-list-item-group
        v-model="selectedItem"
        color="primary"
    >
      <v-list-item v-for="element in screenElements" :key="element.id" draggable>
        <v-list-item-content>
          <v-list-item-title> {{ element.description }} </v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list-item-group>
  </v-list>
</template>

<script>

export default {
  name: 'ScreenElementsManager',
  props: ['screenElements'],
  mounted() {
    this.screenElements.sort((a,b) => a.depth - b.depth);
  },
  data() {
    return {
      selectedItem: null,
    }
  }
}
</script>
<style scoped>
.element-box {
  border: solid black 1px;
}

.element-list {
  overflow-y: overlay;
}
</style>