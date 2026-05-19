namespace CS_DHT.Chord;

public class ChordSimulator
{
	private readonly List<ChordNode> _nodes = [];
	private readonly Random _rand = new();

	private void createNodesList(int nodeCount)
	{
		var sortedIds = new SortedSet<int>();
		while (sortedIds.Count < nodeCount) {
			sortedIds.Add(this._rand.Next());
		}

		this._nodes.Capacity = sortedIds.Count;
		foreach (var id in sortedIds) {
			this._nodes.Add(new ChordNode(id));
		}
	}

	private void assignSuccessors()
	{
		for (int i = 0; i < this._nodes.Count; i++) {
			this._nodes[i].Successor = this._nodes[(i + 1) % this._nodes.Count];
		}
	}

	private void makeFingerTables()
	{
		foreach (var node in this._nodes) {
			for (int i = 0; i < ChordNode.M; i++) {
				int fingerStart = node.Id + (int)Math.Pow(2, i);
				node.FingerTable[i] = this.FastBinarySearchSuccessor(fingerStart);
			}
		}
	}

	public void BuildNetwork(int nodeCount)
	{
		this.createNodesList(nodeCount);
		this.assignSuccessors();
		this.makeFingerTables();
	}

	private ChordNode FastBinarySearchSuccessor(int id)
	{
		int left = 0;
		int right = this._nodes.Count - 1;

		if ((id > this._nodes.Last().Id) || (id <= this._nodes[0].Id)) {
			return this._nodes[0];
		}

		while (left <= right) {
			int mid = left + (right - left) / 2;

			if (this._nodes[mid].Id < id) {
				left = mid + 1;
			}
			else {
				right = mid - 1;
			}
		}
		return this._nodes[left];
	}

	//todo: отказаться от списка hopCount (?)
	public double GetAverageHops(int queryCount)
	{
		var hopCounts = new List<int>(queryCount);

		for (int i = 0; i < queryCount; i++) {
			int key = this._rand.Next();
			var startNode = this._nodes[this._rand.Next(this._nodes.Count)];
			var (_, hops) = startNode.FindSuccessor(key);
			hopCounts.Add(hops);
		}

		return hopCounts.Average();
	}


	//todo: отказаться от списка hopCount (?)
	public void RunBenchmark(int queryCount)
	{
		Console.WriteLine($"\n--- Алгоритм Chord. Результаты теста ({this._nodes.Count} узлов, {queryCount} запросов) ---");

		var hopCounts = new List<int>(queryCount);
		var sw = System.Diagnostics.Stopwatch.StartNew();

		for (int i = 0; i < queryCount; i++) {
			int key = this._rand.Next();
			var startNode = this._nodes[this._rand.Next(this._nodes.Count)];
			var (_, hops) = startNode.FindSuccessor(key);
			hopCounts.Add(hops);
		}

		sw.Stop();

		double avgHops = hopCounts.Average();
		double theoreticalMax = Math.Log(this._nodes.Count);

		Console.WriteLine($"Затрачено времени: {sw.ElapsedMilliseconds} мс");
		Console.WriteLine($"Среднее кол-во прыжков (Hops): {avgHops:F2}");
		Console.WriteLine($"Теоретический предел O(log N): {theoreticalMax:F2}");
		Console.WriteLine($"Макс. прыжков за запрос: {hopCounts.Max()}");

		// Проверка распределения
		var distribution = new Dictionary<int, int>();
		for (int i = 0; i < 10_000; i++) {
			int key = i * 100_000;
			var responsibleId = this.FastBinarySearchSuccessor(key).Id;

			if (!distribution.TryGetValue(responsibleId, out int value)) {
				value = 0;
				distribution[responsibleId] = value;
			}
			distribution[responsibleId] = ++value;
		}

		var maxVal = distribution.Values.Max();
		Console.WriteLine($"Равномерность (нагрузка): узел-лидер держит {maxVal} ключей из теста");
	}

}
