extends Area2D

# Specifies the enemy type. Only really used to calculate movement,
# POSSIBLY damage amt if health is added later
@export var enemyType: String

# Other scripts, to call functions if necessary
var game_manager

func _ready() -> void:
	if (enemyType == "Border"): 
		game_manager = $"%Managers/GameManager"
	else:
		game_manager = get_parent()
		position = Vector2(0, -100)

# Because of physics layers, this only runs when
# the body entering the Area2D is the player
func _on_body_entered(body: Node2D) -> void:
	game_manager.game_over()
