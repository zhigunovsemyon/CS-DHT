namespace CS_DHT.CAN;

public class CANSimulator(int d)
{
	private readonly List<CANNode> _nodes = [];
	private readonly int _dimensions = d;
	private readonly Random _rand = new();

	public void BuildNetwork(int count)
	{
		var first = new CANNode(this._dimensions);
		this._nodes.Add(first);

		for (int i = 1; i < count; i++) {
			var newNode = new CANNode(this._dimensions);

			var pointParam = Enumerable.Range(0, this._dimensions)
				.Select(_ => _rand.NextDouble())
				.ToArray();
			var p = new Point(pointParam);

			var target = first.Route(p).node;

			int splitDim = 0;
			double maxLen = 0;
			for (int d = 0; d < this._dimensions; d++) {
				double l = target.MyZone.Max[d] - target.MyZone.Min[d];
				if (l > maxLen) {
					maxLen = l;
					splitDim = d;
				}
			}

			double mid = (target.MyZone.Min[splitDim] + target.MyZone.Max[splitDim]) * 0.5;
			Array.Copy(target.MyZone.Min, newNode.MyZone.Min, this._dimensions);
			Array.Copy(target.MyZone.Max, newNode.MyZone.Max, this._dimensions);
			newNode.MyZone.Min[splitDim] = mid;
			target.MyZone.Max[splitDim] = mid;

			// Обновление связей
			var oldNeighbors = target.Neighbours.ToList();
			target.Neighbours.Clear();
			target.Neighbours.Add(newNode);
			newNode.Neighbours.Add(target);

			foreach (var n in oldNeighbors) {
				n.Neighbours.Remove(target);
				if (Zone.AreAdjacent(target.MyZone, n.MyZone)) {
					target.Neighbours.Add(n); n.Neighbours.Add(target);
				}
				if (Zone.AreAdjacent(newNode.MyZone, n.MyZone)) {
					newNode.Neighbours.Add(n); n.Neighbours.Add(newNode);
				}
			}
			this._nodes.Add(newNode);
		}
	}

	public double GetAverageHops(int queryCount)
	{
		var points = new List<Point>(queryCount);
		for (int i = 0; i < queryCount; i++) {
			var pointParam = Enumerable.Range(0, this._dimensions)
				.Select(_ => this._rand.NextDouble())
				.ToArray();
			points.Add(new Point(pointParam));
		}

		long totalHops = 0;

		foreach (var p in points) {
			var startNode = _nodes[_rand.Next(_nodes.Count)];
			var (_, hops) = startNode.Route(p);
			totalHops += hops;
		}

		return (double)totalHops / queryCount;
	}

	public void RunBenchmark(int queryCount)
	{
		Console.Write($"\n--- Алгоритм CAN {this._dimensions}D.");
		Console.WriteLine($" Результаты теста ({this._nodes.Count} узлов, {queryCount} запросов) ---");

		var points = new List<Point>(queryCount);
		for (int i = 0; i < queryCount; i++) {
			var pointParam = Enumerable.Range(0, this._dimensions)
				.Select(_ => _rand.NextDouble())
				.ToArray();
			points.Add(new Point(pointParam));
		}

		long totalHops = 0;

		var sw = System.Diagnostics.Stopwatch.StartNew();
		foreach (var p in points) {
			var startNode = _nodes[_rand.Next(_nodes.Count)];
			var (_, hops) = startNode.Route(p);
			totalHops += hops;
		}
		sw.Stop();

		double avgMs = (double)sw.ElapsedMilliseconds / queryCount;
		double avgHops = (double)totalHops / queryCount;
		double avgNeighbours = this._nodes.Average(n => n.Neighbours.Count);

		Console.WriteLine($"Общее время: {sw.ElapsedMilliseconds} мс");
		Console.WriteLine($"Среднее время на 1 запрос: {avgMs:F4} мс");
		Console.WriteLine($"Среднее кол-во прыжков: {avgHops:F2}");
		Console.WriteLine($"Среднее число соседей у узла: {avgNeighbours:F2}");
	}
}
