using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using wawa.Enums;

namespace wawa.Tiles
{
    public partial class Dirt : Tile
    {

        public override void SetDefaults()
        {
            displayName = "Dirt";
            sourceId = 1;
            atlasCoords = new Vector2I(0, 1);
            connects = true;
            connectType = ConnectionType.OneTile;
            itemDrop = new Vector2I(0, 1);
            minMiningPower = 15;
            sound = 0;
        }
    }
}
