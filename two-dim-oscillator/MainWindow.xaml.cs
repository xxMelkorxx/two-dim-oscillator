using System;
using System.Windows;
using System.Drawing;
using ScottPlot;
using ScottPlot.Control;

namespace two_dim_oscillator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    // private RungeKutta _rungeKutta;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnLoadedMainWindow(object sender, RoutedEventArgs e)
    {
        // SetUpChart(ChartPosX, "Изменение координаты X от времени", "Время, с", "Координата X, м");
        // SetUpChart(ChartPosY, "Изменение координаты Y от времени", "Время, с", "Координата Y, м");
        // SetUpChart(ChartFe, "Полная энергия системы", "Время, с", "Энергия, Дж");
        // SetUpChart(ChartKe, "Кинетическая энергия тела", "Время, с", "Энергия, Дж");
        // SetUpChart(ChartPe1, "Потенциальная энергия первой пружинки", "Время, с", "Энергия, Дж");
        // SetUpChart(ChartPe2, "Потенциальная энергия второй пружинки", "Время, с", "Энергия, Дж");
    }
    
    private void OnClickButtonStart(object sender, RoutedEventArgs e)
    {
        BtnStop.IsEnabled = true;
        BtnReset.IsEnabled = false;
    }

    private void OnClickButtonStop(object sender, RoutedEventArgs e)
    {
        BtnReset.IsEnabled = true;
    }

    private void OnClickButtonReset(object sender, RoutedEventArgs e)
    {
        BtnStop.IsEnabled = false;
        BtnReset.IsEnabled = false;
    }

    private static void SetUpChart(IPlotControl chart, string title, string labelX, string labelY)
    {
        chart.Plot.Title(title);
        chart.Plot.XLabel(labelX);
        chart.Plot.YLabel(labelY);
        chart.Plot.XAxis.MajorGrid(enable: true, color: Color.FromArgb(50, Color.Black));
        chart.Plot.YAxis.MajorGrid(enable: true, color: Color.FromArgb(50, Color.Black));
        chart.Plot.XAxis.MinorGrid(enable: true, color: Color.FromArgb(30, Color.Black), lineStyle: LineStyle.Dot);
        chart.Plot.YAxis.MinorGrid(enable: true, color: Color.FromArgb(30, Color.Black), lineStyle: LineStyle.Dot);
        chart.Plot.Margins(x: 0.0, y: 0.6);
        chart.Plot.SetAxisLimits(xMin: 0, yMin: 0);
        chart.Refresh();
    }
}