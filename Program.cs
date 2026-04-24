using CS_DHT.CAN;
using CS_DHT.Pastry;
using CS_DHT.Chord;
using System.Diagnostics;


var chord = new ChordSimulator();
var can2d = new CANSimulator(2);
var can10d = new CANSimulator(10);
var pastry = new PastrySimulator();

chord.BuildNetwork(10_000);
can2d.BuildNetwork(10_000);
can10d.BuildNetwork(10_000);
pastry.BuildNetwork(10_000);

Console.WriteLine("Сети построены");

void benchmarks()
{
	chord.RunBenchmark(100_000);
	pastry.RunBenchmark(100_000);
	can2d.RunBenchmark(100_000);
	can10d.RunBenchmark(100_000);
}

void avgs()
{
	var sw = Stopwatch.StartNew();
	double avgHops = chord.GetAverageHops(100_000);
	sw.Stop();
	Console.WriteLine($"Chord: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");


	sw = Stopwatch.StartNew();
	avgHops = can2d.GetAverageHops(100_000);
	sw.Stop();
	Console.WriteLine($"CAN 2D: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");


	sw = Stopwatch.StartNew();
	avgHops = can10d.GetAverageHops(100_000);
	sw.Stop();
	Console.WriteLine($"CAN 10D: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");


	sw = Stopwatch.StartNew();
	avgHops = pastry.GetAverageHops(100_000);
	sw.Stop();
	Console.WriteLine($"Pastry: {sw.ElapsedMilliseconds}, avg hops: {avgHops}");
}

benchmarks();
//avgs();

Console.Write("Нажмите любую кнопку");
Console.ReadLine();