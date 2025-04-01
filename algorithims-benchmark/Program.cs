using algorithims_benchmark;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<SortingAlgorithms>();
