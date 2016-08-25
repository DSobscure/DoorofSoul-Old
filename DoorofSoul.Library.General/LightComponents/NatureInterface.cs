using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Library.General.LightComponents.Nature;

namespace DoorofSoul.Library.General.LightComponents
{
    public interface NatureInterface
    {
        ContainerElementsInterface ContainerElementsInterface { get; }
        SceneElementsInterface SceneElementsInterface { get; }
    }
}
