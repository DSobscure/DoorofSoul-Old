using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.LightComponents
{
    public interface LibraryHexagramInterface
    {
        KnowledgeInterface KnowledgeInterface { get; }
        ElementInterface ElementInterface { get; }
        NatureInterface NatureInterface { get; }
    }
}
