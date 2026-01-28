using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wawa.Tiles
{
    public static class TileRegistry
    {
        public static readonly Dirt Dirt = new Dirt();
        public static readonly Grass Grass = new Grass();
        public static readonly Stone Stone = new Stone();
        public static readonly Deepslate Deepslate = new Deepslate();
        public static readonly Grasses Grasses = new Grasses();


        public static Dictionary<(Vector2I coord, int sourceid), Tile> AtlasLookup = new();

        static TileRegistry()
        {
            RegisterTile(Dirt);
            RegisterTile(Grass);
            RegisterTile(Stone);
            RegisterTile(Deepslate);
            RegisterTile(Grasses);

        }


        public static void RegisterTile(Tile tile)
        {
            tile.SetDefaults();
            tile.MakeConnectionArray();
            if (tile.connects == true)
            {
                foreach (var coord in tile.connectionCoords)
                {
                    AtlasLookup[(coord, tile.sourceId)] = tile;
                }
            }
            if (tile.isRandom == true)
            {
                foreach (var coord in tile.randomCoords)
                {
                    AtlasLookup[(coord, tile.sourceId)] = tile;
                }
            }
            else
            {
                AtlasLookup[(tile.atlasCoords, tile.sourceId)] = tile;
            }
        }

        public static Tile GetTileFromAtlasCoord(Vector2I coord, int sourceid)
        {
            return AtlasLookup.TryGetValue((coord, sourceid), out var tile) ? tile : null;
        }
    }
}
