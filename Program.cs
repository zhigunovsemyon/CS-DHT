using CS_DHT;

const char ESC = (char)27;
const int NETWORK_SIZE = 10_000;
const int QUERY_COUNT = 100_000;
const int QUESTION_COUNT = 10;

static void benchmarks()
{
	var benchmarks = Benchmarks.BuildNetworks(NETWORK_SIZE);
	Console.WriteLine("Сети построены");

	benchmarks.RunBenchmarks(QUERY_COUNT);
	Console.Write("\nНажмите любую кнопку");
	Console.ReadKey(true);
}

static void printInfo()
{
	Console.WriteLine("Программа-демонстрация алгоритмов распределённого хэширования");
	Console.WriteLine("Нажмите 1 для вывода теории");
	Console.WriteLine("Нажмите 2 для прохождения тестов");
	Console.WriteLine("Нажмите 3 для запуска бенчмарков");
}

static void main()
{
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
			Tests.GetTests(QUESTION_COUNT).RunTests();
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