<!-- TodoApp.vue - main app file -->

<template>
  <div>
    <button @click="openAddTaskForm" class="main-button">Add Task</button>
    
    <!-- Modal to add task -->
    <Modal ref="modal">
      <AddTaskForm @add-task="addTask" @close-form="closeAddTaskForm" />
    </Modal>
    
    <!-- Display Todo List -->
    <TodoList :tasks="tasks" @delete-task="deleteTask" @update-task-status="updateTaskStatus"/>
  </div>
</template>

<script>
import Modal from './Modal.vue';  // Import Modal component
import TodoList from './TodoList.vue';
import AddTaskForm from './AddTaskForm.vue';

export default {
  components: {
    Modal,
    TodoList,
    AddTaskForm,
  },
  data() {
    return {
      tasks: [],
    };
  },
  methods: {
    openAddTaskForm() {
      this.$refs.modal.openModal(); // Open the modal using the ref
    },
    closeAddTaskForm() {
      this.$refs.modal.closeModal(); // Close the modal using the ref
    },
    addTask(task) {
      this.tasks.push(task);
      this.closeAddTaskForm(); // Close the modal after adding the task
    },
    deleteTask(taskId) {
      this.tasks = this.tasks.filter(task => task.id !== taskId);
    },
    updateTaskStatus(updatedTask) {
      const index = this.tasks.findIndex(task => task.id === updatedTask.id);
      if (index !== -1) {
        this.tasks[index] = updatedTask;
      }
    },
  },
};
</script>