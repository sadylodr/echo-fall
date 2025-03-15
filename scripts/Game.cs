using Godot;
using System.Collections.Generic;

public partial class Game : Node2D
{
	private Player _player;
	private List<Platform> _allPlatforms = new();
	private List<Platform> _currentPlatforms = new(); // Текущая пара платформ
	private int _currentStage = 0; // Текущий этап

	public override void _Ready()
	{
		_player = GetNode<Player>("Player");

		// Находим все платформы в сцене
		var platforms = GetTree().GetNodesInGroup("Platforms");
		foreach (var platform in platforms)
		{
			if (platform is Platform p)
			{
				_allPlatforms.Add(p);
				p.PlatformClicked += OnPlatformClicked;
			}
		}

		// Инициализируем первую пару платформ
		InitializeStage();
	}

	private void InitializeStage()
	{
		// Очищаем текущую пару платформ
		_currentPlatforms.Clear();

		for (int i = _currentStage * 2; i < (_currentStage + 1) * 2 && i < _allPlatforms.Count; i++)
		{
			var platform = _allPlatforms[i];
			if (platform != null && IsInstanceValid(platform))
			{
				_currentPlatforms.Add(platform);
			}
		}

		GD.Print($"Stage {_currentStage + 1} initialized with {_currentPlatforms.Count} platforms.");
	}

	private void OnPlatformClicked(bool isReal, Vector2 position)
	{
		if (!IsPlatformInCurrentStage(position))
		{
			GD.Print("Invalid platform selected!");
			return;
		}

		if (isReal)
		{
			GD.Print("Correct platform selected!");

			// Удаляем только ложную платформу из текущей пары
			foreach (var platform in _currentPlatforms)
			{
				if (!platform.IsReal)
				{
					platform.FadeOutAndRemove(); // Удаляем платформу из сцены
				}
			}

			// Переходим к следующей стадии
			_currentStage++;
			int totalPairs = _allPlatforms.Count / 2;

			if (_currentStage >= totalPairs)
			{
				GD.Print("Level completed!");
				// Здесь можно добавить логику завершения уровня
			}
			else
			{
				InitializeStage();
			}

			// Перемещаем игрока на выбранную платформу
			_player.MoveTo(position);
		}
		else
		{
			GD.Print("Wrong platform selected! Restarting level...");
			GetTree().ReloadCurrentScene(); // Перезапускаем уровень
		}
	}

	private bool IsPlatformInCurrentStage(Vector2 position)
	{
		foreach (var platform in _currentPlatforms)
		{
			if (platform.GlobalPosition == position)
			{
				return true;
			}
		}
		return false;
	}
}
