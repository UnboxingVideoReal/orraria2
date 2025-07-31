using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Hotbar : Control
{
    public static Player player;
    public static Vector2I[] inventory;
    public static int[] invstack = [0,0,0,0,0,0];
    [Export]
    public int selectedslot = 0;
    public static Array<Node> hotbar;

    public override void _Ready()
    {
        player = GetTree().Root.GetNode<Player>("Node2D/player");
        inventory = player.inv_entory;
        hotbar = player.GetNode<Control>("Hotbar").GetNode<VBoxContainer>("VBoxContainer").GetChildren();
/*        getitem(World.dirt, 1);
*/        GD.Print(inventory.Stringify());
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("slot1"))
        {
            selectedslot = 0;
        }
        else if (Input.IsActionJustPressed("slot2"))
        {
            selectedslot = 1;
        }
        else if (Input.IsActionJustPressed("slot3"))
        {
            selectedslot = 2;
        }
        else if (Input.IsActionJustPressed("slot4"))
        {
            selectedslot = 3;
        }
        else if (Input.IsActionJustPressed("slot5"))
        {
            selectedslot = 4;
        }
        else if (Input.IsActionJustPressed("slot6"))
        {
            selectedslot = 5;
        }
        for (int i = 0; i < 6; i++)
        {
            if (i == selectedslot)
            {
                hotbar[i].GetNode<Sprite2D>("ho").Frame = 1;
            }
            else
            {
                hotbar[i].GetNode<Sprite2D>("ho").Frame = 0;
            }
            if (inventory[i] == new Vector2I(-1,-1))
            {
                hotbar[i].GetNode<Label>("Label").Text = "0";
                hotbar[i].GetNode<Sprite2D>("Sprite2D").Visible = false;
                hotbar[i].GetNode<Label>("Label").Visible = false;
            }
            else
            {
                hotbar[i].GetNode<Label>("Label").Text = invstack[i].ToString();
                hotbar[i].GetNode<Sprite2D>("Sprite2D").FrameCoords = inventory[i];
                hotbar[i].GetNode<Sprite2D>("Sprite2D").Visible = true;
                hotbar[i].GetNode<Label>("Label").Visible = true;


            }
        }

    }

    public void getitem(Vector2I item, int itemstack)
    {
        if (item == new Vector2I(-1,-1))
        {
            return;
        }
        for (int slot = 0;  slot < 6; slot++)
        {
            if (inventory.Contains(item))
            {
                for (int slot2 = 0; slot2 < 6; slot2++)
                {
                    if ((inventory[slot2] == item) && invstack[slot2] < getitemstack(item))
                    {
                        if (invstack[slot2] < getitemstack(item))
                        {
                            invstack[slot2] += itemstack;
                            return;
                        }
                    }
                }
            }
            if ((inventory[slot] == new Vector2I(-1,-1) && invstack[slot] == 0))
            {
                inventory[slot] = item;
                invstack[slot] += 1;
                return;
            }
        }

    }
    public void removeitem(int hotbarslot)
    {
        invstack[hotbarslot] -= 1;
        if (invstack[hotbarslot] <= 0)
        {
            inventory[hotbarslot] = new Vector2I(-1, -1);
            invstack[hotbarslot] = 0;
        }
    }
    public int getitemstack(Vector2I item)
    {
        return 999;
    }
}