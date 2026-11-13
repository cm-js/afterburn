extends Node

# Reference other managers
@onready var ground_manager = $"../GroundManager"
@onready var player = $"../../Player"

# Game management variables
var game_state # Active, Paused, or Inactive

# Game scoring and other
var distance
var best_distance
var score
var best_score

# Reference displays to set
@onready var distance_label = $"../../UI/GameTrackers/DistanceLabel"
@onready var score_label = $"../../UI/GameTrackers/ScoreLabel"

# Various UIs to toggle
@onready var title_screen = $"../../UI/TitleScreen"
@onready var game_trackers = $"../../UI/GameTrackers"

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	game_state = "active"
	
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
