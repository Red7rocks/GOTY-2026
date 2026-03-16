using Godot;
using System;

public partial class Battle : Node2D
{
	PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/level.tscn");
	Node2D levelParty;
	public override void _Ready()
	{
		levelParty = GetNode<Node2D>("Party");				//Get reference to Party node in level scene
		for (int i = 0; i < Global.Party.getPartyCount(); i++)
		{
			var character = Global.Party.getCharacter(i);
			character.GlobalPosition = Global.levelSpawn;
			if (character.GetParent() != null)
				character.GetParent().RemoveChild(character);
			levelParty.AddChild(character);
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("space"))
		{
			for (int i = 0; i < Global.Party.getPartyCount(); i++)
			{
				var character = Global.Party.getCharacter(i);
				character.GlobalPosition = Global.levelSpawn;
				if (character.GetParent() != null)
					character.GetParent().RemoveChild(character);
				Global.Party.AddChild(character);
			}
			GetTree().ChangeSceneToPacked(levelScene);
		}
		
	}
}
