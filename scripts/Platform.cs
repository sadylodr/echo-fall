using Godot;

public partial class Platform : Area2D
{
	[Export] public bool IsReal = true;
	
	[Signal] public delegate void PlatformClickedEventHandler(bool isReal, Vector2 position);
	
	private AnimatedSprite2D _sprite; // Ссылка на Sprite2D

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		//_sprite.Play("default"); // Запускаем анимацию по умолчанию
		_sprite.Frame = 0; // Устанавливаем первый кадр
		
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		
		InputEvent += OnInputEvent;
	}

	private void OnMouseEntered()
	{
		GD.Print("Enter");
	}

	private void OnMouseExited()
	{
		GD.Print("Exited");
	}
	
	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (!IsInstanceValid(this) || !Monitorable) // Проверяем, активна ли платформа
		{
			return;
		}
		
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			EmitSignal(nameof(PlatformClicked), IsReal, GlobalPosition);
		}
	}
	public async void FadeOutAndRemove()
	{
		_sprite.Stop();
		_sprite.Frame = 0;
		
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		
		_sprite.Play("default");
		
		await ToSignal(_sprite, AnimatedSprite2D.SignalName.AnimationFinished); 
		
		Monitorable = false;
		InputEvent -= OnInputEvent;
	}
}
