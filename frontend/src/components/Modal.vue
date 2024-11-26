<!-- Modal.vue -->

<template>
    <div v-if="isVisible" class="modal-overlay" @click.self="closeModal">
        <div class="modal-content">
            <slot></slot>
        </div>
    </div>
</template>
  
<script>
    export default {
        data() {
            return {
                isVisible: false,
            };
        },
        methods: {
            openModal() {
                this.isVisible = true;
            },
            closeModal() {
                this.isVisible = false;
            },
        },
    };
</script>