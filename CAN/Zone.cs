namespace CS_DHT.CAN;

public class Zone
{
	public double[] Min { get; }

	public double[] Max { get; }

	private int D { get; }

	public Zone(int d)
	{
		this.D = d;
		this.Min = new double[d];
		this.Max = new double[d];
		for (int i = 0; i < d; i++) {
			this.Max[i] = 1.0;
		}
	}
	public bool Contains(Point p)
	{
		for (int i = 0; i < this.D; i++) {
			if (p.Coords[i] < this.Min[i] || p.Coords[i] > this.Max[i]) {
				return false;
			}
		}
		return true;
	}

	// Используем квадрат расстояния для скорости
	public double CenterDistSq(Point p)
	{
		double dist = 0;
		for (int i = 0; i < D; i++) {
			double center = (Min[i] + Max[i]) * 0.5;
			double diff = center - p.Coords[i];
			dist += diff * diff;
		}
		return dist;
	}

	public static bool AreAdjacent(Zone z1, Zone z2)
	{
		int touching = 0;
		for (int i = 0; i < z1.D; i++) {
			bool overlap = !(z1.Max[i] < z2.Min[i] || z2.Max[i] < z1.Min[i]);
			bool touch = Math.Abs(z1.Max[i] - z2.Min[i]) < 1e-9 || Math.Abs(z2.Max[i] - z1.Min[i]) < 1e-9;
			if (touch) {
				touching++;
			}
			else if (!overlap) {
				return false;
			}
		}
		return touching == 1;
	}
}
