-- =========================
-- Create Database CustomersDb
-- =========================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CustomersDb')
    BEGIN
        CREATE DATABASE CustomersDb;
        PRINT '✅ Database CustomersDb created.';
    END
ELSE
    BEGIN
        PRINT 'ℹ️ Database CustomersDb already exists.';
    END
GO

-- =========================
-- Create Database KeycloakDb
-- =========================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'KeycloakDb')
    BEGIN
        CREATE DATABASE KeycloakDb;
        PRINT '✅ Database KeycloakDb created.';
    END
ELSE
    BEGIN
        PRINT 'ℹ️ Database KeycloakDb already exists.';
    END
GO

-- =========================
-- Create Login and User for Keycloak
-- =========================
IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'keycloak')
    BEGIN
        CREATE LOGIN keycloak WITH PASSWORD = '123456@qwerty';
        PRINT '✅ Login keycloak created.';
    END
ELSE
    BEGIN
        PRINT 'ℹ️ Login keycloak already exists.';
    END
GO

USE KeycloakDb;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'keycloak')
    BEGIN
        CREATE USER keycloak FOR LOGIN keycloak;
        ALTER ROLE db_owner ADD MEMBER keycloak;
        PRINT '✅ User keycloak created and added to db_owner in KeycloakDb.';
    END
ELSE
    BEGIN
        PRINT 'ℹ️ User keycloak already exists in KeycloakDb.';
    END
GO

-- =========================
-- Create Login and User for API
-- =========================
IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'api')
    BEGIN
        CREATE LOGIN api WITH PASSWORD = '123456@qwerty';
        PRINT '✅ Login api created.';
    END
ELSE
    BEGIN
        PRINT 'ℹ️ Login api already exists.';
    END
GO

USE CustomersDb;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'api')
    BEGIN
        CREATE USER api FOR LOGIN api;
        ALTER ROLE db_owner ADD MEMBER api;
        PRINT '✅ User api created and added to db_owner in CustomersDb.';
    END
ELSE
    BEGIN
        PRINT 'ℹ️ User api already exists in CustomersDb.';
    END
GO