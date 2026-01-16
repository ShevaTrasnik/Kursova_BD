
DROP DATABASE IF EXISTS bakery_db_clean;
CREATE DATABASE bakery_db_clean
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE bakery_db_clean;


CREATE TABLE categories (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT
);

CREATE TABLE suppliers (
    supplier_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(100),
    address TEXT,
    contract_date DATE
);

CREATE TABLE equipment (
    equipment_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    type VARCHAR(50),
    status VARCHAR(20),
    last_service_date DATE
);

CREATE TABLE employees (
    employee_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    position VARCHAR(50),
    phone VARCHAR(20),
    hire_date DATE,
    fired_date DATE
);
CREATE TABLE bakery_products (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    category_id INT NOT NULL,
    name VARCHAR(100) NOT NULL,
    weight DECIMAL(10,2),
    unit VARCHAR(20),
    description TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
CREATE TABLE ingredients (
    ingredient_id INT AUTO_INCREMENT PRIMARY KEY,
    supplier_id INT,
    name VARCHAR(100) NOT NULL,
    unit VARCHAR(20),
    stock_qty DECIMAL(10,2),
    shelf_life DATE,
    FOREIGN KEY (supplier_id) REFERENCES suppliers(supplier_id)
);
CREATE TABLE recipes (
    recipe_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    ingredient_id INT NOT NULL,
    quantity DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (product_id) REFERENCES bakery_products(product_id)
        ON DELETE CASCADE,
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(ingredient_id)
        ON DELETE CASCADE
);

CREATE TABLE technology_steps (
    step_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    step_number INT NOT NULL,
    step_name VARCHAR(100) NOT NULL,
    description TEXT,
    duration_min INT,
    FOREIGN KEY (product_id) REFERENCES bakery_products(product_id)
        ON DELETE CASCADE
);

CREATE TABLE production_batches (
    batch_id INT AUTO_INCREMENT PRIMARY KEY,
    employee_id INT NOT NULL,
    date DATE NOT NULL,
    shift ENUM('morning', 'evening', 'night'),
    total_weight DECIMAL(10,2),
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id)
);

CREATE TABLE production_items (
    production_item_id INT AUTO_INCREMENT PRIMARY KEY,
    batch_id INT NOT NULL,
    product_id INT NOT NULL,
    produced_qty DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (batch_id) REFERENCES production_batches(batch_id)
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES bakery_products(product_id)
        ON DELETE CASCADE
);

CREATE TABLE used_equipment (
    used_equipment_id INT AUTO_INCREMENT PRIMARY KEY,
    batch_id INT NOT NULL,
    equipment_id INT NOT NULL,
    start_time DATETIME,
    end_time DATETIME,
    FOREIGN KEY (batch_id) REFERENCES production_batches(batch_id)
        ON DELETE CASCADE,
    FOREIGN KEY (equipment_id) REFERENCES equipment(equipment_id)
        ON DELETE CASCADE
);
