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

# Reference displays to set
@onready var distance_label = $"../../UI/GameTrackers/DistanceLabel"
@onready var score_label = $"../../UI/GameTrackers/ScoreLabel"

# Various UIs to toggle
@onready var UI_elements = [
	$"../../UI/TitleScreen", # The Title Screen
	$"../../UI/GameTrackers" # HUD, like Score & Distance
]

@onready var title_screen = $"../../UI/TitleScreen"
@onready var game_trackers = $"../../UI/GameTrackers"

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
	distance = 0
	score = 0


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if game_state == "active":
		# Calculate game varaibles
		distance += ground_manager.speed * delta
		
		# Set UI labels
		distance_label.text = "Distance: " + str(int(round(distance)))
		score_label.text = "Score: " + str(score)

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
		
