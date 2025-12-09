# PROG2111 Project
Design, implement, and programmatically interact with a relational database related to video game e-commerce.

## Instructions to Run C# Application
### Prerequisites:
- MySQL installed with a root user with all permissions on port 3306
- Visual Studio 2022

### Instructions:
1. Ensure the "games" database is installed. Run `game_database.sql` in MySQL Workbench. As an optional step, run `game_database_sample_data.sql` for sample inserted data to be preloaded in the database.
2. Edit the password for the MySQl root user on your machine in `App.Config`, located in the **GameStoreManagementSystem** solution folder.
3. Build the solution and either run it through Visual Studio 2022, or run `GameStoreManagementSystem.exe` under **GameStoreManagementSystem\bin\Debug\net8.0-windows**.
    - If there is an error, make sure that the password is correct and the connection string in `DatabaseConnection.cs` is correct.
4. Select a table to perform CRUD operations, and then select the operation desired. Data may either be edited in the DataGrid or in the form.
5. To save changes to the database, click the "Save" button.

## Project Phases (20%)
### Phase 1: Project Idea & Use Case (5 Marks)
> DUE: Nov 30 2025 @ 8:00pm

[x] Choose a real-world scenario where a relational database is essential (e.g., a library management system, hospital management system, e-commerce site).
<br>[x] Create a GitHub repository and add all members to it
<br>[x] Make sure all members have access before you submit Phase 1 document
<br>[x] Follow the date and timeline for submission on eConestoga

### Phase 2: Data Modeling & ER Diagram (20 Marks)
> DUE: Nov 30 2025 @ 8:00pm

[x] Identify the key entities, attributes, and relationships in your use case.
<br>[x] Develop an Entity-Relationship Diagram (ERD) using appropriate diagramming tools.
<br>[x] The ERD must include all entities, their attributes, primary keys, and relationships between them.

**Deliverable**: ERD in PDF format.

### Phase 3: Normalization (15 Marks)
> DUE: Dec 3 2025 @ 8:00pm

[x] Normalize your database to 3rd Normal Form (3NF).
<br>[x] Submit documentation showing the process of normalizing the database from 1NF to 3NF.
<br>[ ] Clearly explain how you handled redundant data and ensured that data dependencies are logically organized.

**Deliverable**: Normalization report.

### Phase 4: DDL Statements (20 Marks)
> DUE: Dec 3 2025 @ 8:00pm

[x] Based on the ERD, write SQL DDL statements to create the database schema.
<br>[x] Ensure that all necessary primary keys, foreign keys, and integrity constraints are included.

**Deliverable**: SQL script with DDL statements to create tables.

### Phase 5: Programmatic Access & CRUD Operations (30 Marks)
> DUE: Dec 10 2025 @ 8:00**am**

[x] Develop C# programs that connect to the MySQL database using the ADO.NET
<br>[ ] Perform CRUD operations (Create, Read, Update, Delete) for the entities defined in your ERD.
<br>[ ] Ensure proper error handling and transaction control.
<br>[x] Use the ADO.NET MySQL library to establish the connection and perform operations.

**Deliverables**: C# programs with CRUD functionality.

### Phase 6: Final Presentation & Code Walkthrough (10 Marks)
> DUE: Dec 10 2025

Present your database project to the class. The presentation should:
1. Explain the project use case.
2. Walk through the ER diagram.
3. Demonstrate CRUD operations using the C# programs.
4. Discuss any challenges and how your team overcame them.

## Project Guidelines:
**Teamwork**: Work as a cohesive team of three members. Each member should have a defined role, such as data modeling, coding, database design, etc.

**Documentation**: Keep a log of each team member’s contributions, and submit it with the final deliverables.

**Programming Language**: All programmatic access should be done using C#, and SQL queries should be executed through the MySQL API.

**Database**: Use MySQL as the relational database system.

## Assessment Criteria:
- Clarity of the use case: 5 Marks
- Quality of the ERD and normalization: 35 Marks
- Correctness of the DDL statements: 20 Marks
- Functionality and completeness of the C# programs: 30 Marks
- Presentation quality and project explanation: 10 Marks

## Submission Format:
- Upload all deliverables (ERD, DDL, C# programs, normalization report) in a zip file.
- Include the name of each team member and their contribution in the project log
