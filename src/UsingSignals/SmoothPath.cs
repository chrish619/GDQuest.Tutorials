using Godot;
using System;

public partial class SmoothPath : Path2D
{
	private bool _straighten;
	private bool _smooth;

	[Export]
	public float SplineLength { get; set; } = 100f;

	[Export]
	public bool Straighten
	{
		get => _straighten;
		set => StraightenPoints(_straighten = value);
	}

	[Export]
	public bool Smooth
	{
		get => _smooth;
		set => SmoothPoints(_smooth = value);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void StraightenPoints(bool straighten)
	{
		if (!straighten)
		{
			return;
		}

		for (int i = 0; i < Curve.PointCount; i++)
		{
			Curve.SetPointIn(i, new Vector2());
			Curve.SetPointOut(i, new Vector2());
		}
	}

	private void SmoothPoints(bool smooth)
	{
		if (!smooth)
		{
			return;
		}

		int min = 0;
		int max = Curve.PointCount;

		for (int i = 0; i < max; i++)
		{
			var prevPoint = Curve.GetPointPosition(i <= min ? min : (i - 1));
			var nextPoint = Curve.GetPointPosition(i >= max ? max : (i + 1));
			var spline = prevPoint.DirectionTo(nextPoint) * SplineLength;

			Curve.SetPointIn(i, -spline);
			Curve.SetPointOut(i, spline);
		}
	}
}
