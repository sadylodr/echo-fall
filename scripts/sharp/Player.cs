using Godot;

public partial class Player : Node2D
{
	[Export] public float Speed = 200.0f; // Скорость перемещения игрока
	
	private AnimatedSprite2D _sprite;
	private Vector2 _targetPosition = Vector2.Zero; // Целевая позиция для перемещения
	private bool _isMoving = false; // Флаг, указывающий, движется ли игрок

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_targetPosition = GlobalPosition;
		_sprite.Stop();
	}

	public override void _Process(double delta)
	{
		if (_isMoving)
		{
			MoveTowardsTarget((float)delta);
		}
	}

	// Метод для начала перемещения к новой позиции
	public void MoveTo(Vector2 position)
	{
		_targetPosition = position;
		_isMoving = true;
		_sprite.Play("default");
	}

	// Метод для постепенного перемещения игрока
	private void MoveTowardsTarget(float delta)
	{
		Vector2 direction = (_targetPosition - GlobalPosition).Normalized();
		float distance = GlobalPosition.DistanceTo(_targetPosition);

		if (distance > Speed * delta)
		{
			GlobalPosition += direction * Speed * delta;
		}
		else
		{
			// Устанавливаем точную позицию, чтобы избежать дрожания
			GlobalPosition = _targetPosition;
			_isMoving = false;
			_sprite.Stop();
		}
	}
}
