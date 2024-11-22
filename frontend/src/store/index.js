import { createStore } from 'vuex';

const store = createStore({
  state() {
    return {
      tasks: [], // Initialize tasks as an empty array
    };
  },
  mutations: {
    setTasks(state, tasks) {
      state.tasks = tasks;
    },
    deleteTask(state, taskId) {
      state.tasks = state.tasks.filter(task => task.id !== taskId);
    },
    updateTaskState(state, { taskId, newState }) {
      const task = state.tasks.find(task => task.id === taskId);
      if (task) {
        task.state = newState;
      }
    },
  },
  actions: {
    fetchTasks({ commit }) {
      // Simulating an API call with mock data
      const tasks = [
        { id: '1', title: 'First task', content: 'This is the first task.', state: '1' },
        { id: '2', title: 'Second task', content: 'This is the second task.', state: '2' },
      ];
      commit('setTasks', tasks);
    },
    deleteTask({ commit }, taskId) {
      commit('deleteTask', taskId);
    },
    updateTaskState({ commit }, payload) {
      commit('updateTaskState', payload);
    },
  },
});

export default store;