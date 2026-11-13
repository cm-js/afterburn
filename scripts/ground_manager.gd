extends Node

# Reference other managers
@onready var game_manager = $"../GameManager"

# Object lists
@export var ground_objs: Array[StaticBody2D] = []

# Misc management variables
var speed = 5

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if game_manager.game_state == "active":
		for i in ground_objs.size():
			# Transform object
			ground_objs[i].position -= Vector2(speed, 0)
			
			# Move object back to the right if off-screen
			if ground_objs[i].position.x < -2500:
				ground_objs[i].position += Vector2(5000, 0)
