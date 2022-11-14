<template>
  <div class="new-asset-tool-box">
    <v-form
      ref="form"
      v-model="valid"
      @submit="(e) => {e.preventDefault(); confirm();}"
    >
      <div class="row-space-between modal-header">
        <h4 style="margin:0px"> Add Asset </h4>
        <v-btn icon @click="close">
          <font-awesome-icon color icon="xmark" style="fontSize: 1.5rem"/>
        </v-btn>
      </div>
      <v-text-field
        v-model="assetUrl"
        :rules="assetUrlRules"
        label="Image Url"
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
        @click="close"
      >
        Cancel
      </v-btn>
    </v-form>  
  </div>
  
</template>

<script>
export default {
  name: 'NewAssetTool',
  data() {
    return {
      assetUrl: '',
      assetUrlRules: [
        v => !!v || 'Asset url is required',
      ],
      valid: false,
    }
  },
  methods: {
    confirm() {
      this.$store.dispatch('actionAddAsset', this.assetUrl);
      this.close();
    },
    close() {
      this.assetUrl = '';
      this.$emit('closeTool');
    }
  },
}
</script>

<style scoped>
.new-asset-tool-box {
  background-color: rgb(254, 254, 254);
  padding-inline: 30px;
  padding-block: 15px;
  border-radius: 10px;
  z-index: 4000;
}

.modal-header {
  align-items: center;
}
</style>