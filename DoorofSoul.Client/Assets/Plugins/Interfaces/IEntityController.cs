using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DoorofSoul.Client.Library.General;

namespace DoorofSoul.Client.Interfaces
{
    public interface IEntityController
    {
        GameObject GameObject { get; }
        ClientEntity Entity { get; }
        void BindEntity(ClientEntity entity);
    }
}
