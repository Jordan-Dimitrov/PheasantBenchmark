using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace PheasantBench.App
{
    public partial class MainWindow : Window
    {
        private const string _ProcessorIdentifier = "PROCESSOR_IDENTIFIER";
        private const string _ProcessorArchitecture = "PROCESSOR_ARCHITECTURE";
        private int _NumCores;
        private string _ProcessorName;
        private string _Architecture;
        private string _MachineName;
        private string _OsVersion;
        private readonly HttpClient _HttpClient;

        public MainWindow()
        {
            _ProcessorName = Environment.GetEnvironmentVariable(_ProcessorIdentifier);
            _Architecture = Environment.GetEnvironmentVariable(_ProcessorArchitecture);
            _MachineName = Environment.MachineName.ToString();
            _OsVersion = Environment.OSVersion.ToString();
            _NumCores = Environment.ProcessorCount;
            _HttpClient = new HttpClient();

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

            while (!benchmarkTask.IsCompleted && stopwatch.Elapsed < TimeSpan.FromSeconds(30))
            {
                BenchmarkScore.Content = benchmark.Score;
                await Task.Delay(1000);
            }

            benchmark.Stop();
            await benchmarkTask;

            StartBenchmarkButton.IsEnabled = true;

            string tokenFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "token.txt");
            string jwtToken = await File.ReadAllTextAsync(tokenFilePath);

            var dto = new CreateBenchmarkDto()
            {
                ProcessorName = _ProcessorName,
                Architecture = _Architecture,
                MachineName = _MachineName,
                OsVersion = _OsVersion,
                Score = benchmark.Score
            };

            await PostBenchmarkAsync(dto, jwtToken);
        }

        public async Task PostBenchmarkAsync(CreateBenchmarkDto benchmarkDto, string jwtToken)
        {
            var requestUri = "https://localhost:7203/api/BenchmarkApi";
            var jsonContent = JsonConvert.SerializeObject(benchmarkDto);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            _HttpClient.DefaultRequestHeaders.Add("Cookie", $"jwtToken={jwtToken}");
            await _HttpClient.PostAsync(requestUri, content);
        }
    }
}
