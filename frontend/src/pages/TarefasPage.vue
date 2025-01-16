<template>
    <b-container class="mt-4">
        <b-row>
            <b-col>
                <h1 class="text-center">Lista de Tarefas</h1>
            </b-col>
        </b-row>

        <b-col cols="12" class="d-flex">
            <b-button variant="primary" 
                      size="md" 
                      @click="voltarParaHome" 
                      class="mr-2">
                Voltar
            </b-button>

            <b-button variant="success" @click="abrirFormulario">Criar Nova Tarefa</b-button>
        </b-col>

        <!-- Tabela de Tarefas -->
        <b-row>
            <b-col>
                <b-table :items="tarefasPaginas"
                         :fields="campos"
                         striped
                         hover
                         responsive
                         class="mt-3">
                    <template #cell(titulo)="data">
                        {{ mostrarMetadeTexto(data.item.titulo) }}
                    </template>
                    <template #cell(status)="data">
                        <b-badge :variant="obterCorDoEmblema(data.item.status)">
                            {{ obterTextoDeStatus(data.item.status) }}
                        </b-badge>
                    </template>
                    <template #cell(descricao)="data">
                        {{ mostrarMetadeTexto(data.item.descricao) }}
                    </template>
                </b-table>

                <!-- Paginação -->
                <b-pagination v-model="paginaAtual"
                              :total-rows="tarefas.length"
                              :per-page="itensPorPagina"
                              align="center"></b-pagination>
            </b-col>
        </b-row>

        <TarefaForm v-if="mostrarFormulario" :exibirFormulario="mostrarFormulario" />
    </b-container>
</template>

<script>
    import api from "@/services/api"

    export default {
        name: "TarefasPage",
        data() {
            return {
                mostrarFormulario: false,
                paginaAtual: 1, // Página atual
                itensPorPagina: 8, // Quantidade de itens por página
                tarefas: [],
                campos: [
                    { key: "id", label: "ID" },
                    { key: "titulo", label: "Titulo" },
                    { key: "descricao", label: "Descricao" },
                    { key: "status", label: "Status" },
                ],
            }
        },
        computed: {
            // Calcula as tarefas que devem ser exibidas na página atual
            tarefasPaginas() {
                const inicio = (this.paginaAtual - 1) * this.itensPorPagina
                const fim = inicio + this.itensPorPagina
                return this.tarefas.slice(inicio, fim)
            },
        },
        methods: {
            voltarParaHome() {
                this.$router.push('/')
            },
            abrirFormulario() {
                this.mostrarFormulario = true
                this.$router.push("/tarefas/criar")
            },
            async carregarTarefas() {
                try {
                    const resp = await api.get("/tarefas")
                    this.tarefas = resp.data
                } catch (error) {
                    this.$bvToast.toast("Erro ao carregar tarefas.", {
                        title: "Erro",
                        variant: "danger",
                        solid: true,
                    })
                }
            },
            obterTextoDeStatus(status) {
                switch (status) {
                    case 0:
                        return "Pendente"
                    case 1:
                        return "Em Andamento"
                    case 2:
                        return "Concluida"
                    default:
                        return "Desconhecido"
                }
            },
            obterCorDoEmblema(status) {
                switch (status) {
                    case 0:
                        return "warning"
                    case 1:
                        return "primary"
                    case 2:
                        return "success"
                    default:
                        return "secondary"
                }
            },
            mostrarMetadeTexto(texto) {
                const limite = 15
                if (texto.length > limite) {
                    return texto.slice(0, limite) + '...'
                }
                return texto
            }
        },
        created() {
            this.carregarTarefas()
        },
    }
</script>

<style scoped>
    .text-center {
        text-align: center;
    }
</style>
