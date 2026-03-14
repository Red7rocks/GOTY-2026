using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene cowboyScene = GD.Load<PackedScene>("res://Scenes/cowboy.tscn");
	PackedScene alchemistScene = GD.Load<PackedScene>("res://Scenes/alchemist.tscn");
	PackedScene mageScene = GD.Load<PackedScene>("res://Scenes/mage.tscn");
	PackedScene tankScene = GD.Load<PackedScene>("res://Scenes/tank.tscn");
	PackedScene saloonScene = GD.Load<PackedScene>("res://Scenes/saloon.tscn");
	PackedScene badguyScene = GD.Load<PackedScene>("res://Scenes/bad_guy.tscn");

	Node2D levelParty;		//Container that will spawn all characters in level
	Node2D levelBuildings;	//Container that will spawn all buildings in the level
	Node2D levelEnemies;			//Container that will spawn all enemies in the level
	Saloon saloon;			//Saloon object accessible across functions;
	Party party;			//Container that holds all player/movement logic
	BadGuy badguy;

	public void nearbySaloon(){
		//Prompt player if they want to enter
		//if they do, change scene
	}
	public override void _Ready()
	{
		party = new Party();
		saloon = saloonScene.Instantiate<Saloon>();
		badguy = badguyScene.Instantiate<BadGuy>();
		levelParty = GetNode<Node2D>("Party");
		levelBuildings = GetNode<Node2D>("Buildings");
		levelEnemies = GetNode<Node2D>("Enemies");

		levelParty.AddChild(party.CreateCharacter(cowboyScene));			//Add a cowboy to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(alchemistScene));			//Add an alchemist to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(mageScene));				//Add a mage to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(tankScene))				;//Add a tank to the party and the level party in one call
		
		levelBuildings.AddChild(saloon);
		saloon.nearby += nearbySaloon;
		
		levelEnemies.AddChild(badguy);
		//saloon.nearby += nearbySaloon;
	}
	public override void _PhysicsProcess(double delta)
	{
		party.MoveLeader();
		party.recordPosition();
		party.MoveFollowers();
	}
}
