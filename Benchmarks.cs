namespace CS_DHT;

using System.Diagnostics;
using CAN;
using Chord;
using Pastry;

public class Benchmarks
{
	private readonly ChordSimulator _chord = new();
	private readonly CANSimulator _can2d = new(2);
	private readonly CANSimulator _can10d = new(10);
	private readonly PastrySimulator _pastry = new();

	public static Benchmarks BuildNetworks(int nodeCount)
	{
		var bench = new Benchmarks();

		bench._chord.BuildNetwork(nodeCount);
		bench._can2d.BuildNetwork(nodeCount);
		bench._can10d.BuildNetwork(nodeCount);
		bench._pastry.BuildNetwork(nodeCount);

		return bench;
	}

	public void RunBenchmarks(int queryCount)
	{ 
		this._chord.RunBenchmark(queryCount);
		this._pastry.RunBenchmark(queryCount);
		this._can2d.RunBenchmark(queryCount);
		this._can10d.RunBenchmark(queryCount);	
	}

	public void Avgs(int queryCount)
	{
		var sw = Stopwatch.StartNew();
		double avgHops = this._chord.GetAverageHops(queryCount);
		sw.Stop();
		Console.WriteLine($"Chord: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");


		sw = Stopwatch.StartNew();
		avgHops = this._can2d.GetAverageHops(queryCount);
		sw.Stop();
		Console.WriteLine($"CAN 2D: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");


		sw = Stopwatch.StartNew();
		avgHops = this._can10d.GetAverageHops(queryCount);
		sw.Stop();
		Console.WriteLine($"CAN 10D: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");


		sw = Stopwatch.StartNew();
		avgHops = this._pastry.GetAverageHops(queryCount);
		sw.Stop();
		Console.WriteLine($"Pastry: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");
	}
	private Benchmarks()
	{
		Console.WriteLine("Построение бенчмарков");
	}
}
