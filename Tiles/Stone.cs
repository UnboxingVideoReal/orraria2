using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using wawa.Enums;

namespace wawa.Tiles
{
    public partial class Stone : Tile
    {
        public override void SetDefaults()
        {
            displayName = "Stone";
            sourceId = 1;
            atlasCoords = new Vector2I(1, 1);
            connects = true;
            connectType = ConnectionType.OneTile;
            itemDrop = new Vector2I(1, 1);
            minMiningPower = 30;
            sound = 1;
        }
    }
}
