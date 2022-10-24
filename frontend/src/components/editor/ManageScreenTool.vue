<template>
  <div class="manage-screen-tool-box">
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
      <v-switch
        v-model="setThisAsDefaultScreen"
        v-if="!isDefaultScreen()"
        label="Set as default screen?"
      >
      </v-switch>
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

      <!-- Removing screen dialog -->
      <v-dialog 
        v-model="showRemoveDialog"
        v-if="editingMode" 
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
            Are you sure you want to delete screen {{screenName}}?
          </v-card-text>
          <v-card-actions>
            <v-btn
              :disabled="!valid"
              color="success"
              class="mr-4"    
              @click="deleteScreen"        
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
import { mapGetters } from "vuex";

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
      showRemoveDialog: false,
      setThisAsDefaultScreen: false,
      screenName: '',
      screenNameRules: [
        v => !!v || 'Screen name is required',
      ],
      valid: false,
    }
  },
  computed: {
    ...mapGetters({
      defaultScreenId: 'getDefaultScreenId'
    }),
  },
  methods: {
    cancel() {
      this.close();
    },
    confirm() {
      let id = this.screenInfo.id || Date.now() + '';
      if (this.editingMode) {
        this.$store.dispatch('actionChangeScreenName', {
            id: id, 
            name: this.screenName
        });
      } else {
        this.$store.dispatch('actionsAddScreenToProject',  {
            id: id, 
            name: this.screenName
        });
      }
      if (this.setThisAsDefaultScreen) {
        this.$store.dispatch('actionSetDefaultScreenId', id);
      }
      this.close();
    },
    close() {
      this.screenName = '';
      this.$emit('closeTool');
    },
    deleteScreen() {
      this.$store.dispatch('actionsRemoveScreenFromProject', this.screenInfo.id);
      this.showRemoveDialog = false;
      this.close()
    },
    isDefaultScreen() {
      return this.defaultScreenId == this.screenInfo.id;
    }
  },
  beforeMount() {
    if (this.editingMode) {
      this.screenName = this.screenInfo.name ? this.screenInfo.name : ''
    } else {
      this.screenName = '';
    }
  }
}
</script>

<style scoped>
.manage-screen-tool-box {
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