-- Create and use database
CREATE DATABASE games;
USE games;

-- Create tables & define primary keys
CREATE TABLE Game (
	game_id INTEGER AUTO_INCREMENT,
    title VARCHAR(100),
    release_date DATE,
    genre_id INTEGER,
	developer_id INTEGER,
    console_id INTEGER,
    PRIMARY KEY(game_id)
);

CREATE TABLE Genre (
	genre_id INTEGER AUTO_INCREMENT,
    genre_name VARCHAR(50),
    UNIQUE(genre_name),
    PRIMARY KEY(genre_id)
);

CREATE TABLE Developer (
	developer_id INTEGER AUTO_INCREMENT,
    developer_name VARCHAR(50),
    UNIQUE(developer_name),
    PRIMARY KEY(developer_id)
);

CREATE TABLE Console (
	console_id INTEGER AUTO_INCREMENT,
    console_name VARCHAR(50),
    company_id INTEGER,
    UNIQUE (console_name),
    PRIMARY KEY(console_id)
);

CREATE TABLE Company (
	company_id INTEGER AUTO_INCREMENT,
    company_name VARCHAR(50),
    UNIQUE(company_name),
    PRIMARY KEY(company_id)
);

CREATE TABLE Inventory (
	inventory_id INTEGER AUTO_INCREMENT,
    game_id INTEGER,
    console_id INTEGER,
    quantity INTEGER,
    store_id INTEGER,
    PRIMARY KEY(inventory_id)
);

CREATE TABLE Product (
	product_id INTEGER AUTO_INCREMENT,
    inventory_id INTEGER,
    cost FLOAT,
    PRIMARY KEY(product_id)
);

CREATE TABLE Payment (
	payment_id INTEGER AUTO_INCREMENT,
    total_amount FLOAT,
    date_of_purchase DATE,
    product_id INTEGER,
    customer_id INTEGER,
    store_id INTEGER,
    PRIMARY KEY(payment_id)
);

CREATE TABLE Store (
	store_id INTEGER AUTO_INCREMENT,
    location VARCHAR(50),
    PRIMARY KEY(store_id)
);

CREATE TABLE Employee (
	employee_id INTEGER AUTO_INCREMENT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth DATE,
    email VARCHAR(50),
    username VARCHAR(50),
    password VARCHAR(50),
    store_id INTEGER,
    PRIMARY KEY(employee_id)
);

CREATE TABLE Customer (
	customer_id INTEGER AUTO_INCREMENT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
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

ALTER TABLE PRODUCT
ADD CONSTRAINT FOREIGN KEY (inventory_id) REFERENCES Inventory (inventory_id);

ALTER TABLE PAYMENT
ADD CONSTRAINT FOREIGN KEY (product_id) REFERENCES Product (product_id);
ALTER TABLE PAYMENT
ADD CONSTRAINT FOREIGN KEY (customer_id) REFERENCES Customer (customer_id);
ALTER TABLE PAYMENT
ADD CONSTRAINT FOREIGN KEY (store_id) REFERENCES Store (store_id);