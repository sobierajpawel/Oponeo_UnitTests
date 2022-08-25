// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using SDAOponeo.UnitTests.Benchmark.Tests;

var summary = BenchmarkRunner.Run<StringDigitComparator>();
Console.ReadKey();