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

            <b-button variant="success"
                      @click="abrirFormulario">
                Criar Nova Tarefa
            </b-button>
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
                    <template #cell(acoes)="data">
                        <b-button size="sm"
                                  variant="info"
                                  @click="abrirModalTarefaPorId(data.item.id)">
                            Abrir Tarefa
                        </b-button>
                    </template>
                </b-table>

                <!-- Pagina��o -->
                <b-pagination v-model="paginaAtual"
                              :total-rows="tarefas.length"
                              :per-page="itensPorPagina"
                              align="center"></b-pagination>
            </b-col>
        </b-row>
        <TarefaPorId v-if="isModalVisible" />
    </b-container>
</template>

<script>
    import { mapState } from "vuex"
    import api from "@/services/api"
    import TarefaPorId from "@/components/TarefaPorIdComponent"

    export default {
        name: "TarefasPage",
        components: { TarefaPorId },
        data() {
            return {
                paginaAtual: 1, // P�gina atual
                itensPorPagina: 8, // Quantidade de itens por p�gina
                tarefas: [],
                campos: [
                    { key: "id", label: "ID" },
                    { key: "titulo", label: "Titulo" },
                    { key: "descricao", label: "Descricao" },
                    { key: "status", label: "Status" },
                    { key: "acoes", label: "Acoes", sortable: false },
                ],
            }
        },
        computed: {
            ...mapState(["isModalVisible"]),

            // Calcula as tarefas que devem ser exibidas na p�gina atual
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
                this.$router.push("/tarefas/criar")
            },
            async abrirModalTarefaPorId(id) {                       
                try {
                    const resp = await api.get(`/tarefas/${id}`)
                    
                    this.$store.commit("setTarefa", resp.data)
                    this.$router.push(`/tarefas/${id}`)
                } catch (error) {
                    if (error.response && error.response.status === 404) {
                        this.$bvToast.toast("Tarefa n�o encontrada.", {
                            title: "Erro",
                            variant: "danger",
                            solid: true,
                        })

                        this.$router.push("/tarefas")
                    } else {
                        this.$bvToast.toast("Erro ao carregar tarefa.", {
                            title: "Erro",
                            variant: "danger",
                            solid: true,
                        })

                        setTimeout(() => {
                            this.$router.push("/tarefas")
                        }, 1500)
                    }
                }
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
        }        
    }
</script>

<style scoped>
    .text-center {
        text-align: center;
    }
</style>
