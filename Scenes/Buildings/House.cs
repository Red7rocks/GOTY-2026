using Godot;
using System;

public partial class House : Node2D
{
	private SceneTree tree;
	bool canLeave = false;
	bool selectionBoxActive = false;

	PackedScene levelScene = GD.Load<PackedScene>("res://Scenes/World/level.tscn");
	PackedScene selectionBoxScene = GD.Load<PackedScene>("res://Scenes/yes_no_box.tscn");

	Node2D houseParty;
	Node2D houseFurniture;
	Node2D selectionBox;


	private void OnSpawnTimerTimeout(){
		canLeave = true;		//Wait 1 second before allowing players to enter battle so game can first set party position properly
	}
	private void nearbyDoor(Node2D body){
		if(body != Global.Party.getCharacter(0)) return;
		CallDeferred(nameof(DeferredEnterDoor));		//Calling deferred allows godot to clean up script in memory
	}
	private void DeferredEnterDoor()
		{
			selectionBox = selectionBoxScene.Instantiate<Node2D>();
			selectionBox.Position =  GetNode<Node2D>("Door").Position + new Vector2(200, -100);
			AddChild(selectionBox);
			selectionBoxActive = true;
			if (Input.IsActionPressed("left"))					//If space bar is pressed
			{
				GD.Print("move arrow left");
			}
			if (Input.IsActionPressed("right"))
			{
				GD.Print("Move arrow right");
			}
			//	Global.Party.AddPlayersToScene(Global.Instance);	//Add players back to scene transition node
			//	tree.ChangeSceneToPacked(levelScene);				//Change back to overworld
			//}
			//if(canLeave){											//Check if scene has been loaded for > spawnTimer
			//	Global.Party.AddPlayersToScene(Global.Instance);	//if so, Move players back to transition scene
			//	tree.ChangeSceneToPacked(levelScene);				//Load overworld scene
			//}*/
	}
	private void awayFromDoor(Node2D body){
		if(body != Global.Party.getCharacter(0)) return;
		GD.Print(selectionBoxActive);
		if(selectionBoxActive){
			RemoveChild(selectionBox);
		}
		selectionBoxActive = false;
	}
	public override void _Ready()
	{
		canLeave = false;
		GetNode<Timer>("SpawnTimer").Start();
		
		tree = GetTree();									//Get static reference to current tree that will be used to transition out of scene later
		houseParty = GetNode<Node2D>("Party");				//Get reference to Party node in house scene
		houseFurniture = GetNode<Node2D>("Furniture");		//Get reference to Furniture node in house scene

		Global.Party.setPartyPosition(Global.levelSpawn);																						//Need to create seperate variable for shop spawn position
		Global.Party.AddPlayersToScene(houseParty);			//Add players to the house

		GetNode<LevelObject>("Door").Nearby += nearbyDoor;	//Connect house door scene nearby signal to nearbyDoor function
		GetNode<LevelObject>("Door").NotNearby += awayFromDoor;	//Connect house door scene nearby signal to nearbyDoor function
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();		//Same logic from level scene
		Global.Party.recordPosition();
		Global.Party.MoveFollowers();
	}
}
