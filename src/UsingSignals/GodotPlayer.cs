using Godot;
using System;

public partial class GodotPlayer : Sprite2D
{
	public float AngularSpeed { get; } = Mathf.Pi;
	public float Speed { get; } = 300;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Button button = GetParent().GetNode<Button>("ClickMe");
		button.Pressed += () => Button_Pressed(button, EventArgs.Empty);

		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => Timer_Timeout(timer, EventArgs.Empty);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotation += (float)(AngularSpeed * delta);
		Position += Vector2.Up.Rotated(Rotation) * (float)(Speed * delta);
	}

	private void Timer_Timeout(object sender, EventArgs e)
	{
		Visible = !Visible;
	}

	private void Button_Pressed(object sender, EventArgs e)
	{
		SetProcess(!IsProcessing());
		// Replace with function body.
	}
}
