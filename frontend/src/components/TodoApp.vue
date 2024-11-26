<!-- TodoApp.vue - main app file -->
<template>
    <div>
        <!-- 'Add Task' button-->
        <button @click="openAddTaskForm" class="main-button">Add Task</button>
        
        <!-- modal -->
        <Modal ref="modal">
            <AddTaskForm @add-task="addTask" @close-form="closeAddTaskForm" />
        </Modal>
        
        <!-- Todo List -->
        <TodoList :tasks="todos" @delete-task="deleteTask" @update-task-status="updateTaskStatus" />
    </div>
</template>
  
<script>
    import Modal from './Modal.vue'; // Import Modal component
    import TodoList from './TodoList.vue';
    import AddTaskForm from './AddTaskForm.vue';
    import { mapState } from 'vuex';
    
    export default {
            components: {
            Modal,
            TodoList,
            AddTaskForm,
        },
        computed: {
            ...mapState(['todos']), // Map the Vuex state directly to a computed property
        },
        mounted() {
            // Fetch tasks from the backend when the component is mounted
            this.$store.dispatch('fetchTodos');
        },
        methods: {
            openAddTaskForm() {
                this.$refs.modal.openModal(); // Open the modal using the ref
            },
            closeAddTaskForm() {
                this.$refs.modal.closeModal(); // Close the modal using the ref
            },
            addTask(task) {
                this.$store.dispatch('createTodo', task); // Dispatch Vuex action to create a new todo
                this.closeAddTaskForm(); // Close the modal after adding the task
            },
            deleteTask(taskId) {
                this.$store.dispatch('deleteTodo', taskId); // Dispatch Vuex action to delete the todo
            },
            updateTaskStatus(task) {
                this.$store.dispatch('updateTodoStatus', task); // Dispatch Vuex action to update the todo
            },
        },
    };
</script>