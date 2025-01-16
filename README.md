# Documenta��o da API REST To-Do List

## **1� - Processo de Instala��o e Configura��o da Aplica��o**

### Pr�-requisitos
> Certifique-se de que voc� possui os seguintes itens instalados em sua m�quina:
- .NET SDK 6.0 (para o backend).
- Node.js 16.10.0 (para o frontend).
- Gerenciador de pacotes npm (utilizei a vers�o 7.24.0).
- Banco de dados SQL Server.

---

### Passos para Instala��o

#### Backend (C# - API REST)
1. Navegue at� a pasta `/backend` no terminal.
2. Restaure as depend�ncias do projeto com o comando:
   ```bash
   dotnet restore
   ```
3. Configure a conex�o com o banco de dados no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "<sua-string-de-conex�o-aqui>"
   }
   ```

   - Como est� configurado:
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
   - Verifique se o nome da string de conex�o � o mesmo que est� em `appsettings.json` -> "DefaultConnection" ou "SqlServer".

5. Configure a URL da Pol�tica CORS em `Program.cs` para a URL do frontend:
	```csharp
	policy.WithOrigins("http://localhost:8080") // URL do frontend
	      .AllowAnyHeader()
	      .AllowAnyMethod(); 
	```
	- Verifique se a URL `http://localhost:8080` � a mesma do frontend.
   
6. Execute as migra��es para criar o banco de dados:
   ```bash
   dotnet ef database update
   ```
   
7. Inicie o servidor backend:
   ```bash
   dotnet run
   ```
   - O servidor ser� iniciado na URL `https://localhost:7120` -> *Verifique se a porta (`7120`) � a correta*.

---

#### Frontend (Vue.js)
1. Navegue at� a pasta `/frontend` no terminal.
2. Instale as depend�ncias do projeto:
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
	- Verifique se a URL `https://localhost:7120/` � a mesma do backend.
	
4. Inicie o servidor de desenvolvimento:
   ```bash
   npm run serve
   ```
   - O frontend ser� iniciado na URL padr�o `http://localhost:8080` -> *Verifique se a porta (`8080`) � a correta*.

### Testando a Aplica��o
1. Abra o navegador e acesse o frontend em `http://localhost:8080`.
2. Utilize as funcionalidades dispon�veis na interface para interagir com a API.

---

## **2� - Documenta��o dos Endpoints da API REST**

### Criar uma Nova Tarefa
**Descri��o:** Adiciona uma nova tarefa � lista.
- **URL:** `/tarefas/criar`
- **M�todo:** POST
- **Corpo da Requisi��o:**
  ```json
  {
      "titulo": "string",
      "descricao": "string",
      "status": "int" (0 = Pendente, 1 = Em Andamento, 3 = Concluida)
  }
  ```
- **Resposta:**
  - **Sucesso:** C�digo 201 (Created)
    ```json
    {
        "id": "int",
        "titulo": "string",
        "descricao": "string",
        "status": "int"
    }
    ```
  - **Erro:** C�digo 400 (Bad Request)

---

### Visualizar Todas as Tarefas
**Descri��o:** Retorna uma lista de todas as tarefas.
- **URL:** `/tarefas`
- **M�todo:** GET
- **Resposta:**
  - **Sucesso:** C�digo 200 (OK)
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
  - **Erro:** C�digo 500 (Internal Server Error)

---

### Visualizar uma Tarefa Espec�fica
**Descri��o:** Retorna os detalhes de uma tarefa espec�fica com base no ID fornecido.
- **URL:** `/tarefas/:id`
- **M�todo:** GET
- **Par�metros da URL:**
  - `id` (int): ID da tarefa.
- **Resposta:**
  - **Sucesso:** C�digo 200 (OK)
    ```json
    {
        "id": "int",
        "titulo": "string",
        "descricao": "string",
        "status": "int"
    }
    ```
  - **Erro:**
    - C�digo 404 (Not Found): Tarefa n�o encontrada.
    - C�digo 500 (Internal Server Error).

