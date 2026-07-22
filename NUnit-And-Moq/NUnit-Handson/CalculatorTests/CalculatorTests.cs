using CalcLibrary;
using MathLibrary;
using NUnit.Framework;

namespace CalculatorTests;

// ============================================================
// NUnit Hands-On: Full test suite for Calculator and MathClass
//
// Concepts covered:
//   TestFixture   — marks class as test container
//   SetUp         — runs before each test (initialise objects)
//   TearDown      — runs after each test (cleanup)
//   Test          — marks a method as a test
//   TestCase      — parameterised test with inline data
//   Assert.That   — modern NUnit assertion
//   Assert.AreEqual — classic equality assertion
//   Assert.Fail   — explicitly fail a test with a message
//   Try/Catch     — catch exceptions thrown by code under test
//   Ignore        — skip a test with a reason
// ============================================================

[TestFixture]
public class CalculatorTests
{
    // Class under test — stateless calculator
    private Calculator _calc = null!;

    // Stateful math class with GetResult and AllClear
    private MathClass _math = null!;

    // ── SetUp: runs before EACH test ───────────────────────────────────────────
    // Use to initialise objects so each test starts with a clean state
    [SetUp]
    public void SetUp()
    {
        _calc = new Calculator();
        _math = new MathClass();
        Console.WriteLine("[SetUp] Calculator and MathClass initialised.");
    }

    // ── TearDown: runs after EACH test ─────────────────────────────────────────
    // Use to release resources, reset state, or log completion
    [TearDown]
    public void TearDown()
    {
        _math.AllClear();
        Console.WriteLine("[TearDown] MathClass cleared.");
    }

    // ==========================================================================
    // SECTION 1: Addition — TestFixture, Test, TestCase, Assert.That
    // ==========================================================================

    // Single test with no parameters
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
    {
        var result = _calc.Add(3, 5);
        Assert.That(result, Is.EqualTo(8));
    }

    // TestCase: parameterised — multiple inputs tested in one method
    // Benefit: avoids duplicating test code for every combination
    [TestCase(10, 5,  15)]
    [TestCase(0,  0,  0)]
    [TestCase(-3, 3,  0)]
    [TestCase(-5, -5, -10)]
    [TestCase(100, 200, 300)]
    public void Add_VariousInputs_ReturnsExpectedResult(int a, int b, int expected)
    {
        var result = _calc.Add(a, b);
        Assert.That(result, Is.EqualTo(expected));
    }

    // ==========================================================================
    // SECTION 2: Subtraction — multiple TestCase combinations
    // ==========================================================================

    [TestCase(10,  3, 7)]
    [TestCase(0,   0, 0)]
    [TestCase(-5,  5, -10)]
    [TestCase(100, 50, 50)]
    public void Subtract_VariousInputs_ReturnsExpectedResult(int a, int b, int expected)
    {
        var result = _calc.Subtract(a, b);
        // Assert.That with Is.EqualTo — NUnit 4 modern assertion
        Assert.That(result, Is.EqualTo(expected));
    }

    // ==========================================================================
    // SECTION 3: Multiplication — multiple TestCase combinations
    // ==========================================================================

    [TestCase(3,  4,  12)]
    [TestCase(0,  100, 0)]
    [TestCase(-2, 5,  -10)]
    [TestCase(-3, -3, 9)]
    [TestCase(7,  8,  56)]
    public void Multiply_VariousInputs_ReturnsExpectedResult(int a, int b, int expected)
    {
        var result = _calc.Multiply(a, b);
        Assert.That(result, Is.EqualTo(expected));
    }

    // ==========================================================================
    // SECTION 4: Division — TestCase, try/catch, Assert.Fail, ArgumentException
    // ==========================================================================

    [TestCase(10,  2,  5.0)]
    [TestCase(9,   3,  3.0)]
    [TestCase(100, 4,  25.0)]
    [TestCase(-10, 2,  -5.0)]
    public void Divide_ValidInputs_ReturnsExpectedResult(int a, int b, double expected)
    {
        var result = _calc.Divide(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(0.0001));
    }

    // Division by zero — use try/catch to catch ArgumentException
    // Assert.Fail explicitly fails the test if no exception is thrown
    [TestCase(10, 0)]
    [TestCase(-5, 0)]
    public void Divide_ByZero_ThrowsArgumentException(int a, int b)
    {
        try
        {
            _calc.Divide(a, b);

            // If we reach this line, no exception was thrown — test must fail
            Assert.Fail("Division by zero — expected ArgumentException was not thrown.");
        }
        catch (ArgumentException ex)
        {
            // Exception was thrown as expected — assert the message
            Assert.That(ex.Message, Does.Contain("Division by zero"));
        }
    }

    // ==========================================================================
    // SECTION 5: Test void method — AllClear resets result to 0
    // ==========================================================================

    // Testing a void method by checking its side effect on state
    [Test]
    public void TestAddAndClear()
    {
        // Step 1: Invoke Addition and verify result
        var addResult = _math.Add(15, 25);
        Assert.That(addResult, Is.EqualTo(40), "Add result should be 40");
        Assert.That(_math.GetResult, Is.EqualTo(40), "GetResult should return 40 after Add");

        // Step 2: Invoke AllClear (void method)
        _math.AllClear();

        // Step 3: Verify GetResult is now 0
        Assert.That(_math.GetResult, Is.EqualTo(0), "GetResult should be 0 after AllClear");
    }

    [Test]
    public void TestSubtractAndClear()
    {
        _math.Subtract(50, 20);
        Assert.That(_math.GetResult, Is.EqualTo(30));

        _math.AllClear();
        Assert.That(_math.GetResult, Is.EqualTo(0));
    }

    // ==========================================================================
    // SECTION 6: MathClass — stateful operations via GetResult
    // ==========================================================================

    [TestCase(6.0,  2.0, 3.0)]
    [TestCase(9.0,  3.0, 3.0)]
    [TestCase(10.0, 4.0, 2.5)]
    public void MathClass_Divide_StoresResultInGetResult(double a, double b, double expected)
    {
        _math.Divide(a, b);
        Assert.That(_math.GetResult, Is.EqualTo(expected).Within(0.0001));
    }

    [Test]
    public void MathClass_DivideByZero_ThrowsArgumentException()
    {
        try
        {
            _math.Divide(10, 0);
            Assert.Fail("Division by zero — expected ArgumentException was not thrown.");
        }
        catch (ArgumentException ex)
        {
            Assert.That(ex.Message, Does.Contain("Division by zero"));
        }
    }

    // ==========================================================================
    // SECTION 7: Ignored test — demonstrates [Ignore] attribute
    // ==========================================================================

    // [Ignore] skips this test with a reason shown in Test Explorer
    // Use when a feature is not yet implemented or test is temporarily disabled
    [Test]
    [Ignore("Square root not yet implemented in Calculator")]
    public void Sqrt_PositiveNumber_ReturnsCorrectResult()
    {
        // Placeholder — implement when Sqrt is added to Calculator
        Assert.Fail("Not implemented yet.");
    }
}
