using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene cowboyScene = ResourceLoader.Load<PackedScene>("res://Scenes/cowboy.tscn");
	PackedScene alchemistScene = ResourceLoader.Load<PackedScene>("res://Scenes/alchemist.tscn");
	PackedScene mageScene = ResourceLoader.Load<PackedScene>("res://Scenes/mage.tscn");
	PackedScene tankScene = ResourceLoader.Load<PackedScene>("res://Scenes/tank.tscn");

	Node2D levelParty;		//Container that will spawn all characters in level
	Party party;			//Container that holds all player/movement logic

	public override void _Ready()
	{
		party = new Party();
		levelParty = GetNode<Node2D>("Party");
		levelParty.AddChild(party.CreateCharacter(cowboyScene));			//Add a cowboy to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(alchemistScene));			//Add an alchemist to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(mageScene));				//Add a mage to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(tankScene))				;//Add a tank to the party and the level party in one call
	}
	public override void _PhysicsProcess(double delta)
	{
		party.MoveLeader();
		party.recordPosition();
		party.MoveFollowers();
	}
}
