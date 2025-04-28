# Fansoft PocArc Demo

Uma prova de conceito completa, demonstrando integração de:

- **Back-End:** ASP.NET Core 8 Minimal API
- **Front-End:** ASP.NET Core 8 MVC
- **Autenticação:** Keycloak 26 via client_credentials
- **Banco de Dados:** SQL Server (em container)
- **Orquestração:** Docker Compose

---

## Visão Geral

Este projeto mostra um fluxo realista de autenticação e CRUD completo, containerizado para facilitar o deploy em ambientes Linux (Ubuntu) ou na nuvem (Azure).

---

## Tecnologias Usadas

- .NET 8
- Keycloak 26
- SQL Server 2022 Developer Edition (container)
- Docker e Docker Compose
- EF Core 8 (com migrations automáticas)
- JWT Authentication (client_credentials)

---

## Como Executar Localmente

### 1. Requisitos

- Docker
- Docker Compose
- Git

### 2. Clone o Repositório

```bash
git clone https://github.com/fnalin/pocarcdemo.git
cd pocarcdemo
```

### 3. Suba os containers

```bash
docker-compose up -d --build
```

### 4. Acessos

- **FrontEnd:** http://localhost:3000
- **BackEnd (Swagger):** http://localhost:5001/swagger
- **Keycloak:** http://localhost:8080/admin
  - Realm: `fansoft`
  - ClientId: `backend-api`

### 5. Usuários e Senhas

Para Keycloak, acesse como:

- Admin User: `admin`
- Admin Password: `admin`

> O Client configurado é `backend-api`, com grant type `client_credentials` habilitado.

---

## Estrutura do Docker Compose

- **backend**: API Minimal (.NET 8)
- **frontend**: MVC (.NET 8)
- **sqlserverdb**: Banco de dados SQL Server
- **keycloak**: Gerenciador de identidade
- **healthchecks**: Checks para dependências (db)

---

## Fluxo de Autenticação

1. O FrontEnd requisita token via client_credentials no Keycloak
2. O Token JWT é adicionado ao Header nas chamadas para o BackEnd
3. O BackEnd valida o token usando Keycloak como Authority

---

## Comandos úteis

- **Derrubar os containers:**

```bash
docker-compose down
```

- **Logs de um container:**

```bash
docker logs -f frontend
```

- **Reconstruir containers:**

```bash
docker-compose up -d --build
```

---

## Roadmap Futuro (Ideias)

- Mockar LDAP no desenvolvimento local
- Login automático para integração com Active Directory real
- GitHub Actions para CI/CD
- Deploy automático no Azure Web App + Azure SQL

---

## Autor

Desenvolvido por [Fabiano Nalin](https://github.com/fnalin) 🚀

---

**Feito com dedicação para uma arquitetura moderna, segura e containerizada!** 🌟

