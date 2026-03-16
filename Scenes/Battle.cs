using Godot;
using System;

public partial class Battle : Node2D
{
	PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/level.tscn");
	Node2D battleParty;
	public override void _Ready()
	{
		battleParty = GetNode<Node2D>("Party");				//Get reference to Party node in level scene
		Global.Party.AddPlayersToScene(battleParty);
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("space"))
		{
			Global.Party.AddPlayersToScene(Global.Instance);
			GetTree().ChangeSceneToPacked(levelScene);
		}
		
	}
}
