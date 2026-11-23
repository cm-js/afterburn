extends StaticBody2D

var ground_manager
var game_manager

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	ground_manager = get_parent()
	game_manager = get_parent().get_parent().get_node("GameManager")
	position = Vector2(1200, -(randi() % 800) - 100)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if game_manager.game_state != "active": return
	position -= Vector2(ground_manager.speed, 0)
