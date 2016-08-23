using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

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
                if(container.ContainerStatusEffectManager.StatusEffectInfos.Any(x => x.StatusEffect.StatusEffectID == 1))
                {
                    ContainerStatusEffectInfo info = container.ContainerStatusEffectManager.StatusEffectInfos.First(x => x.StatusEffect.StatusEffectID == 1);
                    Database.Database.RepositoryList.KnowledgeRepositoryList.StatusEffectsRepositoryList.ContainerStatusEffectInfoRepository.Delete(info.ContainerStatusEffectInfoID);
                    container.ContainerStatusEffectManager.UnloadStatusEffectInfo(info);
                }
            }
        }
    }
}
