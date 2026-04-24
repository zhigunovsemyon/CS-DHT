namespace CS_DHT.CAN;

public class CANNode(int d)
{
	public int GetRoutingTableSize()
	{
		// Только список непосредственных соседей по граням
		return Neighbours.Count;
	}
	public Zone MyZone { get; set; } = new Zone(d);

	public List<CANNode> Neighbours { get; } = [];

	// Итеративная маршрутизация для замера чистой скорости
	public (CANNode node, int hops) Route(Point p)
	{
		CANNode current = this;
		int hops = 0;

		do {
			if (current.MyZone.Contains(p)) {
				return (current, hops);
			}

			CANNode? bestNeighbor = null;
			double minDistSq = current.MyZone.CenterDistSq(p);

			foreach (var n in current.Neighbours) {
				double d = n.MyZone.CenterDistSq(p);
				if (d < minDistSq) {
					minDistSq = d;
					bestNeighbor = n;
				}
			}

			if (bestNeighbor == null) {
				return (current, hops);
			}
			current = bestNeighbor;
			hops++;

			if (hops > 5000) {
				break;
			}
		} while (true);

		return (current, hops);
	}
}
