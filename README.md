# 💬 ChatAPI

Uma API de chat moderna com autenticação, gerenciamento de salas e mensagens, construída com **.NET 8**, **Firebase Authentication** e **Cloud Firestore**.

---

## 🚀 Tecnologias Utilizadas

- .NET 8  
- Firebase Authentication  
- Cloud Firestore  
- FluentValidation  
- DTOs (Request, Response e Internos)  
- Swagger (OpenAPI)

---

## 📦 Estrutura da API

A API está dividida em três domínios principais:

### 🔐 Autenticação (`/api/auth`)

Responsável pelo login, registro e informações do usuário autenticado.

| Método | Endpoint           | Descrição                                |
|--------|--------------------|------------------------------------------|
| POST   | `/register`        | Cria um novo usuário no Firebase Auth    |
| POST   | `/login`           | Retorna o token de autenticação JWT      |
| GET    | `/user`            | Retorna os dados do usuário autenticado  |

**Validações**: nome, e-mail, senha (mínimo 6 caracteres, formato de e-mail válido).

---

### 🧑‍🤝‍🧑 Salas de Chat (`/api/rooms`)

Gerencia a criação de salas, listagem e entrada em salas existentes.

| Método | Endpoint             | Descrição                                  |
|--------|----------------------|--------------------------------------------|
| POST   | `/`                  | Cria uma nova sala                         |
| GET    | `/`                  | Lista as salas que o usuário participa     |
| GET    | `/{id}`              | Retorna os detalhes de uma sala específica |
| POST   | `/{id}/join`         | Adiciona o usuário à sala                  |

**Validações**: nome único, participantes existentes, verificação de duplicidade.

---

### 💬 Mensagens (`/api/messages`)

Envio e consulta de mensagens dentro das salas.

| Método | Endpoint              | Descrição                                  |
|--------|-----------------------|--------------------------------------------|
| POST   | `/`                   | Envia uma mensagem para uma sala           |
| GET    | `/{roomId}`           | Lista mensagens de uma sala específica     |

**Validações**: sala existente, conteúdo não vazio, usuário deve ser membro da sala.

---

## ✅ Validações com FluentValidation

Utilizamos `FluentValidation` para aplicar regras claras e automáticas de validação para cada endpoint, garantindo:

- Entrada limpa e segura.
- Feedbacks detalhados em caso de erro.
- Separação clara dos dados em DTOs de Request, Response e Internos.

---

## 🔐 Autenticação e Segurança

- Integração com Firebase Authentication.
- Verificação automática de token JWT.
- Proteção de endpoints sensíveis com `[Authorize]`.

---

## 🔥 Firestore - Estrutura

Estrutura das collections no Firestore:

```
/rooms/{roomId}
    - name
    - participants
    - createdAt
    /messages/{messageId}
        - senderId
        - content
        - sentAt
```

---

## 🛠️ Como Rodar Localmente

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/chatapi.git
cd chatapi

# Configure o caminho do arquivo de credenciais do Firebase
set GOOGLE_APPLICATION_CREDENTIALS=credentials/serviceAccountKey.json

# Rode o projeto
dotnet run
```

> ⚠️ Certifique-se de ter o `.json` da conta de serviço no caminho correto.

---

## 📑 Documentação Swagger

Acesse a documentação da API via Swagger:

```
https://localhost:5001/swagger
```

---
