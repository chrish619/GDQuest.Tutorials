using Godot;
using System;

public partial class EnemyPathFollow : PathFollow2D
{
	private Path2D _path;

	[Export]
	public float Speed { get; set; } = 190f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_path = this.GetParent<Path2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Curve2D curve = _path.Curve;
		Progress += (float)(Speed * delta);
	}
}
