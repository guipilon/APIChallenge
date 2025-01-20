<template>
  <div id="app">
    <header>
      <h1>Welcome to Job Listings</h1>
    </header>
    <JobList />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import JobList from './components/JobList.vue';

export default defineComponent({
  name: 'App',
  components: {
    JobList,
  },
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

header {
  margin-bottom: 20px;
}
</style>
