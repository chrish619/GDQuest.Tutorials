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
		set
		{
			StraightenPoints(_straighten = value);
			if (_straighten && _smooth) { _smooth = false; }
			NotifyPropertyListChanged();
		}
	}

	[Export]
	public bool Smooth
	{
		get => _smooth;
		set
		{
			SmoothPoints(_smooth = value);
			if (_smooth && _straighten) { _straighten = false; }
			NotifyPropertyListChanged();
		}
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

		int lim = Curve.PointCount - 1;

		for (int i = 0; i <= lim; i++)
		{
			var prevPoint = Curve.GetPointPosition(i > 0 ? i - 1 : 0);
			var nextPoint = Curve.GetPointPosition(i < lim ? i + 1 : lim);
			var spline = prevPoint.DirectionTo(nextPoint) * SplineLength;

			Curve.SetPointIn(i, i > 0 ? -spline : new Vector2());
			Curve.SetPointOut(i, i < lim ? spline : new Vector2());
		}
	}
}
