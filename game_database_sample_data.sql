/*
* FILE          : game_database_sample_data.sql
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This contains sample data for use in the game database.
*/

USE games;

-- Console Data
INSERT INTO Console (console_name, company) VALUES ("Mintendo Swatch", "Mintendo");
INSERT INTO Console (console_name, company) VALUES ("Y Cube", "Nicrosoft");
INSERT INTO Console (console_name, company) VALUES ("Joystation", "Stony");
INSERT INTO Console (console_name, company) VALUES ("Vapour Machine", "Spigot");

-- Game Data
INSERT INTO Game (title, release_date, genre, developer, console_id)
VALUES ('Petit Larceny 5', '2013-09-17', 'Sandbox', 'Stonestar Games', 2);
INSERT INTO Game (title, release_date, genre, developer, console_id)
VALUES ('Heart Rain Ravine', '2016-02-26', 'Simulation', 'AnxiousMonkey', 1);
INSERT INTO Game (title, release_date, genre, developer, console_id)
VALUES ('Odin\'s Fortress', '2023-08-03', 'RPG', 'Lar Studi', 4);
INSERT INTO Game (title, release_date, genre, developer, console_id)
VALUES ('Older Parchments 5', '2011-11-11', 'Action RPG', 'Bigthesda', 3);
INSERT INTO Game (title, release_date, genre, developer, console_id)
VALUES ('Like a Serpent: Unlimited Affluence', '2024-01-25', 'RPG', 'GRR Studio', 3);

-- Store Data
INSERT INTO Store (location) VALUES ("Waterloo");
INSERT INTO Store (location) VALUES ("Kitchener");

-- Inventory Data
-- Games
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (1, null, 25, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (2, null, 20, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (3, null, 31, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (4, null, 21, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (5, null, 21, store_id);
-- Consoles
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (null, 1, 3, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (null, 2, 15, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (null, 3, 5, store_id);
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (null, 4, 9, store_id);

-- Employee Data
INSERT INTO Employee (first_name, last_name, date_of_birth, email, username, password, store_id)
VALUES ('Daigo', 'Dojima', '1976-03-26', 'tojos6thchairman@mail.com', 'ddojima', 'pexx$6UP&U', 1);

INSERT INTO Employee (first_name, last_name, date_of_birth, email, username, password, store_id)
VALUES ('Ethan', 'Mars', '1978-09-14', 'ethanmars@mail.com', 'emars', '3um^mlQS@!', 2);

-- Customer Data
INSERT INTO Customer (first_name, last_name, date_of_birth, email, username, password)
VALUES ('Terry', 'Bogard', '1971-03-15', 'legendaryhungrywolf@gmail.com', 'tbogard', 'n0fWLIk#gg');

INSERT INTO Customer (first_name, last_name, date_of_birth, email, username, password)
VALUES ('Chiaki', 'Nanami', '1996-03-14', 'chiaki@yahoo.co.jp', 'cnanami', 'vYjIG40M%f');

INSERT INTO Customer (first_name, last_name, date_of_birth, email, username, password)
VALUES ('Futaba', 'Sakura', '2001-02-19', 'fsakura@gmail.com', 'fsakura', 'EOepU0%S$Y');

INSERT INTO Customer (first_name, last_name, date_of_birth, email, username, password)
VALUES ('Ichiban', 'Kasuga', '1977-01-01', 'dragonfish@mail.com', 'ikasuga', '*3VvLU#Apr');

INSERT INTO Customer (first_name, last_name, date_of_birth, email, username, password)
VALUES ('Joseph', 'Joestar', '1920-09-27', 'hermitpurple@mail.com', 'jjoestar', '%XJuCn7Yt^');

-- Product Data
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (1, 1, 49.99, '2025-12-01', 1, 1);
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (3, 1, 49.99, '2025-12-02', 1, 2);
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (4, 3, 49.99, '2025-12-02', 2, 1);
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (1, 4, 49.99, '2025-12-03', 1, 1);
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (7, 5, 49.99, '2025-12-03', 1, 2);
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (5, 3, 49.99, '2025-12-04', 1, 2);
INSERT INTO Product (inventory_id, customer_id, cost, date_of_purchase, quantity, store_id)
VALUES (2, 2, 49.99, '2025-12-05', 1, 1);
