<template>
  <div class="new-screen-creation-tool-box">
    <v-form
      ref="form"
      v-model="valid"
      @submit="(e) => {e.preventDefault(); confirm();}"
    >
      <div class="row-space-between modal-header">
        <h4 style="margin:0px"> Edit element properties </h4>
        <v-btn icon @click="close">
          <font-awesome-icon color icon="xmark" style="fontSize: 1.5rem"/>
        </v-btn>
      </div>
      <v-text-field
        v-model="description"
        :rules="descriptionRules"
        label="Element description"
        outlined
        dense
      />

      <!-- General properties -->
      <template v-if="'width' in properties">
        <v-text-field
          v-model="properties.width"
          :rules="widthRules"
          label="Width (pixels) - leave 0 to not limit"
          class="mb-2"
          outlined
          dense
        />
      </template> 
      <template v-if="'height' in properties">
        <v-text-field
          v-model="properties.height"
          :rules="heightRules"
          label="Height (pixels) - leave 0 to not limit"
          class="mb-2"
          outlined
          dense
        />
      </template> 

      <!-- Images properties-->
      <template v-if="'imageLimitMode' in properties">
        <v-select
          v-model="properties.imageLimitMode"
          :items="['crop', 'scale']"
          label="Image Limiting Mode"
          hint="Crop mode requires height and width limit"
          persistent-hint
          class="mb-2"
          outlined
          dense
        />
      </template>
      <!-- Unique properties editing -->

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


      <!-- Removing element dialog -->
      <v-dialog 
        v-model="showRemoveDialog"
        width="500"
      >
        <template v-slot:activator="{ on }">
          <v-btn
            class="mr-4"
            v-on="on"
          >
            Remove
          </v-btn>
        </template>
        <v-card>
          <v-card-title>
            Are you sure?
          </v-card-title>
          <v-card-text>
            Are you sure you want to delete element {{elementDescription}}?
          </v-card-text>
          <v-card-actions>
            <v-btn
              :disabled="!valid"
              color="success"
              class="mr-4"    
              @click="deleteElement"        
            >
              Yes, delete.
            </v-btn>
            <v-btn
              color="error"
              class="mr-4"
              @click="showRemoveDialog=false"
            >
              No, don't delete.
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

    </v-form>  
  </div>
  
</template>

<script>
export default {
  name: 'ManageElementsPropertiesTool',
  props: {
    elementProperties: {
      type: Object,
      default: () => {}
    },
    elementId: {
      type: String,
      default: ''
    },
    elementDescription: {
      type: String,
      default: ''
    },
  },
  data() {
    return {
      showRemoveDialog: false,
      description: '',
      descriptionRules: [
        v => !!v || 'Description is required',
      ],
      widthRules: [
        v => !!v || 'Width is required',
        v => /^\+?\d+$/.test(v) || 'Value must be an integer.'
      ],
      heightRules: [
        v => !!v || 'Height is required',
        v => /^\+?\d+$/.test(v) || 'Value must be an integer.'
      ],
      properties: {},
      valid: false,
    }
  },
  methods: {
    cancel() {
      this.close();
    },
    confirm() {
      this.$store.dispatch('actionEditElementProperties', {
        id: this.elementId, 
        properties: this.properties,
        description: this.description
      });
      this.close();
    },
    close() {
      this.description = '';
      this.properties = {
      };
      this.$emit('closeTool');
    },
    deleteElement() {
      this.$store.dispatch('actionRemoveElementFromCurrentScreen', this.elementId);
      this.close();
    }
  },
  beforeMount() {
    this.description = this.elementDescription;
    this.properties = { ...this.elementProperties };
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