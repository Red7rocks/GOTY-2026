using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene cowboyScene = GD.Load<PackedScene>("res://Scenes/Classes/cowboy.tscn");
	PackedScene alchemistScene = GD.Load<PackedScene>("res://Scenes/Classes/alchemist.tscn");
	PackedScene mageScene = GD.Load<PackedScene>("res://Scenes/Classes/mage.tscn");
	PackedScene tankScene = GD.Load<PackedScene>("res://Scenes/Classes/tank.tscn");
	PackedScene saloonScene = GD.Load<PackedScene>("res://Scenes/Buildings/saloon.tscn");
	PackedScene badguyScene = GD.Load<PackedScene>("res://Scenes/Enemies/bad_guy.tscn");

	Node2D levelParty;		//Container that will spawn all characters in level
	Node2D levelBuildings;	//Container that will spawn all buildings in the level
	Node2D levelEnemies;			//Container that will spawn all enemies in the level

	LevelObject saloon;			//Saloon object accessible across functions;
	Party party;			//Container that holds all player/movement logic
	LevelObject badguy;

	public void nearbySaloon(){
		//Prompt player if they want to enter
		//if they do, change scene
	}
	public void nearbyEnemy(){
		//Prompt player if they want to battle
		//if they do, change scene
	}
	public override void _Ready()
	{
		party = new Party();
		saloon = saloonScene.Instantiate<LevelObject>();
		badguy = badguyScene.Instantiate<LevelObject>();

		levelParty = GetNode<Node2D>("Party");
		levelBuildings = GetNode<Node2D>("Buildings");
		levelEnemies = GetNode<Node2D>("Enemies");

		levelParty.AddChild(party.CreateCharacter(cowboyScene));			//Add a cowboy to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(alchemistScene));			//Add an alchemist to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(mageScene));				//Add a mage to the party and the level party in one call
		levelParty.AddChild(party.CreateCharacter(tankScene));				//Add a tank to the party and the level party in one call
		
		levelBuildings.AddChild(saloon);			//Spawn Buildings
		saloon.Position = new Vector2(650, 100);
		saloon.nearby += nearbySaloon;
		
		levelEnemies.AddChild(badguy);				//Spawn Enemies
		badguy.Position = new Vector2(650, 600);
		badguy.nearby += nearbyEnemy;
	}
	public override void _PhysicsProcess(double delta)
	{
		party.MoveLeader();
		party.recordPosition();
		party.MoveFollowers();
	}
}
