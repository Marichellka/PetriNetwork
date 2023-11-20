using ScottPlot;
using ScottPlot.Statistics;

namespace PetriNetwork.RepairWorkshop;

public static class StatisticHelper
{
    public static double GetMean(List<double> distribution)
    {
        double mean = 0;

        foreach (var x in distribution)
        {
            mean += x;
        }
        mean /= distribution.Count;
            
        return mean;
    }
    
    public static double GetVariation(List<double> distribution, double mean)
    {
        double sumOfSquares = 0;
        foreach (var x in distribution)
        {
            sumOfSquares += x*x;
        }

        double variation = sumOfSquares / distribution.Count - mean * mean;
        return variation;
    }
    
    public static List<double> GetFrequencies(List<double> distribution, int segmentsCount)
    {
        double[] frequencies = new double[segmentsCount];
        double min = distribution.Min();
        double segmentLength = (distribution.Max() - min) / segmentsCount;

        foreach (var x in distribution)
        {
            int segment = (int)((x - min) / segmentLength);
            if (segment == segmentsCount) segment--;
            frequencies[segment]++;
        }

        return frequencies.ToList();
    }
    
    public static void ShowPlot(List<double> numbers)
    {
        double min = numbers.Min();
        double max = numbers.Max();

        Plot plot = new();
        Histogram hist = new(min, max, numbers.Count);

        hist.AddRange(numbers);

        var bar = plot.AddBar(values: hist.Counts, positions: hist.Bins);
        bar.BarWidth = (max - min) / hist.BinCount;
        
        WpfPlotViewer viewer = new(plot);
        viewer.ShowDialog();
    }
}