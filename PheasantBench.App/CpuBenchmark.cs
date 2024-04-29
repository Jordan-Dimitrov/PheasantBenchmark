using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.App
{
    class CpuBenchmark
    {
        private bool _StopCheck;
        private long _Score;

        public async Task StartAsync(int numberOfCores)
        {
            _StopCheck = false;
            _Score = 0;

            Task[] tasks = new Task[numberOfCores];
            for (int i = 0; i < numberOfCores; i++)
            {
                tasks[i] = Task.Run(() => StressCoreAsync());
            }

            await Task.WhenAll(tasks);
        }

        private void StressCoreAsync()
        {
            while (!_StopCheck)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    double result = Math.Sqrt(i) * Math.Sin(i);
                }
                _Score++;
            }
        }

        public long Score => _Score;
    }
}
