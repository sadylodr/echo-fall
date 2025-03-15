using Godot;
using System;

public partial class platform_handler : Sprite2D
{
	 public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			Vector2 mousePos = GetGlobalMousePosition();
			if (GetRect().HasPoint(ToLocal(mousePos)))
			{
				GD.Print("Спрайт нажат!");
			}
		}
	}
}
