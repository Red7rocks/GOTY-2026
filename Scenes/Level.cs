using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene mageScene = GD.Load<PackedScene>("res://Scenes/Classes/mage.tscn");
	PackedScene tankScene = GD.Load<PackedScene>("res://Scenes/Classes/tank.tscn");
	PackedScene cowboyScene = GD.Load<PackedScene>("res://Scenes/Classes/cowboy.tscn");
	PackedScene alchemistScene = GD.Load<PackedScene>("res://Scenes/Classes/alchemist.tscn");
	PackedScene saloonScene = GD.Load<PackedScene>("res://Scenes/Buildings/saloon.tscn");
	PackedScene badguyScene = GD.Load<PackedScene>("res://Scenes/Enemies/bad_guy.tscn");

	Node2D levelParty;		//Container that will spawn all characters in level
	Node2D levelBuildings;	//Container that will spawn all buildings in the level
	Node2D levelEnemies;	//Container that will spawn all enemies in the level

	LevelObject saloon;			//Saloon object accessible across functions;
	LevelObject badguy;			//badguy object accessible across functions;

	Party party;			//Container that holds all player/movement logic

	public void nearbyBuilding(){
		//Prompt player if they want to enter
		//if they do, change scene
	}
	public void notNearbyBuilding(){
		//Prompt player if they want to enter
		//if they do, change scene
	}
	public void nearbyEnemy(){
		//Prompt player if they want to battle
		//if they do, change scene
	}
	public void notNearbyEnemy(){
		//Prompt player if they want to battle
		//if they do, change scene
	}
	public void addBuilding(LevelObject levOb, Vector2 position){
		levelBuildings.AddChild(levOb);		//Add Building to the level
		levOb.Position = position;			//Set Building position
		levOb.nearby += nearbyBuilding;		//Attach signal to trigger when character approaches building
		levOb.notNearby += nearbyBuilding;	//Attach signal to trigger when character walks away from building
	}
	public void addEnemy(LevelObject levOb, Vector2 position){
		levelEnemies.AddChild(levOb);		//Add Enemy to level
		levOb.Position = position;			//Set Enemy position
		levOb.nearby += nearbyEnemy;		//Attach signal to trigger when character approaches enemy
		levOb.notNearby += notNearbyEnemy;	//Attach signal to trigger when character walks away from enemy
	}
	public override void _Ready()
	{
		party = new Party();		//Instantiate player party
		saloon = saloonScene.Instantiate<LevelObject>();	//create saloon object that can be added to the level
		badguy = badguyScene.Instantiate<LevelObject>();	//create badguy object that can be added to the level

		levelParty = GetNode<Node2D>("Party");				//Get reference to Party node in level scene
		levelBuildings = GetNode<Node2D>("Buildings");		//Get reference to Buildings node in level scene
		levelEnemies = GetNode<Node2D>("Enemies");			//Get reference to Enemies node in level scene

		levelParty.AddChild(party.CreateCharacter(cowboyScene));			//Add a cowboy to the party and the level in one call
		levelParty.AddChild(party.CreateCharacter(alchemistScene));			//Add an alchemist to the party and the level in one call
		levelParty.AddChild(party.CreateCharacter(mageScene));				//Add a mage to the party and the level in one call
		levelParty.AddChild(party.CreateCharacter(tankScene));				//Add a tank to the party and the level in one call
		
		addBuilding(saloon,  new Vector2(650, 100));		//Add saloon building at specified position
		addEnemy(badguy, new Vector2(650, 600));			//Add badguy enemy at specified position
	}
	public override void _PhysicsProcess(double delta)
	{
		party.MoveLeader();
		party.recordPosition();
		party.MoveFollowers();
	}
}
