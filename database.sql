-- database.sql
-- Esquema de referencia si la demo se quisiera migrar a MySQL.
-- La aplicación incluida usa memoria para poder ejecutarse rápido en VS Code.

CREATE TABLE Users (
    UserID CHAR(36) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL DEFAULT 'user'
);
