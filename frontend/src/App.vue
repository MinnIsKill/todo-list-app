<template>
    <div id="app">
        <!-- Main App UI for managing tasks -->
        <TodoApp />
    </div>
</template>

<script>
    import TodoApp from './components/TodoApp.vue';

    export default {
        components: {
            TodoApp,
        },
    };
</script>