using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class Chat : Control
{
    public static Chat Instance { get; set; }
    private RichTextLabel chatlabel;
    private ShaderMaterial shader;
    public string labeltext = "<";
    public bool pressedenter = false;
    public string typing = "";
    public string typedmessages = "";
    public List<string> messagelist = new List<string>
    {
        
    };
    // "yea\n\n<"
    private List<string> messageBuffer = new(); 

    public static List<string> pendingMessages = new(); 



    public override void _Ready()
    {
        Instance = this;

        chatlabel = GetNode<RichTextLabel>("RichTextLabel");
        chatlabel.Text = labeltext;
        shader = (Material as ShaderMaterial);

        foreach (string msg in pendingMessages)
        {
            Send(msg, false);
        }
        pendingMessages.Clear();
        chatlabel.ScrollToLine(chatlabel.GetLineCount());
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("open chat"))
        {
            if (pressedenter == false)
            {
                pressedenter = true;
                shader.SetShaderParameter("alpha", 1f);
                chatlabel.ScrollToLine(chatlabel.GetLineCount());
            }
            else
            {
                pressedenter = false;
                Send(typing, true);
                chatlabel.ScrollToLine(chatlabel.GetLineCount());
                wawa();
            }
        }
    }

    public void wawa()
    {
        shader.SetShaderParameter("alpha", Mathf.Lerp(1f, 0f,0.5f));
    }

    public override void _Input(InputEvent @event)
    {
        if (pressedenter == true)
        {
            if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
            {
                char keyChar = (char)keyEvent.Unicode;

                if (!char.IsControl(keyChar))
                {
                    typing += keyChar;
                }
                else if (keyEvent.Keycode == Key.Backspace)
                {
                    typing = typing.Substring(0, typing.Length - 1);
                }
                GD.Print(typing);
                labeltext = string.Join("\n", messagelist) + "\n\n" + typing;
                chatlabel.Text = labeltext + "<";
            }
        }
    }

    public void Send(string what, bool typings)
    {
        if (typings == true)
        {
            messagelist.Add(typing);
            labeltext = string.Join("\n", messagelist) + $"\n";
            GD.Print(string.Join("\n", messagelist) + labeltext);
            chatlabel.Text = labeltext + "\n<";
            typing = "";
            GD.Print(string.Join("", messagelist));
            chatlabel.ScrollToLine(chatlabel.GetLineCount());
        }
        else
        {
            GD.Print(what);
            messagelist.Add(what + "\n");
            labeltext = string.Join("\n", messagelist) + $"\n";
            GD.Print(string.Join("\n", messagelist) + labeltext);
            chatlabel.Text = labeltext + "<";
        }
    }
}
