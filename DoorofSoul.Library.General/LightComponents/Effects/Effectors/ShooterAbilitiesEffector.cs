using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.LightComponents.Effects.Effectors
{
    public class ShooterAbilitiesEffector : Effector
    {
        public int ShooterAbilitiesEffectorID { get; private set; }
        public ShooterAbilitiesTypeCode AbilityType { get; private set; }
        public int EffectValue { get; private set; }

        public ShooterAbilitiesEffector(int shooterAbilitiesEffectorID, ShooterAbilitiesTypeCode abilityType, int effectValue)
        {
            ShooterAbilitiesEffectorID = shooterAbilitiesEffectorID;
            AbilityType = abilityType;
            EffectValue = effectValue;
        }
        public override bool Affect(List<IEffectorTarget> targets)
        {
            if (targets.Any(x => x is Container))
            {
                targets.OfType<Container>().ToList().ForEach(x => 
                {
                    switch(AbilityType)
                    {
                        case ShooterAbilitiesTypeCode.BulletDamage:
                            x.ShooterAbilities.Damage += EffectValue;
                            break;
                        case ShooterAbilitiesTypeCode.MoveSpeed:
                            x.ShooterAbilities.MoveSpeed += EffectValue;
                            break;
                        case ShooterAbilitiesTypeCode.BulletSpeed:
                            x.ShooterAbilities.BulletSpeed += EffectValue;
                            break;
                        case ShooterAbilitiesTypeCode.Transparancy:
                            x.ShooterAbilities.Transparancy += EffectValue;
                            break;
                        default:
                            break;
                    }
                });
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
