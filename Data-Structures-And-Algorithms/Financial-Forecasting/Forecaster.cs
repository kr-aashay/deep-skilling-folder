namespace FinancialForecasting;

public class Forecaster
{
    // Simple average of all values
    public static double Average(double[] data)
    {
        double sum = 0;
        foreach (var value in data)
            sum += value;
        return sum / data.Length;
    }

    // Moving average of last N months
    public static double MovingAverage(double[] data, int n)
    {
        double sum = 0;
        for (int i = data.Length - n; i < data.Length; i++)
            sum += data[i];
        return sum / n;
    }

    // Linear trend forecast for next period
    public static double LinearTrendForecast(double[] data)
    {
        int n = data.Length;
        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;

        for (int i = 0; i < n; i++)
        {
            sumX  += i + 1;
            sumY  += data[i];
            sumXY += (i + 1) * data[i];
            sumX2 += (i + 1) * (i + 1);
        }

        double slope     = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
        double intercept = (sumY - slope * sumX) / n;

        return slope * (n + 1) + intercept;
    }
}
