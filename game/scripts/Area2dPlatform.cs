using Godot;
using System;

public partial class Area2dPlatform : Area2D
{
	private Sprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D"); // Получаем спрайт
		Connect("mouse_entered", new Callable(this, nameof(OnMouseEnter)));
		Connect("mouse_exited", new Callable(this, nameof(OnMouseExit)));
	}

	private void OnMouseEnter()
	{
		GD.Print("Enter");
		sprite.Texture = GD.Load<Texture2D>("res://assets//platform//platform_test_hover.png");
	}

	private void OnMouseExit()
	{
		GD.Print("Leave");
		sprite.Texture = GD.Load<Texture2D>("res://assets//platform//platform_test.png");
	}
}
