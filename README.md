# pocarcdemo

Criar .env:

# --- SQL Server ---
SA_PASSWORD=Your_strong_password123

# Senha dos usuários no banco
API_USER_PASSWORD=123456@qwerty
KEYCLOAK_DB_PASSWORD=123456@qwerty

# Nome dos Bancos
CUSTOMERS_DATABASE_NAME=CustomersDb
KEYCLOAK_DATABASE_NAME=KeycloakDb

# --- Keycloak ---
KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=admin

# --- Configuração da API (Autenticação) ---
AUTHORITY=http://keycloak:8080/realms/fansoft
AUDIENCE=backend-api
REQUIRE_HTTPS_METADATA=false

# --- Configuração do front
KEYCLOAK_TOKEN_URL=http://keycloak:8080/realms/fansoft/protocol/openid-connect/token
KEYCLOAK_CLIENT_ID=backend-api
KEYCLOAK_CLIENT_SECRET=**********
BACKEND_BASE_URL=http://backendapi:8080
