using CS_DHT;

var benchmarks = Benchmarks.BuildNetworks(10_000);
Console.WriteLine("Сети построены");

benchmarks.RunBenchmarks(100_000);
Console.Write("\nНажмите любую кнопку");
Console.ReadKey();