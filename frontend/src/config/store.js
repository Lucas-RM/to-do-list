import Vue from "vue"
import Vuex from "vuex"

Vue.use(Vuex)

export default new Vuex.Store({
	state: {
		isModalVisible: false,
		tarefa: null,
	},
	mutations: {		
		setTarefa(state, tarefa) {
			state.tarefa = tarefa

			if (tarefa) state.isModalVisible = true
			else state.isModalVisible = false
		},
	},
})
