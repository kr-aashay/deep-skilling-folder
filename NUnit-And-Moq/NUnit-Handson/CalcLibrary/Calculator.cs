namespace CalcLibrary;

// CalcLibrary: Simple stateless calculator
// Each method returns the result of the operation
// Division throws ArgumentException when divisor is 0
public class Calculator
{
    public int Add(int a, int b)      => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;

    public double Divide(int a, int b)
    {
        if (b == 0)
            throw new ArgumentException("Division by zero is not allowed.");
        return (double)a / b;
    }
}
