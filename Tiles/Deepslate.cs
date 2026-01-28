using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace wawa.Tiles
{
    public partial class Deepslate : Tile
    {
        public override void SetDefaults()
        {
            displayName = "Deepslate";
            sourceId = 1;
            atlasCoords = new Vector2I(2, 0);
            connects = false;
            // connectType = ConnectionTypes.1Tile;
            // connectOffset = 2;
            itemDrop = new Vector2I(2, 0);
            minMiningPower = 60;
            sound = 2;
        }
    }
}
