namespace CS_DHT.Chord;

public class ChordNode
{
	private const int M = 32; // Используем 32-битное пространство ID

	public int Id { get; }

	public ChordNode Successor { get; set; }

	public ChordNode[] FingerTable { get; }

	// Finger Table (обычно 32) + Successor + Predecessor
	public int GetRoutingTableSize() => FingerTable.Length + 2;

	public ChordNode(int id)
	{
		this.Id = id;
		this.FingerTable = new ChordNode[M];
		this.Successor = this;
	}

	// Быстрый поиск (используется для тестов)
	public (ChordNode node, int hops) FindSuccessor(int id, int hops = 0)
	{
		if (IsInRangeIE(id, this.Id, this.Successor.Id)) {
			return (this.Successor, hops + 1);
		}

		ChordNode closest = this.ClosestPrecedingNode(id);
		if (closest == this) {
			return (this.Successor, hops + 1);
		}

		return closest.FindSuccessor(id, hops + 1);
	}

	private ChordNode ClosestPrecedingNode(int id)
	{
		for (int i = M - 1; i >= 0; i--) {
			if (this.FingerTable[i] != null && IsInRangeEE(this.FingerTable[i].Id, this.Id, id)) {
				return this.FingerTable[i];
			}
		}
		return this;
	}

	private static bool IsInRangeEE(int val, int start, int end) =>
		(start < end) 
			? (val > start && val < end) 
			: (val > start || val < end);

	private static bool IsInRangeIE(int val, int start, int end) =>
		(start < end) 
			? (val > start && val <= end) 
			: (val > start || val <= end);
}
