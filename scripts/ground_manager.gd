extends Node

# Reference other managers
@onready var game_manager = $"%Managers/GameManager"

# Object lists
@export var ground_objs: Array[StaticBody2D] = []

# Misc management variables
@export var PLATFORM_SPAWNRATE: float
var platform_cooldown
var platform_cooldown_cur
var speed = 5

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	platform_cooldown = PLATFORM_SPAWNRATE
	platform_cooldown_cur = platform_cooldown


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if game_manager.game_state == "active":
		for i in ground_objs.size():
			# Transform object
			ground_objs[i].position -= Vector2(speed, 0)
			
			# Move object back to the right if off-screen
			if ground_objs[i].position.x < -2500:
				ground_objs[i].position += Vector2(6400, 0)
		
		# Process platform spawning
		platform_cooldown_cur -= delta
		if (platform_cooldown_cur <= 0):
			platform_cooldown_cur = platform_cooldown
			
			# Clone eneny.tscn with random enemy type
			var platform = load("res://scenes/platform.tscn")
			var instance = platform.instantiate()
			add_child(instance)
			print("Platform loaded.")
