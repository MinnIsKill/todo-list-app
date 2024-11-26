<!-- TodoList.vue -->

<template>
    <div class="todo-items-container">
        <div v-for="task in tasks" :key="task.id" class="todo-item">
            <div class="todo-item-header">
                <h3 class="todo-item-title">{{ task.title }}</h3>
            </div>
            <p class="todo-item-description">{{ task.content }}</p>
            <div class="todo-item-status">
                <select v-model="task.state" @change="updateStatus(task)">
                    <option value="1">Open</option>
                    <option value="2">In Progress</option>
                    <option value="3">Finished</option>
                </select>
            </div>
            <div class="todo-item-actions">
                <button class="btn delete-btn" @click="deleteTask(task.id)">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>
    </div>
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