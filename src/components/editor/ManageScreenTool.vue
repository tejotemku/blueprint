<template>
  <div class="new-screen-creation-tool-box">
    <v-form
      ref="form"
      v-model="valid"
      @submit="(e) => {e.preventDefault(); confirm();}"
    >
      <div class="row-space-between modal-header">
        <h4 style="margin:0px"> {{editingMode? 'Edit a screen':  'Create a screen' }}</h4>
        <v-btn icon @click="close">
          <font-awesome-icon color icon="xmark" style="fontSize: 1.5rem"/>
        </v-btn>
      </div>
      <v-text-field
        v-model="screenName"
        :rules="screenNameRules"
        label="Screen name"
        outlined
        dense
      />
      <v-btn
        :disabled="!valid"
        color="success"
        class="mr-4"
        type="submit"
      >
        Confirm
      </v-btn>
      <v-btn
        color="error"
        class="mr-4"
        @click="cancel"
      >
        Cancel
      </v-btn>
    </v-form>  
  </div>
  
</template>

<script>
export default {
  name: 'ManageScreenTool',
  props: {
    editingMode: {
      type: Boolean,
      default: false
    },
    screenInfo: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      screenName: '',
      screenNameRules: [
        v => !!v || 'Screen name is required',
      ],
      valid: false,
    }
  },
  methods: {
    cancel() {
      this.screenName = '';
      this.close();
    },
    confirm() {
      if (this.editingMode) {
        this.$store.dispatch('actionChangeScreenName', {
            id: this.screenInfo.id, 
            name: this.screenName
        });
      } else {
        this.$store.dispatch('actionsAddScreenToProject', this.screenName);
      }
      this.close();
    },
    close() {
      this.$emit('closeTool');

    }
  },
  beforeMount() {
    if (this.editingMode) {
      this.screenName = this.screenInfo.name ? this.screenInfo.name : ''
    }
  }
}
</script>

<style scoped>
.new-screen-creation-tool-box {
  background-color: rgba(245, 245, 220, 1);
  padding-inline: 30px;
  padding-block: 15px;
  border-radius: 10px;
}

.modal-header {
  align-items: center;
}
</style>