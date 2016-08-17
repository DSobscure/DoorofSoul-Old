using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.SceneElements;

namespace DoorofSoul.Library
{
    public class ProvidenceEye : SceneEye
    {
        private CancellationTokenSource monitorCancellationTokenSource;
        private Task monitorTask;
        private int cycleTimeMilliseconds;

        public override void BindScene(Scene scene)
        {
            base.BindScene(scene);
            Scene.OnContainerExit += OnContainerExit;
        }

        public override void StartMonitor(int cycleTimeMilliseconds)
        {
            this.cycleTimeMilliseconds = cycleTimeMilliseconds;
            monitorCancellationTokenSource = new CancellationTokenSource();
            monitorTask = Task.Run(() => MonitorLoop(), monitorCancellationTokenSource.Token);
        }

        public override void StopMonitor()
        {
            monitorCancellationTokenSource?.Cancel();
            Observer = null;
            ObserverLevel = 0;
        }

        private void MonitorLoop()
        {
            while (!monitorCancellationTokenSource.Token.IsCancellationRequested)
            {
                if (Observer != null)
                {
                    Monitor();
                }
                monitorTask.Wait(cycleTimeMilliseconds);
            }
        }
        private void OnContainerExit(Container container)
        {
            if (container == Observer)
            {
                StopMonitor();
            }
        }
    }
}
