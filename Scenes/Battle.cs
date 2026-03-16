using Godot;
using System;

public partial class Battle : Node2D
{
	PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/level.tscn");
	public override void _Ready()
	{
		for(int i = 0; i < Global.Party.getPartyCount(); i++){
			GD.Print(Global.Party.getCharacter(i));
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("space"))
		{
			GetTree().ChangeSceneToPacked(levelScene);
		}
		
	}
}
