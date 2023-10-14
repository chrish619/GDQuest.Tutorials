using Godot;
using System;
using System.Diagnostics;

public partial class PlayerController : Node2D
{
	[Signal]
	public delegate void HealthChangedEventHandler(int currentHealth);

	public event HealthChangedEventHandler HealthChanged;

	private AnimatedSprite2D _animatedSprite;
	private Timer _damageTimer;
	private Area2D _area;

	[Export]
	public StringName DamagedAnimationName { get; set; } = "damaged";
	[Export]
	public StringName DefaultAnimationName { get; set; } = "default";

	[Export]
	public int MaxPlayerHealth { get; set; }
	public int PlayerHealth { get; private set; }

	[Export]
	public ProgressBar LifeBar { get; set; }

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		PlayerHealth = MaxPlayerHealth;

		_area = GetNode<Area2D>("PlayerArea");
		_animatedSprite = _area.GetNode<AnimatedSprite2D>("PlayerAnimation");
		_damageTimer = GetNode<Timer>("DamageTimer");

		_animatedSprite.Animation = DefaultAnimationName;

		UpdateLifeBar();

		_area.AreaEntered += Player_AreaEntered;
		_damageTimer.Timeout += () => DamageTimer_Timeout(_damageTimer, EventArgs.Empty);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void Player_AreaEntered(Area2D area)
	{
		EnemyMeta enemyMeta = area.GetNode<EnemyMeta>("EnemyMeta");

		if (enemyMeta is not null && _damageTimer.IsStopped())
		{
			int collisionDamage = enemyMeta.CollisionDamage;

			TakeDamage(collisionDamage);
		}
	}

	private void TakeDamage(int damageTaken)
	{
		PlayerHealth -= damageTaken;

		_animatedSprite.Play(DamagedAnimationName);

		_damageTimer.OneShot = true;
		_damageTimer.Start();

		HealthChanged?.Invoke(new HealthChangedEventArgs()
		{
			MaxPlayerHealth = MaxPlayerHealth,
			PlayerHealth = PlayerHealth,
		});

		UpdateLifeBar();
	}

	private void DamageTimer_Timeout(object sender, EventArgs eventArgs)
	{
		_animatedSprite.Play(DefaultAnimationName);
	}

	private void UpdateLifeBar()
	{
		Debug.WriteLine($"Player Health now : {PlayerHealth}/{MaxPlayerHealth}");
		if (LifeBar is not null)
		{
			LifeBar.MaxValue = MaxPlayerHealth;
			LifeBar.Value = PlayerHealth;
		}
	}
}
