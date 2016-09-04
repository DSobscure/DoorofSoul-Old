using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.MindComponents;
using System.Threading.Tasks;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.ElementComponents;

namespace DoorofSoul.Hexagram.NatureComponents.SceneElements
{
    public class NonPlayerContainerManager
    {
        protected Scene scene;
        protected Soul controlSoul;
        protected Dictionary<int, Container> NPCDictionary;
        private int containerCounter;
        private List<int> itemList;

        public NonPlayerContainerManager()
        {
            NPCDictionary = new Dictionary<int, Container>();
            containerCounter = 10000;
            itemList = new List<int>
            {
                3, 4, 5, 6, 7
            };
        }

        public void StartManage(Scene scene, Soul soul)
        {
            this.scene = scene;
            controlSoul = soul;
            Task.Run((Action)AutoManage);
        }

        public async void AutoManage()
        {
            int npcRatio = 5;
            while (controlSoul.IsActivate)
            {
                if (NPCDictionary.Count < (scene.ContainerCount - NPCDictionary.Count) * npcRatio)
                {
                    int createNumber = (scene.ContainerCount - NPCDictionary.Count) * npcRatio - NPCDictionary.Count;
                    for (int i = 0; i < createNumber; i++)
                    {
                        InstantiateShooter();
                    }
                }
                Hexagram.Log.Debug("30sec NPC created");
                await Task.Delay(30000);
            }
        }
        private void InstantiateShooter()
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            Entity npcEntity = new Entity(containerCounter, "TestContainer", scene.SceneID, new EntitySpaceProperties
            {
                Position = new DSVector3 { x = randomGenerator.Next(-50, 50), y = 0, z = randomGenerator.Next(-50, 50) },
                Scale = new DSVector3 { x = 1, y = 1, z = 1 }
            });
            Container npcContainer = new Container(containerCounter, containerCounter, "", ContainerAttributes.GetDefaultAttribute());
            npcContainer.BindEntity(npcEntity);
            npcContainer.BindInventory(new Inventory(containerCounter, containerCounter, 1));
            npcContainer.Attributes.OnLifePointChange += (lifePoint, delta) => OnShooterDie(npcContainer, lifePoint);

            controlSoul.Answer.LoadContainers(new List<Container> { npcContainer });
            controlSoul.Answer.LinkSoulContainer(controlSoul.SoulID, npcContainer.ContainerID);
            Hexagram.Nature.ContainerManager.ProjectContainer(npcContainer);
            NPCDictionary.Add(npcContainer.ContainerID, npcContainer);
            Task.Run(()=> AutoControl(npcContainer));
            containerCounter++;
        }

        private async void AutoControl(Container shooter)
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            while (shooter.Entity.LocatedScene != null)
            {
                switch(randomGenerator.Next(0,3))
                {
                    case 0:
                        shooter.Entity.EntityEventManager.StartMove(randomGenerator.Next(-1, 1) * 5 * (1 + shooter.ShooterAbilities.MoveSpeed * 0.3f));
                        break;
                    case 1:
                        shooter.Entity.EntityEventManager.StartRotate(randomGenerator.Next(-1, 1) * 1);
                        break;
                    case 2:
                        shooter.ShootABullet();
                        break;
                    case 3:
                        break;
                }
                await Task.Delay(randomGenerator.Next(500, 5000));
            }
        }
        private void OnShooterDie(Container shooter, decimal lifePoint)
        {
            if(lifePoint <= 0)
            {
                Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
                scene.ItemEntityManager.CreateItemEntity(itemList[randomGenerator.Next(0, itemList.Count - 1)], shooter.Entity.Position);
                Hexagram.Nature.ContainerManager.ExtractContainer(shooter);
                controlSoul.UnlinkContainer(shooter);
                shooter.UnlinkSoul(controlSoul);
                NPCDictionary.Remove(shooter.ContainerID);
            }
        }
    }
}
