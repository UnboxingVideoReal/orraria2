using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Net.NetworkInformation;

public partial class Player : CharacterBody2D
{
    public float Speed = 130;
    public int jumpvel { get; set; } = -260;
    private AnimatedSprite2D _animatedSprite;
    private RichTextLabel positiontext;
    public Vector2 velocity = new Vector2(0, 0);
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public Vector2I[] inv_entory = [new Vector2I(-1, -1), new Vector2I(-1, -1), new Vector2I(-1, -1), new Vector2I(-1, -1), new Vector2I(-1, -1), new Vector2I(-1, -1)];
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }


    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
            _animatedSprite.Play("fall");
        }
        if (Input.IsActionPressed("jump") && IsOnFloor() && GetNode<Chat>("Chat").pressedenter == false)
        {
            velocity.Y = jumpvel;
        }
        if (Input.IsActionPressed("left") && GetNode<Chat>("Chat").pressedenter == false)
        {
            velocity.X -= Speed;
            if (IsOnFloor())
            {
                _animatedSprite.Play("walk");
            }
            _animatedSprite.FlipH = false;
            velocity.X = Math.Clamp(velocity.X, -Speed, Speed);

        }
        else if (Input.IsActionPressed("right") && GetNode<Chat>("Chat").pressedenter == false)
        {
            velocity.X += Speed;
            if (IsOnFloor())
            {
                _animatedSprite.Play("walk");
            }
            _animatedSprite.FlipH = true;
            velocity.X = Math.Clamp(velocity.X, -Speed, Speed);

        }
        else if (IsOnFloor() && _animatedSprite.Animation != "idle")
        {
            _animatedSprite.Play("idle");

        }
        velocity.X = Mathf.Lerp(velocity.X, 0, 0.3f);// Mathf.MoveToward(Velocity.X, 0, Speed);

        if (Input.IsActionJustPressed("zoom in"))
        {
            GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("hitbox").GetNode<Camera2D>("Camera2D").Zoom += new Vector2(0.25f, 0.25f);
        }
        else if (Input.IsActionJustPressed("zoom out"))
        {
            GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("hitbox").GetNode<Camera2D>("Camera2D").Zoom -= new Vector2(0.25f, 0.25f);
        }
        Velocity = velocity;
        MoveAndSlide();
    }
}
