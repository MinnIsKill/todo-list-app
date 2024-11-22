<template>
  <form @submit.prevent="submitTask">
    <input v-model="title" placeholder="Title" required />
    <textarea v-model="content" placeholder="Description"></textarea>
    <select v-model="state">
      <option value="1">Open</option>
      <option value="2">In Progress</option>
      <option value="3">Finished</option>
    </select>
    <button type="submit">Add Task</button>
  </form>
</template>

<script>
export default {
  data() {
    return {
      title: "",
      content: "",
      state: "1",
    };
  },
  methods: {
    submitTask() {
      this.$store.dispatch("addTask", {
        title: this.title,
        content: this.content,
        state: parseInt(this.state),
      });
      this.title = "";
      this.content = "";
      this.state = "1";
    },
  },
};
</script>
