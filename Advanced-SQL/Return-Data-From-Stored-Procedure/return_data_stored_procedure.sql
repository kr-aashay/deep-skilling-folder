-- ============================================
-- Returning Data from Stored Procedures
-- ============================================

-- Using same Products table from Create-Stored-Procedure

-- ============================================
-- 1. Return data using SELECT
-- ============================================
CREATE PROCEDURE GetProductByID
    @ProductID INT
AS
BEGIN
    SELECT * FROM Products
    WHERE ProductID = @ProductID;
END;

EXEC GetProductByID @ProductID = 2;

-- ============================================
-- 2. Return scalar value using OUTPUT parameter
-- ============================================
CREATE PROCEDURE GetProductPrice
    @ProductID  INT,
    @Price      DECIMAL(10, 2) OUTPUT
AS
BEGIN
    SELECT @Price = Price
    FROM Products
    WHERE ProductID = @ProductID;
END;

-- Call with OUTPUT parameter
DECLARE @ProductPrice DECIMAL(10, 2);
EXEC GetProductPrice @ProductID = 1, @Price = @ProductPrice OUTPUT;
PRINT 'Product Price: ' + CAST(@ProductPrice AS VARCHAR);

-- ============================================
-- 3. Return status using RETURN value
-- ============================================
CREATE PROCEDURE CheckStock
    @ProductID INT
AS
BEGIN
    DECLARE @Stock INT;

    SELECT @Stock = Stock
    FROM Products
    WHERE ProductID = @ProductID;

    IF @Stock > 0
        RETURN 1;  -- In stock
    ELSE
        RETURN 0;  -- Out of stock
END;

-- Call and check return value
DECLARE @Status INT;
EXEC @Status = CheckStock @ProductID = 1;

IF @Status = 1
    PRINT 'Product is in stock.';
ELSE
    PRINT 'Product is out of stock.';
