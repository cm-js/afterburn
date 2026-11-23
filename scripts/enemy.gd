extends Area2D

# Specifies the enemy type. Only really used to calculate movement,
# POSSIBLY damage amt if health is added later
# VALID TYPES:
#    Border
#    Missile
@export var enemyType: String
var speed;

# Other scripts, to call functions if necessary
var game_manager
var player

func _ready() -> void:
	if (enemyType == "Border"): 
		game_manager = $"%Managers/GameManager"
	else:
		game_manager = get_parent()
		player = get_tree().root.get_node("Game").get_node("Player")
	
	# Initialise enemy by type
	if (enemyType == "Missile"):
		speed = 100
		var areaDir = -1 if randfn(0.0, 1.0) < 0 else 1
		position = Vector2(areaDir * 1000, -(randi() % 1000))

func _process(delta: float) -> void:
	if (!game_manager.game_state == "active"): return
	if (enemyType == "Missile"):
		global_position = global_position.move_toward(player.position, speed * delta)
		speed += 0.1

# Because of physics layers, this only runs when
# the body entering the Area2D is the player
func _on_body_entered(body: Node2D) -> void:
	game_manager.game_over()
