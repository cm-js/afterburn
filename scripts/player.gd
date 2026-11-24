extends CharacterBody2D
@onready var animated_sprite = $AnimatedSprite2D

const SPEED = 300.0
const JUMP_VELOCITY = -450.0
var doubleJumped = false

@onready var game_manager = $"%Managers/GameManager"
@onready var ground_manager = $"%Managers/GroundManager"

func _physics_process(delta: float) -> void:
	# Don't run physics calculations if the game isn't active
	if (game_manager.game_state != "active"): return
	
	# Add the gravity
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump
	if Input.is_action_just_pressed("jump") and (is_on_floor() || !doubleJumped):
		velocity.y = JUMP_VELOCITY
		doubleJumped = true
	
	# Get input direction
	var direction = Input.get_axis("move_left", "move_right")
	
	# Sprite direction handling
	if direction > 0:
		animated_sprite.flip_h = false
	elif direction < 0:
		animated_sprite.flip_h = true

	# Handle movement
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
	
	# Handle on-floor functions
	if is_on_floor():
		if doubleJumped:
			doubleJumped = false
		
		# Only runs if "touching the ground" above y = 0 (platforms)
		if position.y < -100:
			position -= Vector2(ground_manager.speed, 0)

	move_and_slide()
