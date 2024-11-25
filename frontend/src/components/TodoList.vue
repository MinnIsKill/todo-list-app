<!-- TodoList.vue -->

<template>
  <table>
    <thead>
      <tr>
        <th class="fixed-width">Title</th>
        <th class="fixed-width">Description</th>
        <th class="fixed-width">Status</th>
        <th class="fixed-width">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="task in tasks" :key="task.id">
        <td>{{ task.title }}</td>
        <td>{{ task.content }}</td>
        <td>
          <select v-model="task.state">
            <option value="1">Open</option>
            <option value="2">In Progress</option>
            <option value="3">Finished</option>
          </select>
        </td>
        <td>
          <button @click="deleteTask(task.id)">Delete</button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  props: {
    tasks: Array,
  },
  methods: {
    deleteTask(taskId) {
      this.$emit('delete-task', taskId);
    },
    updateStatus(task) {
      this.$emit('update-task-status', task);
    },
  },
};
</script>