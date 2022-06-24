using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDronsEngine
{
    abstract class GameObject
    {
        int xpos = 0, ypos = 0;

        public virtual void Spawn()
        {

        }

        public virtual void Destruct()
        {

        }
    }
}
