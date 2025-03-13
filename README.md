# LibraryApi

LibraryApi é uma API REST desenvolvida em .NET 8 para gerenciar um sistema de biblioteca, permitindo o cadastro, consulta e gerenciamento de autores e livros.

## Tecnologias Utilizadas
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- Swagger

## Como Executar o Projeto
### 1. Clonar o Repositório
```bash
git clone https://github.com/seuusuario/LibraryApi.git
cd LibraryApi
```

### 2. Configurar o Banco de Dados
1. Crie um banco de dados no SQL Server.
2. Configure a string de conexão no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=LibraryDb;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;"
}
```

### 3. Aplicar Migrações
```bash
dotnet ef database update
```

### 4. Executar a API
```bash
dotnet run
```
A API será executada em `https://localhost:7076/`.

## Autenticação
A API usa JWT para autenticação. Para obter um token:

### Endpoint de Login
```http
POST /api/auth/login
```
**Body:**
```json
{
  "email": "user@example.com",
  "password": "senha"
}
```
**Resposta:**
```json
{
  "token": "seu_token_jwt"
}
```

### Usando o Token
Para acessar os endpoints protegidos, adicione o token no cabeçalho:
```bash
-H "Authorization: Bearer seu_token"
```

## Documentação da API
Swagger UI está disponível em:
```
https://localhost:7076/swagger/index.html
```

## Endpoints Principais

### Autores
- `GET /api/authors` - Lista todos os autores
- `POST /api/authors` - Adiciona um novo autor

### Livros
- `GET /api/books` - Lista todos os livros
- `POST /api/books` - Adiciona um novo livro

## Contribuição
1. Fork o repositório
2. Crie uma branch: `git checkout -b feature/sua-feature`
3. Commit suas mudanças: `git commit -m 'Minha nova feature'`
4. Envie para o repositório: `git push origin feature/sua-feature`
5. Abra um Pull Request

## Licença
Este projeto está licenciado sob a MIT License.
