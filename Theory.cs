namespace CS_DHT;

public static class Theory
{
	private const char ESC = (char)27;

	private static void PrintInfo()
	{
		Console.WriteLine("Теоретический материал по DHT. Нажмите...");
		Console.WriteLine("1 -- Хэширование");
		Console.WriteLine("2 -- DHT");
		Console.WriteLine("3 -- Chord");
		Console.WriteLine("4 -- CAN");
		Console.WriteLine("5 -- Pastry");
		Console.WriteLine("6 -- Tapestry");
	}

	public static void Show()
	{

		for (; ; ) {
			Console.Clear();
			Theory.PrintInfo();
			char ch = Console.ReadKey(true).KeyChar;
			Console.Clear();
			switch (ch) {
			case ESC:
				return;
			case '1':
				Theory.ShowHashing();
				break;
			case '2':
				Theory.ShowDHT();
				break;
			case '3':
				Theory.ShowChord();
				break;
			case '4':
				Theory.ShowCAN();
				break;
			case '5':
				Theory.ShowPastry();
				break;
			case '6':
				Theory.ShowTapestry();
				break;
			default:
				continue;
			}
			Console.Write("Нажмите любую кнопку, чтобы продолжить...");
			Console.ReadKey(true);
		}
	}

	private static void ShowHashing()
	{
		string[] text = {
			"Хэширование — процесс, преобразовающий входные данные " +
			"в соответствии с некоторым алгоритмом. " +
			"Занимаются же хэшированием так называемые хэш-функции." +
			" В контексте структур данных идёт речь о хэш-таблицах, " +
			"где хэш-функция используется для преобразования ключа в индекс, " +
			"используемый для доступа к искомомым данным. ",

			"От выбора хэш‑функции зависит эффективность. Она должна как можно" +
			" равномернее распределять ключи по диапазону индексов. " +
			"Также ей желательно быть как можно менее предсказуемой " +
			"для стороннего наблюдателя, не знакомого с её реализацией.",

			"Другой распространённой проблемой хэширования являются коллизии " +
			"— ситуации, когда от хэш‑функция производит одинаковые значения от двух разных ключей. " +
			"Есть два основных пути решения проблемы коллизии: метод цепочек и открытая адресация"
		};
		Theory.PrintText(text);
	}

	private static void ShowDHT()
	{
		throw new NotImplementedException();
	}

	private static void ShowCAN()
	{
		throw new NotImplementedException();
	}

	private static void ShowChord()
	{
		throw new NotImplementedException();
	}

	private static void ShowPastry()
	{
		throw new NotImplementedException();
	}

	private static void ShowTapestry()
	{
		throw new NotImplementedException();
	}

	private static void PrintText(string[] text)
	{
		foreach (string s in text) {
			Console.WriteLine(s);
			var ch = Console.ReadKey(true).KeyChar;
			if (ch == ESC) {
				break;
			}
		}
	}
}
