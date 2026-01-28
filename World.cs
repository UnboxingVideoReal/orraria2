using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using wawa;
using wawa.Tiles;


public partial class World : TileMapLayer
{
    BetterTerrain bt;
    [Export]
    public FastNoiseLite noisething = new FastNoiseLite();
    [Export]
    public FastNoiseLite noisething2 = new FastNoiseLite();
    [Export]
    public FastNoiseLite surfacenoise = new FastNoiseLite();
    [Export]
    public FastNoiseLite hillnoise = new FastNoiseLite();
    public const int worldwidth = 500;
    public const int worldheight = 400;
    public static int SEED = 113/*(int)GD.Randi()*/;
    private int task = 0;
    int dirtlayer = worldheight / 2 + 1;
    double rocklayer = worldheight / 2.05 + 1;
    double deepslatelayer = worldheight / 1.333333 + 1;
    private bool wawa = true;

    private List<Vector2I> surfacegrass = [];

    private Hotbar hotbar;

    // tiles
    public static Vector2I air = new Vector2I(-1, -1);
    public static List<Vector2I> grass = TileRegistry.Grass.connectionCoords;
    public static Vector2I dirt = TileRegistry.Dirt.connectionCoords[0];
    public static Vector2I stone = TileRegistry.Stone.connectionCoords[0];
    public static Vector2I deepslate = new Vector2I(2, 0);

    public static Vector2I[] grasses = TileRegistry.Grasses.randomCoords;


    public static Vector2I coal = new Vector2I(2, 1);
    public static Vector2I deepslatecoal = new Vector2I(3, 1);
    public static Vector2I coalstructurehelper = new Vector2I(2, 2);

    public static Vector2I obsidian = new Vector2I(3, 0);

    public static Vector2I woodplanks = new Vector2I(7, 0);
    public static Vector2I woodpillar = new Vector2I(6, 0);
    public static Vector2I[] stonebricks = [new Vector2I(8, 0), new Vector2I(8, 2), new Vector2I(9, 2)];
    public static Vector2I chiseledtunnelstonebricks = new Vector2I(9, 0);
    public static Vector2I[] stonebrickstairs = [new Vector2I(10, 0), new Vector2I(10, 1), new Vector2I(11, 0), new Vector2I(11, 1)];


    public static Vector2I woodplankwall = new Vector2I(7, 1);
    public static Vector2I woodpillarwall = new Vector2I(6, 1);
    public static Vector2I[] stonebrickwall = [new Vector2I(8, 3), new Vector2I(9, 3), new Vector2I(10, 3)];
    public static Vector2I chiseledtunnelstonebrickwall = new Vector2I(9, 0);

    public static Vector2I eyeoftin = new Vector2I(0, 0);
    public static Vector2I mediumgreyblock = new Vector2I(4, 0);

    public static Vector2I mediumgreywall = new Vector2I(4, 1);

    public static Vector2I RIVULET = new Vector2I(5, 0);


    public Vector2I[] tiles = [
        air,
        grass[0],
        grass[1],
        grass[2],
        grass[3],
        grass[4],
        grass[5],
        grass[6],
        grass[7],
        grass[8],
        grass[9],
        grass[10],
        grass[11],
        grass[12],
        grass[13],
        grass[14],
        grass[15],
        grass[16],
        grass[17],
        grass[18],
        grass[19],
        grass[20],
        grass[21],
        grass[22],
        grass[23],
        grass[24],
        grass[25],
        grass[26],
        grass[27],
        grass[28],
        grass[29],
        grass[30],
        grass[31],
        grass[32],
        grass[33],
        grass[34],
        grass[35],
        grass[36],
        grass[37],
        grass[38],
        grass[39],
        grass[40],
        grass[41],
        grass[42],
        grass[43],
        grass[44],
        grass[45],
        grass[46],
        grass[47],
        grass[48],
        grass[49],
        grass[50],
        grass[51],
        grass[52],
        grass[53],
        grass[54],
        grass[55],
        grass[56],
        grass[57],
        grass[58],
        grass[59],
        grass[60],

        dirt,
        stone,
        deepslate,

        grasses[0],
        grasses[1],
        grasses[2],
        grasses[3],

        coal,
        deepslatecoal,
        coalstructurehelper,
        obsidian,

        woodplanks,
        woodpillar,
        stonebricks[0],
        stonebricks[1],
        stonebricks[2],
        chiseledtunnelstonebricks,
        stonebrickstairs[0],
        stonebrickstairs[1],
        stonebrickstairs[2],
        stonebrickstairs[3],

        woodplankwall,
        woodpillarwall,
        stonebrickwall[0],
        stonebrickwall[1],
        stonebrickwall[2],
        chiseledtunnelstonebrickwall,

        eyeoftin,
        mediumgreyblock,

        mediumgreywall,

        RIVULET
    ];

    private Player player;
    public int set = 150 / 2;

    public override void _Ready()
    {
        World tml = GetTree().Root.GetNode<World>("Node2D/World");
        bt = new BetterTerrain(tml);
        player = GetTree().Root.GetNode<Player>("Node2D/player");
        var rng = new RandomNumberGenerator();
        rng.Seed = (ulong)SEED;


        noisething.Seed = SEED;
        noisething.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
        noisething.Frequency = 0.02f;
        noisething.FractalType = FastNoiseLite.FractalTypeEnum.Ridged;
        noisething.FractalWeightedStrength = 0.3f;

        noisething2.Seed = SEED;
        noisething2.Frequency = 0.9f;
        noisething2.FractalType = FastNoiseLite.FractalTypeEnum.Ridged;

        surfacenoise.Seed = SEED;
        surfacenoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
        surfacenoise.Frequency = 0.01f;
        surfacenoise.FractalType = FastNoiseLite.FractalTypeEnum.None;

        hillnoise.Seed = SEED;
        hillnoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
        hillnoise.Frequency = 0.015f;
        hillnoise.FractalType = FastNoiseLite.FractalTypeEnum.None;

        /*        SetCell(new Vector2I(1, 1), 1, test);
                GD.Print("printed block");*/

        // PASS 1 -- AIR
        for (int x = 0; x < worldwidth; x++)
        {
            for (int y = 0; y < worldheight; y++)
            {
                task += 1;
                SetCell(new Vector2I(x - worldwidth / 2, y - worldheight / 2), 1, air);
            }
        }
        // PASS 2 -- TEST LANDSCAPE
        task = 0;
        /*        
                for (int x = 0; x < worldwidth; x++)
                {
                    for (int y = worldheight - worldheight / 4; y < worldheight; y++)
                    {
                        task += 1;
                        SetCell(new Vector2I(x - worldwidth / 2, y - worldheight / 2), 1, test);
                    }
                }
        */
        // PASS 2 -- LAYERS
        task = 0;
        for (int x = 0; x < worldwidth; x++)
        {
            var surfacen = surfacenoise.GetNoise1D(x + worldwidth) * 10;
            var hilln = hillnoise.GetNoise1D(x + worldwidth) * 13;
            set += rng.RandiRange(-1, 1);
            task = 0;
            dirtlayer = (int)(((worldheight / 2) + worldheight / 4) + hilln + surfacen);
            /*            if (surfacen > 1.3 && surfacen < 2.1)
                        {
                            dirtlayer = (int)(((worldheight / 2) + worldheight / 4) + surfacen * (0.5 * surfacen) + hilln);
                        }
                        if (surfacen > 2.1 && surfacen < 3)
                        {
                            dirtlayer = (int)(((worldheight / 2) + worldheight / 4) + surfacen * (1.0 * surfacen) + hilln);
                        }
                        if (surfacen > 3)
                */
            {
                dirtlayer = (int)(((worldheight / 2) + worldheight / 4) + surfacen * (1.1 * surfacen) + hilln);
            }
            for (int y = 0; y < worldheight; y++)
            {
                task += 1;
                if (task < rocklayer - 10 + dirtlayer - worldheight / 2)
                {
                    SetCell(new Vector2I(x - worldwidth / 2, -y + worldheight), 1, stone);
                }
                else if (task < dirtlayer)
                {
                    /*                    SetCell(new Vector2I(x - worldwidth / 2, -y + worldheight), 1, dirt);
                    */
                    bt.SetCell(new Vector2I(x - worldwidth / 2, -y + worldheight), 2);
                    /*                    bt.UpdateTerrainCell(new Vector2I(x - worldwidth / 2, -y + worldheight), true);
                    */
                }
                else if (task < dirtlayer + 1)
                {
                    /*                    SetCell(new Vector2I(x - worldwidth / 2, -y + worldheight), 1, dirt);
                    */
                    bt.SetCell(new Vector2I(x - worldwidth / 2, -y + worldheight), 1);
                    bt.UpdateTerrainCell(new Vector2I(x - worldwidth / 2, -y + worldheight), true);

                }
                if (task >= deepslatelayer)
                {
                    /*                        GD.Print((x - worldwidth / 2) + "," + (y + worldheight / 4));
                    */
                    SetCell(new Vector2I(x - worldwidth / 2, y + worldheight / 4 - worldheight / 4 + 1), 1, deepslate);
                }
            }
            task = 0;

            if (rng.RandiRange(0, 1) == 0)
            {
                rocklayer += rng.RandiRange(-2, 2);
            }
            if (rng.RandiRange(0, 1) == 0)
            {
                deepslatelayer += rng.RandiRange(-2, 2);
            }

            if (dirtlayer > (worldheight / 2) + 12)
            {
                dirtlayer = (worldheight / 2) + 12;
            }
            if (dirtlayer < (worldheight / 2) - 12)
            {
                dirtlayer = (worldheight / 2) - 12;
            }

            task = 0;

        }
        // PASS 3 - DEEPSLATE
        task = 0;
        /*        for (int x = 0; x < worldwidth; x++)
                {
                    task = 0;
                    for (int y = 0; y < worldheight; y++)
                    {
                        task += 1;
                        if (task >= deepslatelayer)
                        {
                            GD.Print((x - worldwidth / 2) + "," + (y + worldheight / 4));
                            SetCell(new Vector2I(x - worldwidth / 2, y + worldheight / 4 - worldheight / 4 + 1), 1, deepslate);
                        }
                    }
                    if (GD.RandRange(0, 1) == 0)
                    {
                        deepslatelayer += (int)rng.RandfRange(-2, 2);
                    }
                }*/

        // PASS 3 - CAVES
        task = 0;
        for (int x = 0; x < worldwidth; x++)
        {
            float taskf = 0;
            noisething.Frequency = 0.025f;
            for (int y = worldheight; y > -1; y--)
            {
                if (GetCellAtlasCoords(new Vector2I(x - worldwidth / 2, -y + worldheight)) == stone || GetCellAtlasCoords(new Vector2I(x - worldwidth / 2, -y + worldheight)) == deepslate)
                {
                    taskf += 0.00075f * (2000 / worldheight);
                    int worldx = x - worldwidth / 2;
                    int worldy = -y + worldheight;
                    var alti = noisething.GetNoise2D(worldx, worldy);
                    Vector2I atlas = stone;
                    /*                    GD.Print(alti);
                    */
                    if (alti >= taskf)
                    {
                        if (GetCellAtlasCoords(new Vector2I(worldx, worldy)) == stone && GetCellSourceId(new Vector2I(worldx, worldy)) == 1)
                        {
                            atlas = stone;
                        }
                        else if (GetCellAtlasCoords(new Vector2I(worldx, worldy)) == deepslate && GetCellSourceId(new Vector2I(worldx, worldy)) == 1)
                        {
                            atlas = deepslate;
                        }
                    }
                    else
                    {
                        atlas = air;
                    }
                    if (GetCellSourceId(new Vector2I(worldx, worldy)) == 1)
                    {
                        SetCell(new Vector2I(worldx, worldy), 1, atlas);
                    }


                }
            }
        }
        // PASS 4 - ORE STRUCTURE HELPERS
        // ORE 1 - COAL
        /*        for (int x = 0; x < worldwidth; x++)
                {
                    for (int y = 0; y < worldheight; y++)
                    {
                        if (GetCellAtlasCoords(new Vector2I(x - worldwidth / 2, -y + worldheight)) == stone)
                        {
                            if (GD.RandRange(1, 1) == 1)
                            {
                                SetCell(new Vector2I(x - worldwidth / 2, -y + worldheight), 1, coalstructurehelper);
                            }
                        }
                    }
                }*/
        // PASS 5 - ORE VEINS
        // ORE 1 - COAL
        /*        for (int x = 0; x < worldwidth; x++)
                {
                    for (int y = 0; y < worldheight; y++)
                    {
                        if (GetCellAtlasCoords(new Vector2I(x - worldwidth / 2, -y + worldheight)) == coalstructurehelper)
                        {
                            int worldx = x - worldwidth / 2;
                            int worldy = -y + worldheight;
                            var alti = noisething.GetNoise2D(worldx, worldy);
                            Vector2I atlas = stone;
                            GD.Print(alti);
                            if (alti < 0.33)
                            {
                                atlas = stone;
                            }
                            else
                            {
                                atlas = air;
                            }
                            SetCell(new Vector2I(worldx, worldy), 1, atlas);


                        }
                    }
                }
        */
        // PASS 6 - ADD GRASS SURFACE LIST
        for (int x = 0; x < worldwidth; x++)
        {
            for (int y = 0; y < worldheight; y++)
            {
                Godot.Collections.Array<Vector2I> getsurrounding = GetSurroundingCells(new Vector2I(x - worldwidth / 2, -y + worldheight));
                foreach (Vector2I cell in getsurrounding)
                {
                    bool wawa = grass.Contains(GetCellAtlasCoords(cell));
                    if (wawa && GetCellSourceId(cell) == 2 && !surfacegrass.Contains(cell))
                    {
                        surfacegrass.Add(cell);
                    }
                }
            }
        }
        // PASS 7 - GRASSES
/*        RivuletUtils.Log(RivuletUtils.ArrayListToString(surfacegrass, ", ", 300));
*/        
        task = 0;
        for (int x = 1; x < worldwidth; x++)
        {
            int newx = surfacegrass[x].X;
            int y = surfacegrass[x].Y;
            SetCell(new Vector2I(newx, y - 1), 1, grasses[rng.RandiRange(0, 3)]);
        }
        // PASS 8 - MORE GRASS
        for (int x = 0; x < worldwidth; x++)
        {
            for (int y = 0; y < worldheight; y++)
            {
                Vector2I selectedtile = new Vector2I(x - worldwidth / 2, -y + worldheight);
                if (GetCellAtlasCoords(selectedtile) == air)
                {
                    Vector2I[] getsurrounding = GetSurroundingTiles(new Vector2I(x - worldwidth / 2, -y + worldheight));
                    foreach (Vector2I cell in getsurrounding)
                    {
                        bool wawa = dirt == GetCellAtlasCoords(cell);
                        if (wawa && GetCellSourceId(cell) == 1)
                        {
                            bt.SetCell(cell, 1);
                            bt.UpdateTerrainCell(cell, true);
                        }
                    }
                }
            }
        }

        for (int i = 0; i < 50; i++)
        {
            var spawnpos = new Vector2I(0, (worldheight / 2 - worldheight / 4 - 25) * 16);


            player.Position = spawnpos;
        }
    }

    public override void _Process(double delta)
    {
        RichTextLabel positiontext = player.GetNode<RichTextLabel>("RichTextLabel");
        Vector2 hitboxpos = player.Position;
        positiontext.Text = $"{Math.Round((hitboxpos.X) / 16)}, {Math.Round((hitboxpos.Y) / 16)}\n{surfacenoise.GetNoise1D((float)(Math.Round((hitboxpos.X) / 16) + worldwidth)) * 10}".ToString();
        World tml = GetTree().Root.GetNode<World>("Node2D/World");
        bt = new BetterTerrain(tml);

        hotbar = player.GetNode<Hotbar>("Hotbar");
        if (Input.IsActionPressed("mine"))
        {
            var pos = LocalToMap(new Vector2I((int)GetGlobalMousePosition().X, (int)GetGlobalMousePosition().Y));
            Vector2I atlasCoord = GetCellAtlasCoords(pos);
            int sourceId = tml.GetCellSourceId(pos);

            Tile tile = TileRegistry.GetTileFromAtlasCoord(atlasCoord, sourceId);

            if (GetCellAtlasCoords(pos) == air)
            {

            }
            /*            else if (GetCellSourceId(pos) == 2 && new Rect2I(,0,12*16,6*16).HasPoint(GetCellAtlasCoords(pos)))
                        {
                            hotbar.getitem(grass, 1);
                        }
            */
            else/* if (GetCellSourceId(pos) == 1)*/
            {
                if (tile.connects == false)
                {
                    RivuletUtils.Log(tile.atlasCoords.ToString());
                }
                else if (tile.isRandom == true)
                {
                    RivuletUtils.Log(RivuletUtils.ArrayListToString(tile.randomCoords, "", 0));
                }
                else
                {
                    RivuletUtils.Log(RivuletUtils.ArrayListToString(tile.connectionCoords,"",0));
                }
                GD.Print(GetCellAtlasCoords(pos).ToString());
                hotbar.getitem(tile.itemDrop, 1);
            }
            SetCell(pos, 1, air);
            foreach (Vector2I position in GetSurroundingCells(pos))
            {
                bt.UpdateTerrainCell(position, true);
            }
        }
        if (Input.IsActionPressed("build"))
        {
            Vector2I pos = LocalToMap(new Vector2I((int)GetGlobalMousePosition().X, (int)GetGlobalMousePosition().Y));

            if (GetCellAtlasCoords(pos) == air)
            {
                if (player.inv_entory[hotbar.selectedslot] == air)
                {

                }
                else
                {
                    if (grass.Contains(player.inv_entory[hotbar.selectedslot]))
                    {
                        bt.SetCell(pos, 1);
                        bt.UpdateTerrainCell(pos, true);
                    }
                    else if (player.inv_entory[hotbar.selectedslot] == dirt)
                    {
                        bt.SetCell(pos, 2);
                        bt.UpdateTerrainCell(pos, true);
                    }
                    else
                    {
                        SetCell(pos, 1, player.inv_entory[hotbar.selectedslot]);
                    }
                    hotbar.removeitem(hotbar.selectedslot);
                }
            }
        }

        /* func get_surrounding_tiles(current_tile):
	var surrounding_tiles = []
	var target_tile

	for y in 3:
		for x in 3:
			target_tile = current_tile + Vector2(x - 1, y - 1)

			if current_tile == target_tile:
				continue

			surrounding_tiles.append(target_tile)

	return surrounding_tiles
*/
    }
    public Vector2I[] GetSurroundingTiles(Vector2I currenttile)
    {
        Vector2I[] surroundingtiles = [];
        Vector2I targettile;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                targettile = currenttile + new Vector2I(x - 1, y - 1);

                if (currenttile == targettile)
                {
                    continue;
                }

                surroundingtiles.Append(targettile);
            }
        }

        return surroundingtiles;
    }
}