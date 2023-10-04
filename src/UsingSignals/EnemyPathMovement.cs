using Godot;
using System;

public partial class EnemyPathMovement : Node2D
{
	private PathFollow2D _pathFollow;
	private Path2D _path;

	[Export]
	public float Speed { get; set; } = 190f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_pathFollow = GetParent<PathFollow2D>();
		_path = _pathFollow.GetParent<Path2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Curve2D curve = _path.Curve;
		_pathFollow.Progress += (float)(Speed * delta);
	}
}
