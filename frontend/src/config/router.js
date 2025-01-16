import Vue from "vue"
import VueRouter from "vue-router"

import Home from "@/pages/HomePage"
import TarefasPage from "@/pages/TarefasPage"
import TarefaForm from "@/components/TarefaFormComponent"
import TarefaPorId from "@/components/TarefaPorIdComponent"

Vue.use(VueRouter)

const routes = [
    {
        name: "home",
        path: "/",
        component: Home
    },
    {
        name: "tarefas",
        path: "/tarefas",
        component: TarefasPage
    },
    {
        name: "tarefaForm",
        path: "/tarefas/criar",
        component: TarefaForm
    },
    {
        name: "tarefaPorId",
        path: "/tarefas/:id",
        component: TarefaPorId
    },
]

const router = new VueRouter({
    mode: "history",
    routes,
})

export default router
