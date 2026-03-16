using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene saloonScene = GD.Load<PackedScene>("res://Scenes/Buildings/saloon.tscn");
	PackedScene badguyScene = GD.Load<PackedScene>("res://Scenes/Enemies/bad_guy.tscn");
	PackedScene battleScene = GD.Load<PackedScene>("res://Scenes/battle.tscn");

	Node2D levelParty;		//Container that will spawn all characters in level
	Node2D levelBuildings;	//Container that will spawn all buildings in the level
	Node2D levelEnemies;	//Container that will spawn all enemies in the level

	LevelObject saloon;			//Saloon object accessible across functions;
	LevelObject badguy;			//badguy object accessible across functions;

	public void nearbyBuilding(){
		//Prompt player if they want to enter
		//if they do, change scene
	}
	public void notNearbyBuilding(){
		//Remove level entry prompt
	}
	public void nearbyEnemy(){
		CallDeferred(nameof(DeferredChangeScene));
	}
	private void DeferredChangeScene()
	{
		for (int i = 0; i < Global.Party.getPartyCount(); i++)
		{
			var character = Global.Party.getCharacter(i);
			character.GlobalPosition = Global.levelSpawn;
			if (character.GetParent() != null)
				character.GetParent().RemoveChild(character);
			Global.Party.AddChild(character);
		}
		GetTree().ChangeSceneToPacked(battleScene);
	}
	public void addBuilding(LevelObject levOb, Vector2 position){
		levelBuildings.AddChild(levOb);		//Add Building to the level
		levOb.Position = position;			//Set Building position
		levOb.nearby += nearbyBuilding;		//Attach signal to trigger when character approaches building
		levOb.notNearby += notNearbyBuilding;	//Attach signal to trigger when character walks away from building
	}
	public void addEnemy(LevelObject levOb, Vector2 position){
		levelEnemies.AddChild(levOb);		//Add Enemy to level
		levOb.Position = position;			//Set Enemy position
		levOb.nearby += nearbyEnemy;		//Attach signal to trigger when character approaches enemy
	}
	public override void _Ready()
	{
		saloon = saloonScene.Instantiate<LevelObject>();	//create saloon object that can be added to the level
		badguy = badguyScene.Instantiate<LevelObject>();	//create badguy object that can be added to the level

		levelParty = GetNode<Node2D>("Party");				//Get reference to Party node in level scene
		levelBuildings = GetNode<Node2D>("Buildings");		//Get reference to Buildings node in level scene
		levelEnemies = GetNode<Node2D>("Enemies");			//Get reference to Enemies node in level scene
		
		for (int i = 0; i < Global.Party.getPartyCount(); i++)
		{
			var character = Global.Party.getCharacter(i);
			character.GlobalPosition = Global.levelSpawn;
			if (character.GetParent() != null)
				character.GetParent().RemoveChild(character);
			levelParty.AddChild(character);
		}
			//levelParty.AddChild(character);
		addBuilding(saloon, new Vector2(650, 100));		//Add saloon building at specified position
		addEnemy(badguy, new Vector2(650, 600));		//Add badguy enemy at specified position
	}
	public override void _PhysicsProcess(double delta)
	{
		Global.Party.MoveLeader();
		Global.Party.recordPosition();
		Global.Party.MoveFollowers();
	}
}
