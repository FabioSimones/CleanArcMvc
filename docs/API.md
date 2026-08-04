# API — CleanArcMvc.API

Documentação de referência da Web API REST. Baseada nos controllers em `CleanArcMvc.API/Controllers`, nos modelos em `CleanArcMvc.API/Models` e na configuração de JWT em `CleanArcMvc.Infra.IoC/DependencyInjectionJWT.cs`.

## URL base

Definida pelo host em que `CleanArcMvc.API` é executada. No perfil de desenvolvimento padrão (`CleanArcMvc.API/Properties/launchSettings.json`):

```
https://localhost:5001
http://localhost:5000
```

Todos os endpoints de recurso estão sob o prefixo `api/`.

## Autenticação

A API usa **JWT Bearer**. Todos os controllers (`TokenController`, `ProductsController`, `CategoriesController`) têm `[Authorize]` na classe; apenas `POST /api/Token/LoginUser` é `[AllowAnonymous]`.

1. Envie usuário e senha para `POST /api/Token/LoginUser`.
2. Use o `Token` retornado no header das demais requisições:

   ```
   Authorization: Bearer <token>
   ```

O token é assinado com HMAC-SHA256 usando a chave configurada em `Jwt:SecretKey` (`appsettings.json` da API) e expira em 10 minutos (`GenerateToken` em `TokenController.cs`). Emissor e audiência são validados contra `Jwt:Issuer` e `Jwt:Audience`.

> A Swagger UI (`/swagger`) expõe um botão "Authorize" pré-configurado para o esquema Bearer (`DependencyInjectionSwagger.cs`), permitindo testar os endpoints autenticados diretamente pelo navegador.

## Endpoints

### `POST /api/Token/LoginUser`

Autentica um usuário existente (via ASP.NET Core Identity) e retorna um token JWT.

Requisição:

```json
{
  "email": "usuario@exemplo.com",
  "password": "SenhaForte123!"
}
```

Restrições do modelo (`LoginModel`): `Email` obrigatório e em formato válido; `Password` obrigatório, entre 10 e 20 caracteres.

Resposta (200):

```json
{
  "token": "<jwt>",
  "expiration": "2026-08-04T12:34:56Z"
}
```

Resposta (400): erro de validação do `ModelState` quando as credenciais são inválidas.

### `POST /api/Token/CreateUser`

Registra um novo usuário via `IAuthenticate.RegisterUser`. Endpoint oculto do Swagger (`[ApiExplorerSettings(IgnoreApi = true)]`), mas exposto na rota.

Requisição: mesmo formato de `LoginModel` usado em `LoginUser`.

### `GET /api/Categories`

Lista todas as categorias (`CategoryDTO`: `Id`, `Name`). Retorna `404` se o serviço não retornar dados.

### `GET /api/Categories/{id}`

Retorna uma categoria por `Id` (rota nomeada `GetCategory`). `404` se não encontrada.

### `POST /api/Categories`

Cria uma categoria a partir de um `CategoryDTO` (`Name` obrigatório, 3–100 caracteres). Retorna `201` com o recurso criado.

### `PUT /api/Categories`

Atualiza uma categoria existente. Requer `id` de query/rota igual ao `Id` do corpo (ver assinatura de `Put(int id, CategoryDTO categoryDTO)` em `CategoriesController.cs`).

### `DELETE /api/Categories`

Remove uma categoria por `id`. `404` se não encontrada.

### `GET /api/Products`

Lista todos os produtos (`ProductDTO`: `Id`, `Name`, `Description`, `Price`, `Stock`, `Image`, `CategoryId`).

### `GET /api/Products/{id}`

Retorna um produto por `Id` (rota nomeada `GetProduct`).

### `POST /api/Products`

Cria um produto a partir de um `ProductDTO`. Validações do DTO: `Name` (3–100), `Description` (5–200), `Price` obrigatório, `Stock` entre 1 e 9999, `Image` até 250 caracteres. Retorna `201`.

### `PUT /api/Products`

Atualiza um produto existente, seguindo a mesma assinatura `Put(int id, ProductDTO productDTO)`.

### `DELETE /api/Products`

Remove um produto por `id`.

## Observações

- Os endpoints de `Products` são processados através de Commands/Queries via MediatR (`ProductService`); os de `Categories` acessam o repositório diretamente (`CategoryService`) — ver [Decisões técnicas](../README.md#decisões-técnicas) no README.
- Não há documentação OpenAPI adicional além da gerada automaticamente pelo Swashbuckle em `/swagger/v1/swagger.json`.
