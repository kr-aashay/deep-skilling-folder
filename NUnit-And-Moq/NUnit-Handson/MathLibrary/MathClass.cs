namespace MathLibrary;

// MathLibrary: Stateful math class
// Stores result of each operation in 'result' variable
// GetResult property exposes the stored result
// AllClear resets result to 0 (void method — tested via side effect)
public class MathClass
{
    private double _result = 0;

    // Property to access the stored result
    public double GetResult => _result;

    public double Add(double a, double b)
    {
        _result = a + b;
        return _result;
    }

    public double Subtract(double a, double b)
    {
        _result = a - b;
        return _result;
    }

    public double Multiply(double a, double b)
    {
        _result = a * b;
        return _result;
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("Division by zero is not allowed.");
        _result = a / b;
        return _result;
    }

    // Void method — resets stored result to 0
    public void AllClear()
    {
        _result = 0;
    }
}
