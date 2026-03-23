using Godot;
using System;

public partial class House : Node2D
{
	private SceneTree tree;		//Private reference to the current tree
	bool canLeave = false;		//Flag used for setting delay between allowing player to change scenes, to prevent unpredictable spawns
	bool selectionBoxActive = false;	//Flag used for checking if selection box is currently live as a child of the House scene

	PackedScene levelScene = GD.Load<PackedScene>("res://Scenes/World/level.tscn");
	PackedScene selectionBoxScene = GD.Load<PackedScene>("res://Scenes/UI/yes_no_box.tscn");

	Node2D houseParty;		//Node referring to our player party
	Node2D houseFurniture;	//Node to add all furniture in the shop to. need to add assets for shelves/goods/etc.
	YesNoBox selectionBox;	//Selection box that will get addded/interacted with whenever the door is approached

	public void OnSpawnTimerTimeout(){
		canLeave = true;		//Wait 1 second before allowing players to enter battle so game can first set party position properly
	}
	private void nearbyDoor(Node2D body){
		if (!IsInstanceValid(this)) return; 				//Check if door actually exists, and is not a remnant in memory
		if(body != Global.Party.getCharacter(0)) return;	//We dont care about the movement of trailing characters
		CallDeferred(nameof(DeferredEnterDoor), body);		//Calling deferred allows godot to clean up script in memory
		}
		private void DeferredEnterDoor(Node2D body)
		{
			selectionBox = selectionBoxScene.Instantiate<YesNoBox>();					//Create a prompt to ask the player if they want to leave
			selectionBox.setupSelectionBox("leave building?", body.Position, levelScene);
			AddChild(selectionBox);														//Add the prompt to the scene
			selectionBoxActive = true;													//Set flag stating prompt is currently live in the scene
	}
	private void awayFromDoor(Node2D body){
		if (!IsInstanceValid(this)) return;					//Prevents multiple doors from being deleted
		if(body != Global.Party.getCharacter(0)) return;	//We dont care about the movement of trailing characters
		if(selectionBoxActive && GodotObject.IsInstanceValid(selectionBox)){	//Check if our selection box is active and has not been cleaned out by X entry
			CallDeferred(nameof(DeferredRemoveSelectionBox));;											//If so, delete selection box
		}
	}
	private void DeferredRemoveSelectionBox(){
		selectionBoxActive = false;	//Set our active flag to false
		if(GodotObject.IsInstanceValid(selectionBox)){
			RemoveChild(selectionBox);		//remove selection box from scene at the leisure of the engine
			selectionBox.QueueFree();		//Delete selection box object
			selectionBox = null;			//Set variable to null
		}
	}
	public override void _Ready()
	{
		canLeave = false;						//Set delay before player can change scenes again to avoid unpredictable spawns
		GetNode<Timer>("SpawnTimer").Start();	//Start delay timer
		
		tree = GetTree();									//Get static reference to current tree that will be used to transition out of scene later
		houseParty = GetNode<Node2D>("Party");				//Get reference to Party node in house scene
		houseFurniture = GetNode<Node2D>("Furniture");		//Get reference to Furniture node in house scene

		Global.Party.setPartyPosition(Global.levelSpawn);																						//Need to create seperate variable for shop spawn position
		Global.Party.AddPlayersToScene(houseParty);			//Add players to the house

		GetNode<LevelObjectNode>("Door").Data.Nearby += nearbyDoor;	//Connect house door scene nearby signal to nearbyDoor function
		GetNode<LevelObjectNode>("Door").Data.NotNearby += awayFromDoor;	//Connect house door scene nearby signal to nearbyDoor function
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();		//Same logic from level scene
		Global.Party.recordPosition();
		Global.Party.MoveFollowers();
	}
}
