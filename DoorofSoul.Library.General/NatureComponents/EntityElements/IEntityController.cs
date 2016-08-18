using UnityEngine;

namespace DoorofSoul.Library.General.NatureComponents.EntityElements
{
    public interface IEntityController
    {
        GameObject GameObject { get; }
        Entity Entity { get; }
        void BindEntity(Entity entity);
        void StartMove(float velocity);
        void StartRotate(float angularVelocity);
    }
}
