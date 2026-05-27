using CS_DHT;

static void tests()
{
	Console.Clear();
	throw new NotImplementedException("tests()");
}

static void theory()
{
	Console.Clear();
	throw new NotImplementedException("theory()");
}

static void benchmarks()
{
	var benchmarks = Benchmarks.BuildNetworks(10_000);
	Console.WriteLine("Сети построены");

	benchmarks.RunBenchmarks(100_000);
	Console.Write("\nНажмите любую кнопку");
	Console.ReadKey();
	Console.Clear();
}

static void main()
{
	benchmarks();
}

main();