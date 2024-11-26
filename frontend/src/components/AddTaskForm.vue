<template>
    <div>
        <form @submit.prevent="submitForm" class="task-form">
            <input
                v-model="title"
                type="text"
                placeholder="Title..."
                class="form-input"
                required
            />
    
            <textarea
                v-model="content"
                placeholder="Description..."
                class="form-textarea"
                required
            ></textarea>
    
            <select v-model="state" class="form-select">
                <option value="1">Open</option>
                <option value="2">In Progress</option>
                <option value="3">Finished</option>
            </select>
    
            <div class="form-buttons">
                <button type="submit" class="submit-button">Add Task</button>
                <button type="button" class="cancel-button" @click="closeForm">Cancel</button>
            </div>
        </form>
    </div>
</template>
  
<script>
    export default {
        data() {
            return {
                title: '',
                content: '',
                state: '1', // Default to "Open"
            };
        },
        methods: {
            submitForm() {
                const newTask = {
                    id: this.generateId(),
                    title: this.title,
                    content: this.content,
                    state: this.state,
                };
                this.$emit('add-task', newTask);
            },
            closeForm() {
                this.$emit('close-form');
            },
            generateId() { // UUID generator
                return crypto.randomUUID();
            },
        },
    };
</script>