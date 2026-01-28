using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace wawa.Tiles
{
    public partial class Grasses : Tile
    {
        public override void SetDefaults()
        {
            displayName = "Grasses";
            sourceId = 1;
            isRandom = true;
            randomCoords = [new Vector2I(0, 4), new Vector2I(1, 4), new Vector2I(0, 5), new Vector2I(1, 5)];
            itemDrop = new Vector2I(0, 4);
            minMiningPower = 0;
            sound = 3;
        }
    }
}
