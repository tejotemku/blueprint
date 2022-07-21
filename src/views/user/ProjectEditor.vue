<template>
  <div>
    <LoggedInNavbarVue />
    Project Editor site - project id {{this.$route.params.id}}
  </div>

</template>

<script>
import LoggedInNavbarVue from '../../components/elementary/LoggedInNavbar.vue'


export default {
  name: "ProjectEditor",
  components: {
    LoggedInNavbarVue
  }
}
</script>
