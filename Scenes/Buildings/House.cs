using Godot;
using System;

public partial class House : Node2D
{
	private SceneTree tree;
	PackedScene levelScene = GD.Load<PackedScene>("res://Scenes/World/level.tscn");
	PackedScene doorScene = GD.Load<PackedScene>("res://Scenes/Buildings/door.tscn");
	Node2D houseParty;
	Node2D houseFurniture;
	bool canSpawn = false;
	
	void OnSpawnTimerTimeout(){
		GD.Print("can spawn");
		canSpawn = true;		//Wait 1 second before allowing players to enter battle so game can first set party position properly
	}
	public void nearbyDoor(){		
		CallDeferred(nameof(DeferredEnterDoor));
	}
	private void DeferredEnterDoor()
	{
		if(canSpawn){
			Global.Party.AddPlayersToScene(Global.Instance);
			tree.ChangeSceneToPacked(levelScene);
		}
	}
	void addDoor(LevelObject levOb, Vector2 position){
		levOb.Nearby += nearbyDoor;
		houseFurniture.AddChild(levOb);		//Add Furniture to level
		levOb.Position = position;			//Set Furniture position
	}
	public override void _Ready()
	{
		canSpawn = false;
		GetNode<Timer>("SpawnTimer").Start();
		tree = GetTree();
		houseParty = GetNode<Node2D>("Party");				//Get reference to Party node in house scene
		houseFurniture = GetNode<Node2D>("Furniture");
		Global.Party.setPartyPosition(Global.levelSpawn);
		Global.Party.AddPlayersToScene(houseParty);			
		LevelObject door = doorScene.Instantiate<LevelObject>();
		addDoor(door, new Vector2(512,811));
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();
		Global.Party.recordPosition();
		Global.Party.MoveFollowers();
	}
}
