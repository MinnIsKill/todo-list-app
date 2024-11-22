<template>
  <div>
    <button @click="openAddTaskForm">Add Task</button>

    <table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Description</th>
          <th>Status</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Sample Task</td>
          <td>This is a static task.</td>
          <td>
            <select>
              <option value="1">Open</option>
              <option value="2">In Progress</option>
              <option value="3">Finished</option>
            </select>
          </td>
          <td>
            <button>Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  methods: {
    openAddTaskForm() {
      console.log('Add task button clicked!');
    },
  },
};
</script>