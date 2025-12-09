/*
* FILE          : game_database.sql
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-01
* DESCRIPTION   : This contains the DDL statements to create the necessary tables for our database for video game e-commerce, and defines the
* 				  necessary primary keys, foreign keys, and other constraints on data.
* CHANGELOG		: 2025-12-07: Reduced the number of entities
*/

INSERT INTO GAME VALUES (1,"GAMER Nation", "2025-01-12","Car Game","JUST THE WORST",1);


-- Create and use database
CREATE DATABASE games;
USE games;

-- Create tables & define primary keys
CREATE TABLE Game (
	game_id INTEGER AUTO_INCREMENT,
    title VARCHAR(100) NOT NULL,
    release_date DATE,
    genre VARCHAR(50),
    developer VARCHAR(50),
    console_id INTEGER NOT NULL,
    PRIMARY KEY(game_id)
);

CREATE TABLE Console (
	console_id INTEGER AUTO_INCREMENT,
    console_name VARCHAR(50) NOT NULL,
    company VARCHAR(50),
    UNIQUE (console_name),
    PRIMARY KEY(console_id)
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
    customer_id INTEGER NOT NULL,
    cost FLOAT NOT NULL,
    date_of_purchase DATE NOT NULL,
    quantity INTEGER NOT NULL,
    store_id INTEGER NOT NULL,
    CHECK (cost > 0),
    CHECK (quantity > 0),
    PRIMARY KEY(product_id)
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
ADD CONSTRAINT FOREIGN KEY (console_id) REFERENCES Console (console_id);

ALTER TABLE Inventory
ADD CONSTRAINT FOREIGN KEY (game_id) REFERENCES Game (game_id);
ALTER TABLE Inventory
ADD CONSTRAINT FOREIGN KEY (console_id) REFERENCES Console (console_id);
ALTER TABLE Inventory
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);

ALTER TABLE Product
ADD CONSTRAINT FOREIGN KEY (inventory_id) REFERENCES Inventory (inventory_id);
ALTER TABLE Product
ADD CONSTRAINT FOREIGN KEY (customer_id) REFERENCES Customer (customer_id);
ALTER TABLE Product
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);

ALTER TABLE Employee
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);