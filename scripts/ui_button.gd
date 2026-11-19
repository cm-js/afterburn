extends Label
@onready var game_manager = $"%Managers/GameManager"

@export var buttonFunction: String
@export var hoverColor: Color

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

# Change the label color when being hovered by the mouse
func _on_mouse_entered() -> void:
	set("theme_override_colors/font_color", hoverColor)

# Revert color when no longer hovering
func _on_mouse_exited() -> void:
	set("theme_override_colors/font_color", Color(1, 1, 1))

func detectFunction() -> void:
	if buttonFunction == "N/A":
		print("This button doesn't do anything.")
	
	if buttonFunction == "Start" || buttonFunction == "Continue":
		# Start the game
		game_manager.game_state = "active"
		game_manager.swap_ui(1)
	
	if buttonFunction == "ToMenu":
		# End the game
		game_manager.game_state = "inactive"
		game_manager.swap_ui(0)
		game_manager.game_reset()
