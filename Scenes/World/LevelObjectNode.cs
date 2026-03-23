using Godot;
using System;

public partial class LevelObjectNode : StaticBody2D	//This is the template class for all objects to spawn in overworld scenes
{													//Currently provides signals for approaching and walking away from said object
													//Resources are used for storing data, where as class handlers such as this one are used for physics functions.
	
	[Export]										
	public LevelObjectResource Data;					//Build off of our LevelObject resource
	
	private bool playerNearby = false;			//Keep track of if player is in the vicinity
	private Node2D currentBody;					//Keep track of which object is nearby (typically party leader)

	public override void _Ready()
	{
		var area = GetNode<Area2D>("SurroundingArea");	//Get surrounding area node from declared object
		area.BodyEntered += OnBodyEntered;				//Link entry signal to pulled node
		area.BodyExited += OnBodyExited;				//Link exit signal to pulled node
	}
	private void OnBodyEntered(Node body)
	{
		if (body is not Node2D node) return;	//If body is something other than a player, do nothing

		playerNearby = true;	//set our active flag true
		currentBody = node;		//Keep track of what is nearby

		Data?.OnNearby(node);	//Trigger our nearby function from our LevelObject resource
	}
	private void OnBodyExited(Node body)
	{
		if (body is not Node2D node) return;	//If body is something other than a player, do nothing

		playerNearby = false;	//set our active flag false
		currentBody = null;		//no longer keep track of whichever player was in vicinity

		Data?.OnNotNearby(node);	//Trigger our not nearby function from our LevelObject resource
	}
}
