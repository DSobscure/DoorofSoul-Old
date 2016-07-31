using UnityEngine;

namespace DoorofSoul.Library.General.IControllers
{
    public interface IEntityController
    {
        GameObject GameObject { get; }
        Entity Entity { get; }
        void BindEntity(Entity entity);
        void StartMove(int direction);
        void StartRotate(int direction);
    }
}
