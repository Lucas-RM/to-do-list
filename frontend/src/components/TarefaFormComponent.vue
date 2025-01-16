<template>
    <b-modal :visible="mostrarFormulario"
             @hidden="fecharFormulario"
             title="Criar Nova Tarefa"
             hide-footer>
        <b-form @submit.prevent="criarTarefa">
            <b-container>
                <b-row>
                    <!-- Campo Título -->
                    <b-col cols="12" class="mb-3">
                        <b-form-group label="Titulo" label-for="tarefa-titulo">
                            <b-form-input id="tarefa-titulo"
                                          v-model="tarefa.titulo"
                                          maxlength="50"
                                          placeholder="Digite o titulo da tarefa">
                            </b-form-input>
                        </b-form-group>
                    </b-col>

                    <!-- Campo Descrição -->
                    <b-col cols="12" class="mb-3">
                        <b-form-group label="Descricao" label-for="tarefa-desc">
                            <b-form-textarea id="tarefa-desc"
                                             v-model="tarefa.descricao"
                                             rows="4"
                                             :max-length="255"
                                             placeholder="Adicione uma descricao para a tarefa">
                            </b-form-textarea>
                        </b-form-group>
                    </b-col>

                    <!-- Campo Status -->
                    <b-col cols="12" class="mb-3">
                        <b-form-group label="Status" label-for="tarefa-status">
                            <b-form-select id="tarefa-status"
                                           v-model="tarefa.status"
                                           :options="opcoesStatus">
                            </b-form-select>
                        </b-form-group>
                    </b-col>

                    <!-- Botões de Criar e Cancelar -->
                    <b-col cols="12" class="d-flex">
                        <b-button type="submit"
                                  variant="success"
                                  size="md"
                                  class="mr-2">
                            Criar Tarefa
                        </b-button>
                        <b-button variant="secondary"
                                  size="md"
                                  @click="fecharFormulario">
                            Cancelar
                        </b-button>
                    </b-col>
                </b-row>
            </b-container>
        </b-form>
    </b-modal>
</template>

<script>
    import api from "@/services/api"

    export default {
        name: "TarefaForm",
        data() {
            return {
                mostrarFormulario: false,
                tarefa: {
                    titulo: "",
                    descricao: "",
                    status: 0,
                },
                opcoesStatus: [
                    { value: 0, text: "Pendente" },
                    { value: 1, text: "Em Andamento" },
                    { value: 2, text: "Concluida" },
                ]
            }
        },
        methods: {
            async criarTarefa() {
                try {
                    // Criar nova tarefa
                    await api.post("/tarefas/criar", this.tarefa)
                    this.$bvToast.toast("Tarefa criada com sucesso!", {
                        title: "Sucesso",
                        variant: "success",
                        solid: true,
                        autoHideDelay: 1000
                    })

                    setTimeout(() => {
                        this.$router.push("/tarefas")
                    }, 900)
                } catch (error) {
                    if (error.response && error.response.data) {
                        const erros = error.response.data.errors

                        // Iterar sobre as propriedades dos erros
                        Object.keys(erros).forEach((campo) => {
                            erros[campo].forEach((mensagem) => {                               
                                this.$bvToast.toast(mensagem, {
                                    title: "Erro",
                                    variant: "danger",
                                    solid: true,
                                })
                            })
                        })
                    } else {
                        this.$bvToast.toast("Erro ao criar a tarefa.", {
                            title: "Erro",
                            variant: "danger",
                            solid: true,
                        })
                    }
                }
            },
            fecharFormulario() {
                this.mostrarFormulario = false
                this.$router.go(-1)
            }
        },
        mounted() {
            this.mostrarFormulario = true
        }
    }</script>

<style>
    textarea.form-control {
        max-height: 195px;
    }
</style>
