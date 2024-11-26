import { createStore } from 'vuex';

export const store = createStore({
    state: {
        todos: [],
    },
    mutations: {
        setTodos(state, todos) {
            console.log('Mutating state with todos:', todos);
            state.todos = todos;
        },
        addTodo(state, todo) {
            state.todos.push(todo);
        },
        updateTodo(state, updatedTodo) {
            const index = state.todos.findIndex(todo => todo.id === updatedTodo.id);
            if (index !== -1) {
                state.todos[index] = updatedTodo;
            }
        },
        deleteTodo(state, id) {
            state.todos = state.todos.filter(todo => todo.id !== id);
        }
    },
    actions: {
        async fetchTodos({ commit }) {
            console.log('fetchTodos reached');
            const apiUrl = import.meta.env.VITE_API_URL;
            const response = await fetch(`${apiUrl}/api/todo`);
            const data = await response.json();
            console.log('Fetched data:', data); // Log the data
            commit('setTodos', data);
        },
        async createTodo({ commit }, todo) {
            try {
                const apiUrl = import.meta.env.VITE_API_URL; // Fetch the URL from environment variables
                console.log('Sending request to add Todo:', todo);
                const response = await fetch(`${apiUrl}/api/todo`, {
                    method: 'POST',
                    body: JSON.stringify(todo),
                    headers: { 'Content-Type': 'application/json' },
                });
        
                if (!response.ok) {
                    throw new Error(`Error: ${response.statusText}`);
                }
        
                const newTodo = await response.json();
                commit('addTodo', newTodo);
        
                console.log('Todo added successfully:', newTodo);
            } catch (error) {
                console.error('Error adding todo:', error);
            }
        },
        async updateTodoStatus({ commit }, todo) {
            try {
                const apiUrl = import.meta.env.VITE_API_URL; // Fetch the URL from environment variables
                console.log('Sending request to update Todo:', todo);
                const response = await fetch(`${apiUrl}/api/todo/${todo.id}`, {
                    method: 'PUT',
                    body: JSON.stringify(todo),
                    headers: { 'Content-Type': 'application/json' },
                });
                
                if (!response.ok) {
                    throw new Error(`Error: ${response.statusText}`);
                }
    
                const updatedTodo = await response.json();
                commit('updateTodo', updatedTodo);
    
                console.log("Todo updated successfully:", updatedTodo);
            } catch (error) {
                console.error('Error updating todo:', error);
            }
        },
        async deleteTodo({ commit }, id) {
            const apiUrl = import.meta.env.VITE_API_URL; // Fetch the URL from environment variables
            console.log('Sending request to delete Todo:', id);
            const response = await fetch(`${apiUrl}/api/todo/${id}`, { method: 'DELETE' });
            if (response.ok) {
                commit('deleteTodo', id);
                console.log("Todo deleted successfully:", id);
            } else {
                throw new Error(`Error: ${response.statusText}`);
            }
        },
    }
});

export default store;