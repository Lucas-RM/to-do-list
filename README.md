# Documentação da API REST To-Do List

## **1º - Processo de Instalação e Configuração da Aplicação**

### Pré-requisitos
> Certifique-se de que você possui os seguintes itens instalados em sua máquina:
- .NET SDK 6.0 (para o backend).
- Node.js 16.10.0 (para o frontend).
- Gerenciador de pacotes npm (utilizei a versão 7.24.0).
- Banco de dados SQL Server.

---

### Passos para Instalação

#### Backend (C# - API REST)
1. Navegue até a pasta `/backend` no terminal.
2. Restaure as dependências do projeto com o comando:
   ```bash
   dotnet restore
   ```
3. Configure a conexão com o banco de dados no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "<sua-string-de-conexão-aqui>"
   }
   ```

   - Como está configurado:
		```json
		"ConnectionStrings": {
			"SqlServer": "Server=localhost\\sqlexpress; initial Catalog=ToDoList; Integrated Security=True"
		}
		```

4. Em `Program.cs` configure o banco de dados:
   ```csharp
   builder.Services.AddDbContext<DbContexto>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")
    )); 
   ```
   - Verifique se o nome da string de conexão é o mesmo que está em `appsettings.json` -> "DefaultConnection" ou "SqlServer".

5. Configure a URL da Política CORS em `Program.cs` para a URL do frontend:
	```csharp
	policy.WithOrigins("http://localhost:8080") // URL do frontend
	      .AllowAnyHeader()
	      .AllowAnyMethod(); 
	```
	- Verifique se a URL `http://localhost:8080` é a mesma do frontend.
   
6. Execute as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update
   ```
   
7. Inicie o servidor backend:
   ```bash
   dotnet run
   ```
   - O servidor será iniciado na URL `https://localhost:7120` -> *Verifique se a porta (`7120`) é a correta*.

---

#### Frontend (Vue.js)
1. Navegue até a pasta `/frontend` no terminal.
2. Instale as dependências do projeto:
   ```bash
   npm install
   ```
3. Configure o Axios em `/services/api.js` com a URL do backend, por exemplo:
	```js
	import axios from 'axios'
	
	const api = axios.create({
		baseURL: 'https://localhost:7120/',
		timeout: 10000,
	});
	export default api
	```
	- Verifique se a URL `https://localhost:7120/` é a mesma do backend.
	
4. Inicie o servidor de desenvolvimento:
   ```bash
   npm run serve
   ```
   - O frontend será iniciado na URL padrão `http://localhost:8080` -> *Verifique se a porta (`8080`) é a correta*.

### Testando a Aplicação
1. Abra o navegador e acesse o frontend em `http://localhost:8080`.
2. Utilize as funcionalidades disponíveis na interface para interagir com a API.

---

## **2º - Documentação dos Endpoints da API REST**

### Criar uma Nova Tarefa
**Descrição:** Adiciona uma nova tarefa à lista.
- **URL:** `/tarefas/criar`
- **Método:** POST
- **Corpo da Requisição:**
  ```json
  {
      "titulo": "string",
      "descricao": "string",
      "status": "int" (0 = Pendente, 1 = Em Andamento, 3 = Concluida)
  }
  ```
- **Resposta:**
  - **Sucesso:** Código 201 (Created)
    ```json
    {
        "id": "int",
        "titulo": "string",
        "descricao": "string",
        "status": "int"
    }
    ```
  - **Erro:** Código 400 (Bad Request)

---

### Visualizar Todas as Tarefas
**Descrição:** Retorna uma lista de todas as tarefas.
- **URL:** `/tarefas`
- **Método:** GET
- **Resposta:**
  - **Sucesso:** Código 200 (OK)
    ```json
    [
        {
            "id": "int",
            "titulo": "string",
            "descricao": "string",
            "status": "int"
        }
    ]
    ```
  - **Erro:** Código 500 (Internal Server Error)

---

### Visualizar uma Tarefa Específica
**Descrição:** Retorna os detalhes de uma tarefa específica com base no ID fornecido.
- **URL:** `/tarefas/:id`
- **Método:** GET
- **Parâmetros da URL:**
  - `id` (int): ID da tarefa.
- **Resposta:**
  - **Sucesso:** Código 200 (OK)
    ```json
    {
        "id": "int",
        "titulo": "string",
        "descricao": "string",
        "status": "int"
    }
    ```
  - **Erro:**
    - Código 404 (Not Found): Tarefa não encontrada.
    - Código 500 (Internal Server Error).

