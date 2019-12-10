using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    [Serializable]
    public class Unarmed : Weapon
    {
        public Unarmed(int x, int y) : base(x, y, 0, 0)
        {
            this.range = 35;
            this.damage = 10;
        }
    }
}
