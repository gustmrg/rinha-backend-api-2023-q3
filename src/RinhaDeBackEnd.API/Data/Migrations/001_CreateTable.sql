CREATE DATABASE rinha;

CREATE TABLE IF NOT EXISTS persons (
    person_id UUID PRIMARY KEY,
    nickname VARCHAR(32) UNIQUE NOT NULL,
    name VARCHAR(100) NOT NULL,
    date_of_birth DATE NOT NULL
);