<template>
    <b-container class="py-4">
        <img alt="Vue logo" src="@/assets/logo.png">
        <b-row>
            <b-col>
                <h1 class="text-center">Gerenciador de Tarefas</h1>
            </b-col>
        </b-row>
        <b-row class="mt-4 justify-content-center">
            <b-col cols="auto">
                <b-button variant="primary" @click="irParaTarefas">Ver Todas as Tarefas</b-button>
            </b-col>
            <b-col cols="auto">
                <b-button variant="success" @click="abrirFormulario">Criar Nova Tarefa</b-button>
            </b-col>
        </b-row>

        <TarefaForm v-if="mostrarFormulario"
                    :value="mostrarFormulario" />
    </b-container>
</template>

<script>
    import TarefaForm from "@/components/TarefaFormComponent"

    export default {
        name: "HomePage",
        components: { TarefaForm },
        data() {
            return {
                mostrarFormulario: false,
                tarefaPadrao: {
                    id: null,
                    titulo: "",
                    descricao: "",
                    status: 0,
                },
            }
        },
        methods: {
            irParaTarefas() {
                this.$router.push("/tarefas")
            },
            abrirFormulario() {
                this.mostrarFormulario = true
                this.$router.push("/tarefas/criar")
            },
            fecharFormulario() {
                this.mostrarFormulario = false
            }       
        },
    }
</script>

<style>
    textarea.form-control {
        max-height: 195px;
    }
</style>
