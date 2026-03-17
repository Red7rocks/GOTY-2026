using Godot;
using System;

public partial class Battle : Node2D
{
	private SceneTree tree;
	PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/World/level.tscn");
	Node2D battleParty;

	public override void _Ready()
	{
		tree = GetTree();
		battleParty = GetNode<Node2D>("Party");				//Get reference to Party node in battle scene

		Global.Party.setPartyPosition(Global.levelSpawn);
		Global.Party.AddPlayersToScene(battleParty);		//Add players to Party node
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("space"))
		{
			Global.Party.AddPlayersToScene(Global.Instance);	//Add players back to scene transition node
			tree.ChangeSceneToPacked(levelScene);				//Change back to overworld
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();
		Global.Party.recordPosition();
		Global.Party.MoveFollowers();
	}
}
