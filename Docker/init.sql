CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    login VARCHAR(20) UNIQUE NOT NULL,
    password VARCHAR(100) NOT NULL,
    rating INT NOT NULL DEFAULT 1000
);