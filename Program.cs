using CS_DHT;

static void benchmarks()
{
	var benchmarks = Benchmarks.BuildNetworks(10_000);
	Console.WriteLine("Сети построены");

	benchmarks.RunBenchmarks(100_000);
	Console.Write("\nНажмите любую кнопку");
	Console.ReadKey(true);
}

static void printInfo()
{
	Console.WriteLine("Программа-демонстрация алгоритов распределённого хэширования");
	Console.WriteLine("Нажмите 1 для вывода теории");
	Console.WriteLine("Нажмите 2 для прохождения тестов");
	Console.WriteLine("Нажмите 3 для запуска бенчмарков");
}

static void main()
{
	const char ESC = (char)27;

	for (; ; ) {
		Console.Clear();
		printInfo();
		var ch = Console.ReadKey(true).KeyChar;
		Console.Clear();
		switch (ch) {
		case ESC:
			return;
		case '1':
			Theory.Show();
			break;
		case '2':
			new Tests(10).RunTests();
			break;
		case '3':
			benchmarks();
			break;
		default:
			continue;
		}
	}
}

main();