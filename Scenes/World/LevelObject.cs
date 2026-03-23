using Godot;
using System;

[GlobalClass]
public partial class LevelObject : Resource			//Generic class for objects in level that detect when players enter/leave nearby area
{
	[Signal]
	public delegate void NearbyEventHandler(Node2D body);		//Event handler for approaching object. Body refers to whatever is entering the object's surrounding area
	
	[Signal]
	public delegate void NotNearbyEventHandler(Node2D body);	//Event handler for walking away from object. Body refers to whatever is entering the object's surrounding area
	
	public virtual void OnNearby(Node2D body)
	{	
		EmitSignal(SignalName.Nearby, body);
	}
	public virtual void OnNotNearby(Node2D body)
	{	
		EmitSignal(SignalName.NotNearby, body);		
	}
	/*public override void _Ready()
	{
		var area = GetNode<Area2D>("SurroundingArea");
		area.BodyEntered += OnSurroundingAreaEntered;		//Add signals so enter/exit functions are always attached
		area.BodyExited += OnSurroundingBodyExited;
	}*/
}
