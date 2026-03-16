using Godot;
using System;

public partial class Global : Node		//This script is set to auto load in project settings, so no instantiation is needed to access variables
{
	public static Global Instance;						//Static reference to itself for accessing all child data
	public Party party;									//dynamic party variable for containing all current party members
	public static Party Party { get; private set; }		//Static reference to party that can be accessed across entire project
	public override void _Ready()
	{
		Instance = this;			//Create a new global instance on game launch
		Party = new Party();		//Create our party that will be used across the project
		AddChild(Party);
	}
}
