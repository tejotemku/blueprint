<template>
  <div>
    <LoggedInNavbarVue />
    <CreateProjectForm />
  </div>
</template>

<script>
import CreateProjectForm from '../../components/general/CreateProjectForm.vue'
import LoggedInNavbarVue from '../../components/general/LoggedInNavbar.vue'

export default {
  name: 'CreateProject',
  components: {
    CreateProjectForm,
    LoggedInNavbarVue
  }
}
</script>

<style scoped>

</style>