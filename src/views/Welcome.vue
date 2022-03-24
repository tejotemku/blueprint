<template>
  <div>
    <p>
      For a guide and recipes on how to configure / customize this project,<br>
      check out the
    </p>
    <v-expansion-panels accordion>
      <v-expansion-panel v-for="n in news" :key="n.id">
        <v-expansion-panel-header>{{ n.title }} </v-expansion-panel-header>
        <v-expansion-panel-content>
          {{ n.description }}
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-expansion-panels>
    <v-card
    class="mx-auto"
    max-width="300"
    tile
  >
    <v-list dense>
      <v-subheader>REPORTS</v-subheader>
      <v-list-item-group
        v-model="selectedItem"
        color="primary"
      >
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
        >
          <v-list-item-icon>
            <v-icon v-text="item.icon"></v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-card>
    
  </div>
</template>

<script>
export default {
  name: 'WelcomePage',
  data() {
    return {
      news: [
        {
          id: 1, 
          title: "Test1", 
          description: "Description1"
        }, 
        {
          id: 2, 
          title: "Test2", 
          description: "Description2"
        }
      ],
    
      selectedItem: 1,
      items: [
        { text: 'Real-Time', icon: 'mdi-clock' },
        { text: 'Audience', icon: 'mdi-account' },
        { text: 'Conversions', icon: 'mdi-flag' },
      ],
    };
  }
}
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
