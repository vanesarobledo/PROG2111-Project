USE games;

-- Genre
INSERT INTO Genre (genre_name) VALUES ("Action");
INSERT INTO Genre (genre_name) VALUES ("Adventure");
INSERT INTO Genre (genre_name) VALUES ("Fighting");
INSERT INTO Genre (genre_name) VALUES ("Horror");
INSERT INTO Genre (genre_name) VALUES ("Role-playing");
INSERT INTO Genre (genre_name) VALUES ("Puzzle");
INSERT INTO Genre (genre_name) VALUES ("Sandbox");
INSERT INTO Genre (genre_name) VALUES ("Simulation");
INSERT INTO Genre (genre_name) VALUES ("Strategy");

-- Company
INSERT INTO Company (company_name) VALUES ("Mintendo");
INSERT INTO Company (company_name) VALUES ("Nicrosoft");
INSERT INTO Company (company_name) VALUES ("Tony");
INSERT INTO Company (company_name) VALUES ("Spigot");

-- Developer
INSERT INTO Developer (developer_name) VALUES ("Bigthesda");
INSERT INTO Developer (developer_name) VALUES ("Game Weirdo");
INSERT INTO Developer (developer_name) VALUES ("Quadarc");
INSERT INTO Developer (developer_name) VALUES ("Wire");
INSERT INTO Developer (developer_name) VALUES ("Stonestar Games");

-- Console
INSERT INTO Console (console_name, company_id) VALUES ("Mintendo Swatch", 1);
INSERT INTO Console (console_name, company_id) VALUES ("Y Cube Model S", 2);
INSERT INTO Console (console_name, company_id) VALUES ("Joystation 5", 3);
INSERT INTO Console (console_name, company_id) VALUES ("Vapour Machine", 4);

-- Games
INSERT INTO Game (title, release_date, genre_id, developer_id, console_id)
VALUES ('Petit Larceny 5', '2013-09-17', 7, 5, 3);

-- Store
INSERT INTO Store (location) VALUES ("Waterloo");
INSERT INTO Store (location) VALUES ("Kitchener");

-- Inventory
INSERT INTO Inventory (game_id, console_id, quantity, store_id)
VALUES (1, null, 10, 1);

-- Product
INSERT INTO Product (inventory_id, cost)
VALUES (1, 19.99);

-- Customer
INSERT INTO Customer (first_name, last_name, date_of_birth, email, username, password)
VALUES ('Chiaki', 'Nanami', '1996-03-14', 'cnanami@yahoo.co.jo', 'cnanami', 'vYjIG40M%f');

-- Payment
INSERT INTO Payment (total_amount, date_of_purchase, customer_id, store_id)
VALUES ('19.99', '2025-12-01', 1, 1);

-- Payment_Line
INSERT INTO Payment_Line (payment_id, product_id, quantity)
VALUES (1, 1, 1);

-- Employee
INSERT INTO Employee (first_name, last_name, date_of_birth, email, username, password, store_id)
VALUES ('Ethan', 'Mars', '1978-09-14', 'ethanmars@mail.com', 'emars', 'pexx$6UP&U', 1);