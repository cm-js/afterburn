extends Node

# Reference other managers
@onready var ground_manager = $"../GroundManager"
@onready var player = $"../../Player"

# Game management variables
var game_state # active, paused, or inactive

# Game scoring and other
var distance
var best_distance
var score
var best_score

# Enemy management
@export var enemyList: Array[String] = []
@export var BASE_ENEMY_SPAWNRATE: float
var enemy_cooldown
var enemy_cooldown_cur

# Reference displays to set
@onready var distance_label = $"../../UI/GameTrackers/DistanceLabel"
@onready var score_label = $"../../UI/GameTrackers/ScoreLabel"

# Various UIs to toggle
@onready var UI_elements = [
	$"../../UI/TitleScreen", # The Title Screen
	$"../../UI/GameTrackers", # HUD, like Score & Distance
	$"../../UI/PauseMenu", # Pause Menu
	$"../../UI/GameOver" # Game Over Screen
]

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	# Wait for every UI element to be loaded
	for node in UI_elements:
		if not node.ready:
			await node.ready
	
	# Setup UI menus according to game state on startup
	swap_ui(0)
	
	# Initialize game variables
	game_state = "inactive"
	game_reset()
	# TODO -- Saving system for best vars
	best_distance = 0
	best_score = 0

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if game_state == "active":
		# Calculate game varaibles
		distance += ground_manager.speed * delta
		
		# Set UI labels
		distance_label.text = "Distance: " + str(int(round(distance)))
		score_label.text = "Score: " + str(score)
		
		# Process enemy spawning
		enemy_cooldown_cur -= delta
		if (enemy_cooldown_cur <= 0):
			enemy_cooldown_cur = enemy_cooldown
			
			# Clone eneny.tscn with random enemy type
			var enemy = load(enemyList.pick_random())
			var instance = enemy.instantiate()
			add_child(instance)
			print("Enemy loaded.")
		
		# Set enemy cooldown relative to distance traveled
		enemy_cooldown = BASE_ENEMY_SPAWNRATE - (0.35 * log(distance + 1))

# Runs on key input
func _input(event: InputEvent) -> void:
	if event.is_action_pressed("pause"):
		if game_state == "active":
			game_state = "paused"
			swap_ui(2)
		elif game_state == "paused":
			game_state = "active"
			swap_ui(1)
	# Spacebar alternative for resuming the game
	if event.is_action_pressed("resume") && game_state == "paused":
		game_state = "active"
		swap_ui(1)

func swap_ui(num: int) -> void:
	# Loop for each UI element
	for i in len(UI_elements):
		# Get the node and set it to a variable
		var node = UI_elements[i]
		
		# Check if the UI num is the one we want to show
		if i == num:
			# Show every child of that UI node
			# (since you can't toggle parent nodes)
			for child in node.get_children():
				child.show()
		else:
			# Hide every child of any other UI node
			for child in node.get_children():
				child.hide()

# Runs when the player collides with an enemy
func game_over() -> void:
	swap_ui(3)
	game_state = "inactive"
	if (score > best_score): best_score = score
	if (distance > best_distance): best_distance = distance

# Reset the game
func game_reset() -> void:
	distance = 0
	score = 0
	
	# Reset the player's position to the center
	player.position = Vector2(0, -30)
	# Add a little jump when respawning the player
	player.velocity = Vector2(0, -300)
	
	# Reset enemy spawning conditionals
	enemy_cooldown = BASE_ENEMY_SPAWNRATE
	enemy_cooldown_cur = enemy_cooldown
	
	# Delete all instantiated enemy nodes
	for child in get_children():
		child.queue_free()
