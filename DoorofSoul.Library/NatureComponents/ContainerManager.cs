﻿using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Library.General.NatureComponents;
using System.Threading.Tasks;

namespace DoorofSoul.Hexagram.NatureComponents
{
    public class ContainerManager
    {
        private Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }

        public ContainerManager()
        {
            containerDictionary = new Dictionary<int, Container>();
        }
        public void Initial()
        {

        }

        public bool ContainsContainer(int container)
        {
            return containerDictionary.ContainsKey(container);
        }
        public Container FindContainer(int container)
        {
            if (ContainsContainer(container))
            {
                return containerDictionary[container];
            }
            else
            {
                return null;
            }
        }

        public void ProjectContainer(Container container)
        {
            if (!ContainsContainer(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
                AssenblyContainer(container);
                if (Hexagram.Nature.SceneManager.ContainsScene(container.Entity.LocatedSceneID))
                {
                    Scene scene = Hexagram.Nature.SceneManager.FindScene(container.Entity.LocatedSceneID);
                    if (Hexagram.Nature.WorldManager.ContainsWorld(scene.WorldID))
                    {
                        Hexagram.Nature.WorldManager.FindWorld(scene.WorldID).ContainerEnter(container);
                        scene.ContainerEnter(container);
                        Hexagram.Nature.EntityManager.ProjectEntity(container.Entity);
                    }
                    else
                    {
                        Hexagram.Log.ErrorFormat("Hexagram: World Not Exist WorldID: {0}", scene.WorldID);
                    }
                }
                else
                {
                    Hexagram.Log.ErrorFormat("Hexagram: Scene Not Exist SceneID: {0}", container.Entity.LocatedSceneID);
                }
            }
        }
        public void ExtractContainer(Container container)
        {
            if (containerDictionary.ContainsKey(container.ContainerID))
            {
                Database.Database.RepositoryList.NatureRepositoryList.ContainerRepository.Save(container);
                Scene scene = container.Entity.LocatedScene;
                Hexagram.Nature.EntityManager.ExtractEntity(container.Entity);
                scene.ContainerExit(container.ContainerID);
                scene.World.ContainerExit(container);
                containerDictionary.Remove(container.ContainerID);
                DisassemblyContainer(container);
            }
        }
        public void AssenblyContainer(Container container)
        {
            container.ContainerStatusEffectManager.InitialStatusEffectInfos(Database.Database.RepositoryList.KnowledgeRepositoryList.StatusEffectsRepositoryList.ContainerStatusEffectInfoRepository.ListOfAffected(container.ContainerID));
            container.Attributes.OnLifePointChange += container.ContainerEventManager.InformDataResolver.InformLifePointChange;
            container.Attributes.OnEnergyPointChange += container.ContainerEventManager.InformDataResolver.InformEnergyPointChange;
            container.ContainerStatusEffectManager.OnContainerStatusEffectInfoChange += container.ContainerEventManager.InformDataResolver.InformContainerStatusEffectInfoChange;
            container.Inventory.OnItemChange += container.ContainerEventManager.InformDataResolver.InformInventoryItemInfoChange;

            container.Attributes.OnLifePointChange += (lifePoint, delta) => OnShooterDie(container, lifePoint);
            container.OnShootBullet += ShootBulletCoolDown;
            container.ShooterAbilities.OnDamageChange += container.ContainerEventManager.InformDataResolver.InformBulletDamageChange;
            container.ShooterAbilities.OnMoveSpeedChange += container.ContainerEventManager.InformDataResolver.InformMoveSpeedChange;
            container.ShooterAbilities.OnBulletSpeedChange += container.ContainerEventManager.InformDataResolver.InformBulletSpeedChange;
            container.ShooterAbilities.OnTransparancyChange += container.ContainerEventManager.InformDataResolver.InformTranspancyChange;
        }
        public void DisassemblyContainer(Container container)
        {
            container.Attributes.OnLifePointChange -= container.ContainerEventManager.InformDataResolver.InformLifePointChange;
            container.Attributes.OnEnergyPointChange -= container.ContainerEventManager.InformDataResolver.InformEnergyPointChange;
            container.ContainerStatusEffectManager.OnContainerStatusEffectInfoChange -= container.ContainerEventManager.InformDataResolver.InformContainerStatusEffectInfoChange;
            container.Inventory.OnItemChange -= container.ContainerEventManager.InformDataResolver.InformInventoryItemInfoChange;

            container.OnShootBullet -= ShootBulletCoolDown;
            container.ShooterAbilities.OnDamageChange -= container.ContainerEventManager.InformDataResolver.InformBulletDamageChange;
            container.ShooterAbilities.OnMoveSpeedChange -= container.ContainerEventManager.InformDataResolver.InformMoveSpeedChange;
            container.ShooterAbilities.OnBulletSpeedChange -= container.ContainerEventManager.InformDataResolver.InformBulletSpeedChange;
            container.ShooterAbilities.OnTransparancyChange -= container.ContainerEventManager.InformDataResolver.InformTranspancyChange;
        }
        private async void ShootBulletCoolDown(Container shooter)
        {
            shooter.CanShootBullet = false;
            await Task.Delay(500);
            shooter.CanShootBullet = true;
        }
        private async void OnShooterDie(Container shooter, decimal lifePoint)
        {
            if(lifePoint <= 0)
            {
                shooter.ShooterAbilities.Damage = 0;
                shooter.ShooterAbilities.MoveSpeed = 0;
                shooter.ShooterAbilities.BulletSpeed = 0;
                shooter.ShooterAbilities.Transparancy = 0;
                await Task.Delay(500);
                shooter.Attributes.LifePoint = shooter.Attributes.MaxLifePoint / 2;
            }
        }
    }
}
