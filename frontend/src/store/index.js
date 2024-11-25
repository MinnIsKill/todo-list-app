import { createStore } from 'vuex';

export const store = createStore({
  state: {
    todos: [],
  },
  mutations: {
    setTodos(state, todos) {
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
      const response = await fetch('/api/todo');
      const data = await response.json();
      commit('setTodos', data);
    },
    async createTodo({ commit }, todo) {
      const response = await fetch('/api/todo', {
        method: 'POST',
        body: JSON.stringify(todo),
        headers: { 'Content-Type': 'application/json' }
      });
      const newTodo = await response.json();
      commit('addTodo', newTodo);
    },
    async updateTodoStatus({ commit }, { id, state }) {
      const response = await fetch(`/api/todo/${id}`, {
        method: 'PUT',
        body: JSON.stringify({ state }),
        headers: { 'Content-Type': 'application/json' }
      });
      const updatedTodo = await response.json();
      commit('updateTodo', updatedTodo);
    },
    async deleteTodo({ commit }, id) {
      await fetch(`/api/todo/${id}`, { method: 'DELETE' });
      commit('deleteTodo', id);
    }
  }
});

export default store;