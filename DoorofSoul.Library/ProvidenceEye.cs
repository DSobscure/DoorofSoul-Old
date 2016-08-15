using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DoorofSoul.Library.General.SceneElements;

namespace DoorofSoul.Library
{
    public class ProvidenceEye : SceneEye
    {
        private CancellationTokenSource monitorCancellationTokenSource;
        private Task monitorTask;

        public override void StartMonitot(int cycleTimeMilliseconds)
        {
            StopMonotot();
            monitorCancellationTokenSource = new CancellationTokenSource();
            monitorTask = Task.Run(() => MonitorLoop(cycleTimeMilliseconds), monitorCancellationTokenSource.Token);
        }

        public override void StopMonotot()
        {
            monitorCancellationTokenSource?.Cancel();
        }

        private async void MonitorLoop(int cycleTimeMilliseconds)
        {
            while (!monitorTask.IsCanceled)
            {
                if (Observer != null)
                {
                    Monitor();
                }
                await Task.Delay(cycleTimeMilliseconds);
            }
        }
    }
}
