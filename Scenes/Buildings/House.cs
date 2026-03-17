using Godot;
using System;

public partial class House : Node2D
{
	private SceneTree tree;
	PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/World/level.tscn");
	Node2D houseParty;

	public override void _Ready()
	{
		tree = GetTree();
		houseParty = GetNode<Node2D>("Party");				//Get reference to Party node in level scene
		Global.Party.AddPlayersToScene(houseParty);
		Global.Party.setPartyPosition(Global.levelSpawn);
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("space"))
		{
			Global.Party.AddPlayersToScene(Global.Instance);
			tree.ChangeSceneToPacked(levelScene);
		}
	}
}
