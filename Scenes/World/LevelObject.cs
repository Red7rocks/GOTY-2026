using Godot;
using System;

public partial class LevelObject : StaticBody2D			//Generic class for objects in level that detect when players enter/leave nearby area
{
	[Signal]
	public delegate void NearbyEventHandler(Node2D body);
	
	[Signal]
	public delegate void NotNearbyEventHandler(Node2D body);
	
	private void OnSurroundingAreaEntered(Node2D body)
	{	
		EmitSignal(SignalName.Nearby, body);
	}
	private void OnSurroundingBodyExited(Node2D body)
	{	
		EmitSignal(SignalName.NotNearby, body);
	}
	public override void _Ready()
	{
		var area = GetNode<Area2D>("SurroundingArea");
		area.BodyEntered += OnSurroundingAreaEntered;		//Add signals so enter/exit functions are always attached
		area.BodyExited += OnSurroundingBodyExited;
	}
}
