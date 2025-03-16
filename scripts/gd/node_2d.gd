extends Node2D

# Ссылки на узлы
@onready var sprite = $Sprite2D
@onready var label = $Label  # Убедитесь, что Label добавлен в сцену
@onready var button = $Button

# Флаг для отслеживания состояния видимости спрайта и Label
var is_visible = false

# Путь к текстовому файлу
var file_path = "res://text//answer.txt"

func _ready():
	# Устанавливаем начальное состояние спрайта и Label
	sprite.visible = is_visible
	label.visible = is_visible
	
	# Подключаем сигнал кнопки к функции
	button.connect("pressed", Callable(self, "toggle_visibility"))
	
	# Загружаем текст из файла и устанавливаем его в Label
	load_text_into_label()

# Функция для переключения видимости спрайта и Label
func toggle_visibility():
	is_visible = not is_visible
	sprite.visible = is_visible
	label.visible = is_visible

# Функция для загрузки текста из файла и установки его в Label
func load_text_into_label():
	var text = load_text_from_file(file_path)
	if text != null:
		label.text = text  # Устанавливаем текст в Label
	else:
		label.text = "Не удалось загрузить текст из файла."

# Функция для чтения текста из файла
func load_text_from_file(path: String) -> String:
	var file = FileAccess.open(path, FileAccess.READ)
	if file:
		var text = file.get_as_text()  # Читаем весь текст из файла
		file.close()
		return text
	else:
		push_error("Файл не найден или не может быть открыт: " + path)
		return ""
