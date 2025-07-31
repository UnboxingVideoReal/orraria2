using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using wawa.Enums;

namespace wawa.Tiles
{
    public partial class Grass : Tile
    {

        public override void SetDefaults()
        {
            displayName = "Grass";
            sourceId = 2;
            connects = true;
            connectType = ConnectionType.SixtyOneTiles;
            connectOffset = 0;
            itemDrop = new Vector2I(1, 0);
            minMiningPower = 15;
            sound = 0;
        }
    }
}
