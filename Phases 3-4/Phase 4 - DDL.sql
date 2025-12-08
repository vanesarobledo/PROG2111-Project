/*
* FILE          : Phase 4 - DDL.sql
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-01
* DESCRIPTION   : This contains the DDL statements to create the necessary tables for our database for video game e-commerce, and defines the
* 				  necessary primary keys, foreign keys, and other constraints on data.
*/

-- Create and use database
CREATE DATABASE games;
USE games;

-- Create tables & define primary keys
CREATE TABLE Game (
	game_id INTEGER AUTO_INCREMENT,
    title VARCHAR(100) NOT NULL,
    release_date DATE,
    genre_id INTEGER,
	developer_id INTEGER,
    console_id INTEGER NOT NULL,
    PRIMARY KEY(game_id)
);

CREATE TABLE Genre (
	genre_id INTEGER AUTO_INCREMENT,
    genre_name VARCHAR(50) NOT NULL,
    UNIQUE(genre_name),
    PRIMARY KEY(genre_id)
);

CREATE TABLE Developer (
	developer_id INTEGER AUTO_INCREMENT,
    developer_name VARCHAR(50) NOT NULL,
    UNIQUE(developer_name),
    PRIMARY KEY(developer_id)
);

CREATE TABLE Console (
	console_id INTEGER AUTO_INCREMENT,
    console_name VARCHAR(50) NOT NULL,
    company_id INTEGER,
    UNIQUE (console_name),
    PRIMARY KEY(console_id)
);

CREATE TABLE Company (
	company_id INTEGER AUTO_INCREMENT,
    company_name VARCHAR(50) NOT NULL,
    UNIQUE(company_name),
    PRIMARY KEY(company_id)
);

CREATE TABLE Inventory (
	inventory_id INTEGER AUTO_INCREMENT,
    game_id INTEGER,
    console_id INTEGER,
    quantity INTEGER,
    store_id INTEGER,
    CHECK (quantity >= 0),
    PRIMARY KEY(inventory_id)
);

CREATE TABLE Product (
	product_id INTEGER AUTO_INCREMENT,
    inventory_id INTEGER NOT NULL,
    cost FLOAT NOT NULL,
    CHECK (cost > 0),
    PRIMARY KEY(product_id)
);

CREATE TABLE Payment (
	payment_id INTEGER AUTO_INCREMENT,
    total_amount FLOAT NOT NULL,
    date_of_purchase DATE NOT NULL,
    customer_id INTEGER NOT NULL,
    store_id INTEGER NOT NULL,
    PRIMARY KEY(payment_id)
);

CREATE TABLE Payment_Line (
	payment_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL
);

CREATE TABLE Store (
	store_id INTEGER AUTO_INCREMENT,
    location VARCHAR(50) NOT NULL,
    PRIMARY KEY(store_id)
);

CREATE TABLE Employee (
	employee_id INTEGER AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    email VARCHAR(50) NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    store_id INTEGER NOT NULL,
    PRIMARY KEY(employee_id)
);

CREATE TABLE Customer (
	customer_id INTEGER AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    date_of_birth DATE,
    email VARCHAR(50),
    username VARCHAR(50),
    password VARCHAR(50),
    PRIMARY KEY(customer_id)
);

-- Add foreign keys
ALTER TABLE Game
ADD CONSTRAINT FOREIGN KEY (genre_id) REFERENCES Genre (genre_id);
ALTER TABLE Game
ADD CONSTRAINT FOREIGN KEY (developer_id) REFERENCES Developer (developer_id);
ALTER TABLE Game
ADD CONSTRAINT FOREIGN KEY (console_id) REFERENCES Console (console_id);

ALTER TABLE Console
ADD CONSTRAINT FOREIGN KEY (company_id) REFERENCES Company (company_id);

ALTER TABLE Inventory
ADD CONSTRAINT FOREIGN KEY (game_id) REFERENCES Game (game_id);
ALTER TABLE Inventory
ADD CONSTRAINT FOREIGN KEY (console_id) REFERENCES Console (console_id);
ALTER TABLE Inventory
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);

ALTER TABLE Product
ADD CONSTRAINT FOREIGN KEY (inventory_id) REFERENCES Inventory (inventory_id);

ALTER TABLE Payment
ADD CONSTRAINT FOREIGN KEY (customer_id) REFERENCES Customer (customer_id);
ALTER TABLE Payment
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);

ALTER TABLE Payment_Line
ADD CONSTRAINT FOREIGN KEY (payment_id) REFERENCES Payment (payment_id);
ALTER TABLE Payment_Line
ADD CONSTRAINT FOREIGN KEY (product_id) REFERENCES Product (product_id);

ALTER TABLE Employee
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);