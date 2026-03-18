using Godot;
using System;

public partial class House : Node2D
{
	private SceneTree tree;
	PackedScene levelScene = GD.Load<PackedScene>("res://Scenes/World/level.tscn");
	Node2D houseParty;
	Node2D houseFurniture;
	bool canLeave = false;
	
	private void OnSpawnTimerTimeout(){
		canLeave = true;		//Wait 1 second before allowing players to enter battle so game can first set party position properly
	}
	private void nearbyDoor(){
		CallDeferred(nameof(DeferredEnterDoor));		//Calling deferred allows godot to clean up script in memory
		}
		private void DeferredEnterDoor()
		{
			if(canLeave){			//Check if scene has been loaded for > spawnTimer
				Global.Party.AddPlayersToScene(Global.Instance);	//Move players back to transition scene
				tree.ChangeSceneToPacked(levelScene);				//Load overworld scene
			}
	}
	public override void _Ready()
	{
		canLeave = false;
		tree = GetTree();									//Get static reference to current tree that will be used to transition out of scene later
		GetNode<Timer>("SpawnTimer").Start();
		houseParty = GetNode<Node2D>("Party");				//Get reference to Party node in house scene
		houseFurniture = GetNode<Node2D>("Furniture");		//Get reference to Furniture node in house scene
		Global.Party.setPartyPosition(Global.levelSpawn);																						//Need to create seperate variable for shop spawn position
		Global.Party.AddPlayersToScene(houseParty);			//Add players to the house

		GetNode<LevelObject>("Door").Nearby += nearbyDoor;	//Connect house door scene nearby signal to nearbyDoor function
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();		//Same logic from level scene
		Global.Party.recordPosition();
		Global.Party.MoveFollowers();
	}
}
