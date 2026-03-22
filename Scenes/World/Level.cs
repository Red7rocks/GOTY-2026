using Godot;
using System;

public partial class Level : Node2D
{
	private SceneTree tree;		//Private reference to the current tree
	bool canLeave = false;	//Flag used for setting delay between allowing player to change scenes, to prevent unpredictable spawns
	bool selectionBoxActive = false;

	PackedScene saloonScene = GD.Load<PackedScene>("res://Scenes/Buildings/saloon.tscn");
	PackedScene badguyScene = GD.Load<PackedScene>("res://Scenes/Enemies/bad_guy.tscn");
	PackedScene battleScene = GD.Load<PackedScene>("res://Scenes/Battle/battle.tscn");
	PackedScene buildingScene = GD.Load<PackedScene>("res://Scenes/Buildings/house.tscn");
	PackedScene selectionBoxScene = GD.Load<PackedScene>("res://Scenes/yes_no_box.tscn");

	Node2D levelParty;		//Container that will spawn all characters in level
	Node2D levelBuildings;	//Container that will spawn all buildings in the level
	Node2D levelEnemies;	//Container that will spawn all enemies in the level
	YesNoBox selectionBox;	//Selection box that will get addded/interacted with whenever the Building is approached


	private void nearbyEnemy(Node2D body){		
		if(body != Global.Party.getCharacter(0)) return;	//We dont care about the movement of trailing characters
		CallDeferred(nameof(DeferredEnterBattle));		//Call deferred to allow godot to unload current scene
		}
		private void DeferredEnterBattle()
		{
			if(canLeave){											//Check if it has been > spawnTimer amount of time before changing scene
				Global.Party.AddPlayersToScene(Global.Instance);	//Add players to transition node
				tree.ChangeSceneToPacked(battleScene);				//change to battle scene
			}
	}

	private void nearbyBuilding(Node2D body){
		if(body != Global.Party.getCharacter(0)) return;	//We dont care about the movement of trailing characters
		CallDeferred(nameof(DeferredEnterBuilding), body);	//Call deferred to allow godot to unload current scene
		}
		private void DeferredEnterBuilding(Node2D body)
		{
			if (selectionBoxActive && IsInstanceValid(selectionBox)) return;
			selectionBox = selectionBoxScene.Instantiate<YesNoBox>();									//Create a prompt to ask the player if they want to leave
			selectionBox.Position =  body.Position + new Vector2(200, -100);		//Set this prompt slightly offset from the door
			selectionBox.setTargetScene(buildingScene);
			AddChild(selectionBox);																	//Add the prompt to the scene
			selectionBoxActive = true;																//Set flag stating prompt is currently live in the scene
	}
	
	private void notNearbyBuilding(Node2D body){
		if(body != Global.Party.getCharacter(0)) return;	//We dont care about the movement of trailing characters
		if(selectionBoxActive && GodotObject.IsInstanceValid(selectionBox)){	//Check if our selection box is active and has not been cleaned out by X entry
			selectionBox.QueueFree();
			selectionBox = null;	//Delete selection box
		}
		selectionBoxActive = false;												//Set our active flag to false
	}

	private void addBuilding(LevelObject levOb, Vector2 position){
		levOb.Position = position;				//Set Building position
		levOb.Nearby += nearbyBuilding;			//Attach signal to trigger when character approaches building
		levOb.NotNearby += notNearbyBuilding;	//Attach signal to trigger when character walks away from building
		levelBuildings.AddChild(levOb);			//Add Building to the level
	}
	private void addEnemy(LevelObject levOb, Vector2 position){
		levOb.Position = position;				//Set Enemy position
		levOb.Nearby += nearbyEnemy;			//Attach signal to trigger when character approaches enemy
		levelEnemies.AddChild(levOb);			//Add Enemy to level
	}
	private void OnSpawnTimerTimeout(){
		canLeave = true;		//Wait 1 second before allowing players to enter battle so game can first set party position properly
	}
	public override void _Ready()
	{
		canLeave = false;		//Ensure player cannot change scenes on load
		GetNode<Timer>("SpawnTimer").Start();
		
		tree = GetTree();									//Get reference to current tree to use for scene switching later
		levelParty = GetNode<Node2D>("Party");				//Get reference to Party node in level scene
		levelBuildings = GetNode<Node2D>("Buildings");		//Get reference to Buildings node in level scene
		levelEnemies = GetNode<Node2D>("Enemies");			//Get reference to Enemies node in level scene

		LevelObject saloon = saloonScene.Instantiate<LevelObject>();	//create saloon object that can be added to the level
		LevelObject badguy = badguyScene.Instantiate<LevelObject>();	//create badguy object that can be added to the level

		Global.Party.setPartyPosition(Global.levelSpawn);
		Global.Party.AddPlayersToScene(levelParty);

		addBuilding(saloon, new Vector2(650, 100));		//Add saloon building at specified position
		addEnemy(badguy, new Vector2(650, 600));		//Add badguy enemy at specified position
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();			//Move first player in party
		Global.Party.recordPosition();		//Record position of first player in party
		Global.Party.MoveFollowers();		//Use previously recorded positions to move other members of party
	}
}
