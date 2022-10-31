<template>
  <div @click="goToCorrectPage" class="er404">
    <h1>404 page not found</h1>
    <hr />
    <h2>Comeback</h2>
  </div>
</template>

<script>
import router from '@/router'
export default {
  name: 'PageNotFound',
  components: {
  },
  methods: {
    goToCorrectPage() {
      router.push("/");
    }
  }
}
</script>

<style>
.er404 {
  color: red;
}

.er404:hover {
  color:maroon;
}
</style>
