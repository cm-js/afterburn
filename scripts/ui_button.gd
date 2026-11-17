extends Label
@onready var game_manager = $"%Managers/GameManager"

@export var buttonFunction: String

func _ready() -> void:
	# Make sure the label detects mouse input
	mouse_filter = Control.MOUSE_FILTER_STOP
	
	# Change cursor to pointer
	mouse_default_cursor_shape = Control.CURSOR_POINTING_HAND

func _gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		# Left click only
		if event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
			detectFunction()

func detectFunction() -> void:
	if buttonFunction == "Start":
		# Start the game
		game_manager.game_state = "active"
		game_manager.swap_ui(1)
