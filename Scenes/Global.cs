using Godot;
using System;

public partial class Global : Node		//This script is set to auto load in project settings, so no instantiation is needed to access variables
{
	PackedScene mageScene = GD.Load<PackedScene>("res://Scenes/Classes/mage.tscn");
	PackedScene tankScene = GD.Load<PackedScene>("res://Scenes/Classes/tank.tscn");
	PackedScene cowboyScene = GD.Load<PackedScene>("res://Scenes/Classes/cowboy.tscn");
	PackedScene alchemistScene = GD.Load<PackedScene>("res://Scenes/Classes/alchemist.tscn");
	
	public static Global Instance;						//Static reference to itself for accessing all child data
	public Party party;									//dynamic party variable for containing all current party members
	public static Party Party { get; private set; }		//Static reference to party that can be accessed across entire project
	public static Vector2 levelSpawn = new Vector2(100, 100);
	public override void _Ready()
	{
		Instance = this;			//Create a new global instance on game launch
		Party = new Party();		//Create our party that will be used across the project
		AddChild(Party);
		Party.CreateCharacter(cowboyScene);			//Add a cowboy to the party and the level in one call
		Party.CreateCharacter(alchemistScene);			//Add an alchemist to the party and the level in one call
		Party.CreateCharacter(mageScene);				//Add a mage to the party and the level in one call
		Party.CreateCharacter(tankScene);				//Add a tank to the party and the level in one call
	}
}
