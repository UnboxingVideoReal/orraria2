using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using wawa;
using wawa.Enums;
using wawa.Tiles;

/// <summary> 
/// idk like a tile 
/// </summary>>
public abstract partial class Tile : TileMapLayer
{
    private static Chat chat => Chat.Instance;
    public virtual void SetDefaults()
    {

    }
    public string displayName { get; set; }

    public int sourceId { get; set; }

    public bool isRandom { get; set; }

    public Vector2I[] randomCoords { get; set; }

    public Vector2I atlasCoords { get; set; }

    public bool connects { get; set; }

    public ConnectionType connectType { get; set; }

    public int connectOffset { get; set; }

    public List<Vector2I> connectionCoords = [new Vector2I(0, 0)];

    /// <summary>
    /// DO NOT CHANGE
    /// </summary>
    public bool yep = false;
    public void MakeConnectionArray()
    {
        int connectoffsetnew = (connectOffset == 0) ? 0 : connectOffset * 12;

        List<Vector2I> coords;

        if ((int)connectType == 1)
        {
            coords = new List<Vector2I>
            {
                atlasCoords
            };
        }
        else if ((int)connectType == 61)
        {
            coords = new List<Vector2I>
            {
                new Vector2I(connectoffsetnew,0),
                new Vector2I(connectoffsetnew+1,0),
                new Vector2I(connectoffsetnew+2,0),
                new Vector2I(connectoffsetnew+3,0),
                new Vector2I(connectoffsetnew+4,0),
                new Vector2I(connectoffsetnew+5,0),
                new Vector2I(connectoffsetnew+6,0),
                new Vector2I(connectoffsetnew+7,0),
                new Vector2I(connectoffsetnew+8,0),
                new Vector2I(connectoffsetnew+9,0),
                new Vector2I(connectoffsetnew+10,0),

                new Vector2I(connectoffsetnew,1),
                new Vector2I(connectoffsetnew+1,1),
                new Vector2I(connectoffsetnew+2,1),
                new Vector2I(connectoffsetnew+3,1),
                new Vector2I(connectoffsetnew+4,1),
                new Vector2I(connectoffsetnew+5,1),
                new Vector2I(connectoffsetnew+6,1),
                new Vector2I(connectoffsetnew+7,1),
                new Vector2I(connectoffsetnew+8,1),
                new Vector2I(connectoffsetnew+9,1),
                new Vector2I(connectoffsetnew+10,1),

                new Vector2I(connectoffsetnew,2),
                new Vector2I(connectoffsetnew+1,2),
                new Vector2I(connectoffsetnew+2,2),
                new Vector2I(connectoffsetnew+3,2),
                new Vector2I(connectoffsetnew+4,2),
                new Vector2I(connectoffsetnew+5,2),
                new Vector2I(connectoffsetnew+6,2),
                new Vector2I(connectoffsetnew+7,2),
                new Vector2I(connectoffsetnew+8,2),
                new Vector2I(connectoffsetnew+9,2),
                new Vector2I(connectoffsetnew+10,2),
                new Vector2I(connectoffsetnew+11,2),

                new Vector2I(connectoffsetnew,3),
                new Vector2I(connectoffsetnew+1,3),
                new Vector2I(connectoffsetnew+3,3),
                new Vector2I(connectoffsetnew+4,3),
                new Vector2I(connectoffsetnew+5,3),
                new Vector2I(connectoffsetnew+6,3),
                new Vector2I(connectoffsetnew+7,3),
                new Vector2I(connectoffsetnew+8,3),
                new Vector2I(connectoffsetnew+9,3),
                new Vector2I(connectoffsetnew+10,3),
                new Vector2I(connectoffsetnew+11,3),

                new Vector2I(connectoffsetnew,4),
                new Vector2I(connectoffsetnew+1,4),
                new Vector2I(connectoffsetnew+4,4),
                new Vector2I(connectoffsetnew+5,4),
                new Vector2I(connectoffsetnew+6,4),
                new Vector2I(connectoffsetnew+7,4),
                new Vector2I(connectoffsetnew+8,4),
                new Vector2I(connectoffsetnew+9,4),
                new Vector2I(connectoffsetnew+10,4),
                new Vector2I(connectoffsetnew+11,4),

                new Vector2I(connectoffsetnew+6,5),
                new Vector2I(connectoffsetnew+7,5),
                new Vector2I(connectoffsetnew+8,5),
                new Vector2I(connectoffsetnew+9,5),
                new Vector2I(connectoffsetnew+10,5),
                new Vector2I(connectoffsetnew+11,5),
            };
        }
        else
        {
            coords = new List<Vector2I>
            {
                new Vector2I(connectoffsetnew,0),
                new Vector2I(connectoffsetnew+1,0),
                new Vector2I(connectoffsetnew+2,0),
                new Vector2I(connectoffsetnew+3,0),
                new Vector2I(connectoffsetnew+4,0),
                new Vector2I(connectoffsetnew+5,0),
                new Vector2I(connectoffsetnew+6,0),
                new Vector2I(connectoffsetnew+7,0),
                new Vector2I(connectoffsetnew+8,0),
                new Vector2I(connectoffsetnew+9,0),
                new Vector2I(connectoffsetnew+10,0),

                new Vector2I(connectoffsetnew,1),
                new Vector2I(connectoffsetnew+1,1),
                new Vector2I(connectoffsetnew+2,1),
                new Vector2I(connectoffsetnew+3,1),
                new Vector2I(connectoffsetnew+4,1),
                new Vector2I(connectoffsetnew+5,1),
                new Vector2I(connectoffsetnew+6,1),
                new Vector2I(connectoffsetnew+7,1),
                new Vector2I(connectoffsetnew+8,1),
                new Vector2I(connectoffsetnew+9,1),
                new Vector2I(connectoffsetnew+10,1),

                new Vector2I(connectoffsetnew,2),
                new Vector2I(connectoffsetnew+1,2),
                new Vector2I(connectoffsetnew+2,2),
                new Vector2I(connectoffsetnew+3,2),
                new Vector2I(connectoffsetnew+4,2),
                new Vector2I(connectoffsetnew+5,2),
                new Vector2I(connectoffsetnew+6,2),
                new Vector2I(connectoffsetnew+7,2),
                new Vector2I(connectoffsetnew+8,2),
                new Vector2I(connectoffsetnew+9,2),
                new Vector2I(connectoffsetnew+10,2),
                new Vector2I(connectoffsetnew+11,2),

                new Vector2I(connectoffsetnew,3),
                new Vector2I(connectoffsetnew+1,3),
                new Vector2I(connectoffsetnew+3,2),
                new Vector2I(connectoffsetnew+4,3),
                new Vector2I(connectoffsetnew+5,3),
                new Vector2I(connectoffsetnew+6,3),
                new Vector2I(connectoffsetnew+7,3),
                new Vector2I(connectoffsetnew+8,3),
                new Vector2I(connectoffsetnew+9,3),
                new Vector2I(connectoffsetnew+10,3),
                new Vector2I(connectoffsetnew+11,3),

                new Vector2I(connectoffsetnew,4),
                new Vector2I(connectoffsetnew+1,4),
                new Vector2I(connectoffsetnew+4,4),
                new Vector2I(connectoffsetnew+5,4),
                new Vector2I(connectoffsetnew+6,4),
                new Vector2I(connectoffsetnew+7,4),
                new Vector2I(connectoffsetnew+8,4),
                new Vector2I(connectoffsetnew+9,4),
                new Vector2I(connectoffsetnew+10,4),
                new Vector2I(connectoffsetnew+11,4),

                new Vector2I(connectoffsetnew+6,5),
                new Vector2I(connectoffsetnew+7,5),
                new Vector2I(connectoffsetnew+8,5),
                new Vector2I(connectoffsetnew+9,5),
                new Vector2I(connectoffsetnew+10,5),
                new Vector2I(connectoffsetnew+11,5),
            };
        }

        connectionCoords = coords;
        RivuletUtils.Log($"{string.Join(", ", connectionCoords)}");
        yep = true;
    }

    public Vector2I itemDrop { get; set; }

    public float minMiningPower { get; set; }

    public int sound {  get; set; }

    public virtual void OnStep()
    {

    }

    public virtual void OnFall()
    {

    }

    public virtual void OnBreak()
    {

    }

    public virtual void OnMine()
    {

    }

    public virtual void LeftClick()
    {

    }

    public virtual void RightClick()
    {

    }

    public virtual void OnPlace()
    {

    }

    public virtual void Update()
    {

    }

/*    public Tile()
    {
        SetDefaults();
        MakeConnectionArray();

        TileRegistry.RegisterTile(this);
    }*/
}
