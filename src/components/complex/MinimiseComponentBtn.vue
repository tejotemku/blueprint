<template>
  <div class="bordered-box">
    <v-btn @click="onClick">
      <font-awesome-icon :icon="isMinimised? 'maximize' : 'minimize'" style="fontSize: 1.5rem"/>
    </v-btn>
    <slot v-if="!isMinimised" />
  </div>
</template>

<script>

export default {
  name: 'MinimiseComponentBtn',
  data() {
    return {
      isMinimised: false,
    }
  },
  methods: {
    onClick() {
      this.isMinimised = !this.isMinimised;
    }
  }
  
}
</script>

<style scoped>
.bordered-box {
  border: 1px solid black;
}
</style>