using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PheasantBench.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _ProcessorIdentifier = "PROCESSOR_IDENTIFIER";
        private string _ProcessorArchitecture = "PROCESSOR_ARCHITECTURE";
        private int _NumCores;
        private string _ProcessorName;
        private string _Architecture;
        private string _MachineName;
        private string _OsVersion;
        public MainWindow()
        {
            _ProcessorName = Environment.GetEnvironmentVariable(_ProcessorIdentifier);
            _Architecture = Environment.GetEnvironmentVariable(_ProcessorArchitecture);
            _MachineName = Environment.MachineName.ToString();
            _OsVersion = Environment.OSVersion.ToString();
            _NumCores = Environment.ProcessorCount;

            InitializeComponent();

            CPUCores.Content += _NumCores.ToString();
            ProcessorName.Content += _ProcessorName;
            Architecture.Content += _Architecture;
            MachineName.Content += _MachineName;
            OSVersion.Content += _OsVersion;
        }

        private async void StartBenchmarkButton_Click(object sender, RoutedEventArgs e)
        {
            StartBenchmarkButton.IsEnabled = false;

            CpuBenchmark benchmark = new CpuBenchmark();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            Task benchmarkTask = benchmark.StartAsync(_NumCores);

            while (!benchmarkTask.IsCompleted && stopwatch.Elapsed < TimeSpan.FromMinutes(1))
            {
                BenchmarkScore.Content = benchmark.Score;
                await Task.Delay(1000);
            }

            StartBenchmarkButton.IsEnabled = true;
        }
    }
}