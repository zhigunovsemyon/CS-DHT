namespace CS_DHT;

public static class Theory
{
	private static void PrintInfo()
	{
		Console.WriteLine("Теоретический материал по DHT. Нажмите...");
		Console.WriteLine("1 -- DHT");
		Console.WriteLine("2 -- Chord");
		Console.WriteLine("3 -- CAN");
		Console.WriteLine("4 -- Pastry");
		Console.WriteLine("5 -- Tapestry");
	}

	public static void Show()
	{
		const char ESC = (char)27;

		for (; ; ) {
			Console.Clear();
			Theory.PrintInfo();
			switch (Console.ReadKey(true).KeyChar) {
			case ESC:
				return;
			case '1':
				Theory.ShowDHT();
				break;
			case '2':
				Theory.ShowChord();
				break;
			case '3':
				Theory.ShowCAN();
				break;
			case '4':
				Theory.ShowPastry();
				break;
			case '5':
				Theory.ShowTapestry();
				break;
			default:
				continue;
			}
		}
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
}
