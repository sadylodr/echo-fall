using Godot;

public partial class Platform : Area2D
{
	[Export] public bool IsReal = true;
	
	[Signal] public delegate void PlatformClickedEventHandler(bool isReal, Vector2 position);
	
	private Sprite2D _sprite; // Ссылка на Sprite2D
	private Texture2D _normalTexture; // Обычная текстура
	private Texture2D _hoverTexture; // Текстура при наведении

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");

		// Загружаем текстуры из файлов
		_normalTexture = GD.Load<Texture2D>("res://assets/platform/platform_test.png");
		_hoverTexture = GD.Load<Texture2D>("res://assets/platform/platform_test_hover.png");

		if (_normalTexture != null)
		{
			_sprite.Texture = _normalTexture;
		}

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		
		InputEvent += OnInputEvent;
	}

	private void OnMouseEntered()
	{
		if (_hoverTexture != null)
		{
			_sprite.Texture = _hoverTexture;
		}
	}

	private void OnMouseExited()
	{
		if (_normalTexture != null)
		{
			_sprite.Texture = _normalTexture;
		}
	}
	
	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			EmitSignal(nameof(PlatformClicked), IsReal, GlobalPosition);
		}
	}
}
