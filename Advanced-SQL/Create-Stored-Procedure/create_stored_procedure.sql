-- ============================================
-- Creating Stored Procedures in SQL
-- ============================================

-- Sample table
CREATE TABLE Products (
    ProductID   INT,
    Name        VARCHAR(100),
    Category    VARCHAR(50),
    Price       DECIMAL(10, 2),
    Stock       INT
);

INSERT INTO Products VALUES (1, 'Laptop',      'Electronics', 999.99, 50);
INSERT INTO Products VALUES (2, 'Phone',        'Electronics', 499.99, 100);
INSERT INTO Products VALUES (3, 'Desk',         'Furniture',   199.99, 30);
INSERT INTO Products VALUES (4, 'Chair',        'Furniture',   149.99, 60);
INSERT INTO Products VALUES (5, 'Headphones',   'Electronics',  79.99, 80);

-- ============================================
-- 1. Simple stored procedure — get all products
-- ============================================
CREATE PROCEDURE GetAllProducts
AS
BEGIN
    SELECT * FROM Products;
END;

EXEC GetAllProducts;

-- ============================================
-- 2. Stored procedure with input parameter
-- ============================================
CREATE PROCEDURE GetProductsByCategory
    @Category VARCHAR(50)
AS
BEGIN
    SELECT * FROM Products
    WHERE Category = @Category;
END;

EXEC GetProductsByCategory @Category = 'Electronics';

-- ============================================
-- 3. Stored procedure to insert a new product
-- ============================================
CREATE PROCEDURE AddProduct
    @ProductID  INT,
    @Name       VARCHAR(100),
    @Category   VARCHAR(50),
    @Price      DECIMAL(10, 2),
    @Stock      INT
AS
BEGIN
    INSERT INTO Products (ProductID, Name, Category, Price, Stock)
    VALUES (@ProductID, @Name, @Category, @Price, @Stock);

    PRINT 'Product added successfully.';
END;

EXEC AddProduct @ProductID = 6, @Name = 'Monitor', @Category = 'Electronics', @Price = 299.99, @Stock = 40;

-- ============================================
-- 4. Stored procedure to update product price
-- ============================================
CREATE PROCEDURE UpdateProductPrice
    @ProductID  INT,
    @NewPrice   DECIMAL(10, 2)
AS
BEGIN
    UPDATE Products
    SET Price = @NewPrice
    WHERE ProductID = @ProductID;

    PRINT 'Price updated successfully.';
END;

EXEC UpdateProductPrice @ProductID = 1, @NewPrice = 899.99;
