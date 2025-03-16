using Godot;
using System;

public partial class Menu : Control
{
	// Объявляем переменные для кнопок как private поля класса
private Godot.Button playButton;
private Godot.Button settingsButton;
private Godot.Button exitButton;

// Вызывается при загрузке узла
public override void _Ready()
{
	// Инициализация кнопок через их путь в дереве узлов
	playButton = GetNode<Godot.Button>("VBoxContainer/PlayButton");
	settingsButton = GetNode<Godot.Button>("VBoxContainer/SettingsButton");
	exitButton = GetNode<Godot.Button>("VBoxContainer/ExitButton");

	// Подключение сигналов к обработчикам
	playButton.Pressed += OnPlayPressed;
	settingsButton.Pressed += OnSettingsPressed;
	exitButton.Pressed += OnExitPressed;
}

// Обработчик нажатия на кнопку "Играть"
private void OnPlayPressed()
{
	// Меняем сцену на игровую
	GetTree().ChangeSceneToFile("res://scenes/game.tscn");
}

// Обработчик нажатия на кнопку "Настройки"
private void OnSettingsPressed()
{
	// Выводим сообщение в консоль (можно добавить переход на сцену настроек)
	GD.Print("Открываем настройки");
}

// Обработчик нажатия на кнопку "Выход"
private void OnExitPressed()
{
	// Завершаем работу игры
	GetTree().Quit();
}
}
