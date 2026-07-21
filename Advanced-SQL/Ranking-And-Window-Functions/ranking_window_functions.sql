-- ============================================
-- Ranking and Window Functions in SQL
-- ============================================

-- Sample table
CREATE TABLE Employees (
    EmployeeID   INT,
    Name         VARCHAR(50),
    Department   VARCHAR(50),
    Salary       DECIMAL(10, 2)
);

INSERT INTO Employees VALUES (1, 'Alice',   'HR',          60000);
INSERT INTO Employees VALUES (2, 'Bob',     'IT',          80000);
INSERT INTO Employees VALUES (3, 'Charlie', 'IT',          75000);
INSERT INTO Employees VALUES (4, 'Diana',   'HR',          62000);
INSERT INTO Employees VALUES (5, 'Eve',     'Finance',     90000);
INSERT INTO Employees VALUES (6, 'Frank',   'Finance',     90000);
INSERT INTO Employees VALUES (7, 'Grace',   'IT',          80000);

-- ============================================
-- 1. ROW_NUMBER — unique rank per partition
-- ============================================
SELECT
    Name,
    Department,
    Salary,
    ROW_NUMBER() OVER (PARTITION BY Department ORDER BY Salary DESC) AS RowNum
FROM Employees;

-- ============================================
-- 2. RANK — same rank for ties, gaps after
-- ============================================
SELECT
    Name,
    Department,
    Salary,
    RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS Rank
FROM Employees;

-- ============================================
-- 3. DENSE_RANK — same rank for ties, no gaps
-- ============================================
SELECT
    Name,
    Department,
    Salary,
    DENSE_RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS DenseRank
FROM Employees;

-- ============================================
-- 4. SUM — running total per department
-- ============================================
SELECT
    Name,
    Department,
    Salary,
    SUM(Salary) OVER (PARTITION BY Department ORDER BY Salary) AS RunningTotal
FROM Employees;

-- ============================================
-- 5. AVG — average salary per department
-- ============================================
SELECT
    Name,
    Department,
    Salary,
    AVG(Salary) OVER (PARTITION BY Department) AS AvgDeptSalary
FROM Employees;
