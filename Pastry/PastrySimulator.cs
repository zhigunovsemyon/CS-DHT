namespace CS_DHT.Pastry;

public class PastrySimulator
{
	private readonly List<PastryNode> _nodes = [];
	private readonly Random _rand = new();

	public void BuildNetwork(int nodeCount)
	{
		//Console.WriteLine($"Генерация {nodeCount} узлов Pastry...");
		while (this._nodes.Count < nodeCount) {
			var id = this._rand.Next();
			_nodes.Add(new PastryNode(id));
		}

		foreach (var node in this._nodes) {
			node.InitializeTables(this._nodes);
		}
	}

	//todo: избавится (?) от списка хопов
	// избавится от метода (?)
	public double GetAverageHops(int queryCount)
	{
		var hopCounts = new List<int>(queryCount);

		for (int i = 0; i < queryCount; i++) {
			int targetKey = this._rand.Next();
			var startNode = _nodes[this._rand.Next(_nodes.Count)];
			var (_, hops) = startNode.Route(targetKey);
			hopCounts.Add(hops);
		}


		return hopCounts.Average();
	}

	//todo: избавится (?) от списка хопов
	public void RunBenchmark(int queryCount)
	{
		Console.WriteLine($"\n--- Алгоритм Pastry. Результаты теста ({this._nodes.Count} узлов, {queryCount} запросов) ---");
		var hopCounts = new List<int>(queryCount);
		var sw = System.Diagnostics.Stopwatch.StartNew();

		for (int i = 0; i < queryCount; i++) {
			int targetKey = this._rand.Next();
			var nodeId = this._rand.Next(this._nodes.Count);
			var startNode = this._nodes[nodeId];
			var (_, hops) = startNode.Route(targetKey);
			hopCounts.Add(hops);
		}

		sw.Stop();
		Console.WriteLine($"Среднее кол-во прыжков: {hopCounts.Average():F2}");
		Console.WriteLine($"Макс. прыжков: {hopCounts.Max()}");
		Console.WriteLine($"Время: {sw.ElapsedMilliseconds} мс");
	}

}
