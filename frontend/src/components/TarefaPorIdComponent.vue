<template>
    <b-modal v-if="tarefa" title="Detalhes da Tarefa" :visible="isModalVisible" @hidden="fecharModal" hide-footer centered>
        <!-- Cabeçalho do Card -->
        <b-card-header class="text-center">
            <strong>Tarefa ID: {{ tarefa.id }}</strong>
        </b-card-header>

        <!-- Corpo do Card -->
        <b-card-body class="pb-0">
            <!-- Status -->
            <b-row class="mb-3">
                <b-col>
                    <b-badge :variant="obterCorDoEmblema(tarefa?.status)">
                        {{ obterTextoDeStatus(tarefa.status) }}
                    </b-badge>
                </b-col>
            </b-row>

            <!-- Título -->
            <b-row class="mb-3">
                <b-col>
                    <b-form-group class="font-weight-bold" label="Titulo:" label-for="titulo">
                        <b-form-input id="titulo"
                                      v-model="tarefa.titulo"
                                      disabled>
                        </b-form-input>
                    </b-form-group>
                </b-col>
            </b-row>

            <!-- Descrição -->
            <b-row class="mb-3">
                <b-col>
                    <b-form-group class="font-weight-bold" label="Descricao:" label-for="descricao">
                        <b-form-textarea id="descricao"
                                         v-model="tarefa.descricao"
                                         rows="5"
                                         disabled>
                        </b-form-textarea>
                    </b-form-group>
                </b-col>
            </b-row>
        </b-card-body>
    </b-modal>
</template>

<script>
    import { mapState } from "vuex"

    export default {
        name: "TarefaPorId",
        computed: mapState(["tarefa", "isModalVisible"]),
        methods: {
            tarefaExiste(tarefa) {
                if (tarefa === null) {
                    this.$router.push("/tarefas")

                    setTimeout(() => {
                        this.$bvToast.toast("Tarefa nao encontrada.", {
                            title: "Erro",
                            variant: "danger",
                            solid: true,
                        })
                    }, 100)
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
            fecharModal() {
                this.$store.commit("setTarefa", null)
                this.$router.go(-1)
            }
        },
        created() {
            this.tarefaExiste(this.$store.state.tarefa)
        }
    }</script>

<style scoped>
    .badge {
        font-size: 1rem;
        padding: .6rem 0;
        width: 100%;
    }

    textarea.form-control {
        resize: none;
    }
</style>
