using Godot;
using System;

public partial class Player : Sprite2D
{
	public float TurnRate { get; } = Godot.Mathf.Pi;
	public float Speed { get; } = 88f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int turning = 0;
		int forward = 0;

		if (Input.IsActionPressed("ui_left"))
		{
			turning = -1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			turning = 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			forward = 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			forward = -1;
		}

		float rotate = turning * (float)(TurnRate * delta);
		Rotation += rotate;
		Position += Vector2.Up.Rotated(Rotation) * (float)(forward * Speed * delta);
	}
}
