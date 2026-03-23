using Godot;
using System;

[GlobalClass]
public partial class LevelObject : Resource			//Generic class for objects in level that detect when players enter/leave nearby area
{
	Area2D surroundingArea;
	AnimatedSprite2D overworldSprite;
	Sprite2D UIsprite;
	[Signal]
	public delegate void NearbyEventHandler(Node2D body);		//Event handler for approaching object. Body refers to whatever is entering the object's surrounding area
	
	[Signal]
	public delegate void NotNearbyEventHandler(Node2D body);	//Event handler for walking away from object. Body refers to whatever is entering the object's surrounding area
	
	public virtual void OnNearby(Node2D body)
	{	
		EmitSignal(SignalName.Nearby, body);	//If OnNearby() function gets called from this resource, emit nearby signal. Will be called by LevelObjectNode.cs
	}
	public virtual void OnNotNearby(Node2D body)
	{	
		EmitSignal(SignalName.NotNearby, body);		//If NotNearby() function gets called from this resource, emit nearby signal. Will be called by LevelObjectNode.cs
	}
}
