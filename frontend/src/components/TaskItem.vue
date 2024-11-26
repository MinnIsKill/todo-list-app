<!-- TaskItem.vue -->
 
<template>
    <div>
        <h3>{{ task.title }}</h3>
        <p>{{ task.content }}</p>
        <p>
            Status:
            {{
                task.state === 1
                ? "Open"
                : task.state === 2
                ? "In Progress"
                : "Finished"
            }}
        </p>
    </div>
</template>

<script>
    export default {
        props: {
            task: Object,
        },
    };
</script>
