namespace CS_DHT.Pastry;

public class PastryNode(int id)
{
	private readonly int Id = id;

	private const int B = 4; // Основание 2^4 = 16

	private const int IdBits = 32;

	private const int Digits = IdBits / B;

	private const int LeafSetSize = 8;

	public List<PastryNode> LeafSet { get; private set; } = [];

	public PastryNode[,] RoutingTable { get; private set; } = new PastryNode[Digits, 1 << B];

	public int GetRoutingTableSize()
	{
		int count = 0;
		// Считаем непустые ячейки в матрице маршрутизации
		foreach (var n in this.RoutingTable) {
			if (n != null) {
				count++;
			}
		}

		// Плюс Leaf Set
		return count + this.LeafSet.Count;
	}

	private static int GetDigit(int val, int digitPos)
	{
		int shift = (Digits - 1 - digitPos) * B;
		return (val >> shift) & 0xF;
	}

	private static int CommonPrefixLength(int id1, int id2)
	{
		for (int i = 0; i < PastryNode.Digits; i++) {
			if (PastryNode.GetDigit(id1, i) != PastryNode.GetDigit(id2, i)) {
				return i;
			}
		}
		return Digits;
	}

	// ИТЕРАТИВНЫЙ ПОИСК (защищен от Stack Overflow)
	public (PastryNode node, int hops) Route(int targetId)
	{
		PastryNode currentNode = this;
		int hops = 0;
		var visited = new HashSet<int>(); // Защита от бесконечных циклов

		// Ограничитель на случай ошибок в логике
		while (hops < 100) {
			if (currentNode.Id == targetId || visited.Contains(currentNode.Id)) {
				return (currentNode, hops);
			}
			visited.Add(currentNode.Id);

			// 1. Проверка в Leaf Set (самые близкие по значению)
			var closestInLeaf = currentNode.LeafSet
				.OrderBy(n => Math.Abs((long)n.Id - targetId))
				.FirstOrDefault();

			if (closestInLeaf != null && currentNode.IsInLeafSetRange(targetId)) {
				if (Math.Abs((long)closestInLeaf.Id - targetId) < Math.Abs((long)currentNode.Id - targetId)) {
					currentNode = closestInLeaf;
					hops++;
					continue;
				}
				else {
					return (currentNode, hops);
				}
			}

			// 2. Поиск по таблице префиксов
			int l = CommonPrefixLength(currentNode.Id, targetId);
			int digit = GetDigit(targetId, l);
			PastryNode nextNode = currentNode.RoutingTable[l, digit];

			if (nextNode != null) {
				currentNode = nextNode;
				hops++;
				continue;
			}

			// 3. Если в таблице пусто, ищем любого, кто хоть чуть-чуть ближе по префиксу или ID
			var fallbackNode = currentNode.RoutingTable.Cast<PastryNode>()
				.Concat(currentNode.LeafSet)
				.Where(n => n != null && !visited.Contains(n.Id))
				.OrderByDescending(n => CommonPrefixLength(n.Id, targetId))
				.ThenBy(n => Math.Abs((long)n.Id - targetId))
				.FirstOrDefault();

			if (fallbackNode is null) {
				// Ближе найти не удалось
				return (currentNode, hops);
			}

			if ((CommonPrefixLength(fallbackNode.Id, targetId) >= l) &&
				Math.Abs((long)fallbackNode.Id - targetId) < Math.Abs((long)currentNode.Id - targetId)) {
				currentNode = fallbackNode;
				hops++;
			}
			else {
				// Ближе найти не удалось
				return (currentNode, hops);
			}
		}

		return (currentNode, hops);
	}

	private bool IsInLeafSetRange(int targetId)
	{
		if (LeafSet.Count == 0) {
			return false;
		}
		return targetId >= LeafSet.Min(n => n.Id) && targetId <= LeafSet.Max(n => n.Id);
	}

	public void InitializeTables(List<PastryNode> allNodes)
	{
		// Берем ближайших соседей по числовому значению
		this.LeafSet = allNodes
			.Where(n => n.Id != this.Id)
			.OrderBy(n => Math.Abs(n.Id - this.Id))
			.Take(LeafSetSize)
			.ToList();

		foreach (var node in allNodes) {
			if (node.Id == this.Id) {
				continue;
			}

			int l = PastryNode.CommonPrefixLength(this.Id, node.Id);
			int digit = GetDigit(node.Id, l);

			// В симуляции: если ячейка пуста или новый узел "лучше" (ближе)
			if (this.RoutingTable[l, digit] == null) {
				this.RoutingTable[l, digit] = node;
			}
		}
	}
}

