using FinancialForecasting;

double[] monthlySales = { 1200, 1500, 1300, 1700, 1600, 1800, 2000, 1900, 2100, 2200, 2400, 2300 };

Console.WriteLine("=== Monthly Sales Data ===");
for (int i = 0; i < monthlySales.Length; i++)
    Console.WriteLine($"Month {i + 1}: ${monthlySales[i]}");

Console.WriteLine("\n=== Forecasting Results ===");
Console.WriteLine($"Average Sales        : ${Forecaster.Average(monthlySales):F2}");
Console.WriteLine($"Moving Average (3mo) : ${Forecaster.MovingAverage(monthlySales, 3):F2}");
Console.WriteLine($"Next Month Forecast  : ${Forecaster.LinearTrendForecast(monthlySales):F2}");
