extends Control

@onready var play_button = $VBoxContainer/PlayButton
@onready var settings_button = $VBoxContainer/SettingsButton
@onready var exit_button = $VBoxContainer/ExitButton
@onready var transition = $AnimatedSprite2D

func _ready():
	play_button.pressed.connect(_on_play_pressed)
	settings_button.pressed.connect(_on_settings_pressed)
	exit_button.pressed.connect(_on_exit_pressed)

func _on_play_pressed():
	transition.play("transition")
	get_tree().change_scene_to_file("res://scenes/game.tscn")  # Меняем сцену на игру
	

func _on_settings_pressed():
	print("Открываем настройки")  # Можно добавить сцену настроек

func _on_exit_pressed():
	get_tree().quit()  # Выход из игры
