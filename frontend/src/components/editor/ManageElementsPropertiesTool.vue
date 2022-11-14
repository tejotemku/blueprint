<template>
  <div class="new-screen-creation-tool-box">
    <v-form
      ref="form"
      v-model="valid"
      @submit="(e) => {e.preventDefault(); confirm();}"
      class="v-form-cl"
    >
      <div class="row-space-between modal-header">
        <h4 style="margin:0px; margin-block-end: ;"> Edit element properties </h4>
        <v-btn icon @click="close">
          <font-awesome-icon color icon="xmark" style="fontSize: 1.5rem"/>
        </v-btn>
      </div>
      <div class="v-form-wrap mt-1 mb-1">
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
            type="number"
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
            type="number"
          />
        </template> 
        <template v-if="'redirect' in properties">
          <v-select
            v-model="properties.redirect"
            :items="screensInfo"
            label="Change to screen by clicking"
            persistent-hint
            class="mb-2"
            outlined
            dense
          />
        </template> 
        <template v-if="'text' in properties">
          <v-text-field
            v-model="properties.text"
            label="Label or placeholder"
            class="mb-2"
            outlined
            dense
            type="text"
          />
        </template>
        <template v-if="'textColor' in properties">
          <div class="property-box">
            <p class="property-label">Text Color</p>
            <v-color-picker
              v-model="properties.textColor"
              dot-size="14"
              mode="rgba"
              swatches-max-height="250"
            />
          </div>
        </template> 
        <template v-if="'borderColor' in properties">
          <div class="property-box">
            <p class="property-label">Border Color</p>
            <v-color-picker
              v-model="properties.borderColor"
              dot-size="14"
              mode="rgba"
              swatches-max-height="250"
            />
          </div>
        </template> 
        <template v-if="'backgroundColor' in properties">
          <div class="property-box">
            <p class="property-label">Background Color</p>
            <v-color-picker
              v-model="properties.backgroundColor"
              dot-size="14"
              mode="rgba"
              swatches-max-height="250"
            />
          </div>
        </template>


        
        <!-- ========================= -->
        <!-- ========================= -->
        <!-- Unique properties editing -->
        <!-- ========================= -->
        <!-- ========================= -->

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

        <template v-if="'checked' in properties">
          <v-switch
            v-model="properties.checked"
            label="Is Checkbox checked"
            class="mb-2"
            outlined
            dense
          />
        </template>

        
        <!-- Save / Delete buttons -->

      </div>
      <div class="button-row">
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
      </div>
    </v-form>  
  </div>
  
</template>

<script>
import { mapGetters } from "vuex";


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
    }
  },
  data() {
    return {
      showRemoveDialog: false,
      description: '',
      descriptionRules: [
        v => !!v || 'Description is required',
      ],
      widthRules: [
        v => /^\+?\d+$/.test(v) || v == null || 'Value must be an integer.'
      ],
      heightRules: [
        v => /^\+?\d+$/.test(v) || v == null || 'Value must be an integer.'
      ],
      properties: {},
      valid: false,
    }
  },
  computed: {
    ...mapGetters({
      screens: 'getScreensInfo',
      currentScreenId: 'getCurrentScreenId'
    }),
    screensInfo: function () {
      const screens = this.screens;
      let processedScreens = [{
          text: "No redirect",
          value: null,
        }];
      for (const [screenId, screenData] of Object.entries(screens)) {
        if (screenId == this.currentScreenId) continue;
        const processedScreen = {
          text: screenData.name,
          value: screenId,
        }
        processedScreens.push(processedScreen);
      }
      return processedScreens;
    } 
  },
  methods: {
    cancel() {
      this.close();
    },
    confirm() {
      this.properties.width = this.properties.width == '0' ? null :  this.properties.width;
      this.properties.height = this.properties.height == '0' ? null :  this.properties.height;
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
  background-color: rgb(254, 254, 254);
  padding-inline: 30px;
  padding-block: 15px;
  border-radius: 10px;
  max-height: 90vh;
  display: block;
  z-index: 4000;
  width: fit-content important;
}

.v-form-wrap {
  display: flex;
  flex-flow: column wrap;
  max-height: 80vh;
  min-width: min-content;
  margin: -10px;
  min-width: 60vw;
}
.v-form-wrap>* {
  flex: auto;
  flex-basis: 30%;
  margin: 10px;
}

.v-form-cl {
  max-width: 90vw !important;
  width: fit-content !important
}

.modal-header {
  align-items: center;
}
.button-row {
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
}

.property-label {
  color: #444;
}

.property-box {
  padding: 3px;
  border-radius: 5px;
  /* border: 1px solid #aaa; */
  background: rgba(204, 204, 204, 0.2);
}
</style>