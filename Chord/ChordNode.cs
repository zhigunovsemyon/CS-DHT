namespace CS_DHT.Chord;

public class ChordNode
{
	// 32-битное пространство ID
	private const int M = 32; 

	public int Id { get; init; }

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
	public (ChordNode node, int hops) FindSuccessor(int id) => this.FindSuccessor(id, 0);

	private (ChordNode node, int hops) FindSuccessor(int id, int hops)
	{
		static bool IsInRangeWithEnd(int val, int start, int end) =>
			(start < end) 
				? (val > start && val <= end) 
				: (val > start || val <= end);

		if (IsInRangeWithEnd(id, this.Id, this.Successor.Id)) {
			return (this.Successor, hops + 1);
		}

		var closestPrecedingNode = this.ClosestPrecedingNode(id);
		return (closestPrecedingNode == this) 
			? (this.Successor, hops + 1)
			: closestPrecedingNode.FindSuccessor(id, hops + 1);
	}

	private ChordNode ClosestPrecedingNode(int id)
	{
		static bool IsInRangeWithoutEnd(int val, int start, int end) =>
			(start < end) 
				? (val > start && val < end) 
				: (val > start || val < end);

		for (int i = M - 1; i >= 0; i--) {
			bool inRange = IsInRangeWithoutEnd(this.FingerTable[i].Id, this.Id, id);
			if ((this.FingerTable[i] != null) && inRange) {
				return this.FingerTable[i];
			}
		}
		return this;
	}
}
