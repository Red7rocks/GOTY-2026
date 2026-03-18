using Godot;
using System;

public partial class LevelObject : StaticBody2D
{
	[Signal]
	public delegate void NearbyEventHandler();
	
	[Signal]
	public delegate void NotNearbyEventHandler();
	
	private void OnSurroundingAreaEntered(Node2D body)
	{	
		//GetNode<AnimatedSprite2D>("AnimationSprite2D").Play();
		EmitSignal(SignalName.Nearby);
	}
	private void OnSurroundingBodyExited(Node2D body)
	{	
		//GetNode<AnimatedSprite2D>("AnimationSprite2D").Stop();
		EmitSignal(SignalName.NotNearby);
	}
	public override void _Ready()
	{
		var area = GetNode<Area2D>("SurroundingArea"); // make sure name matches scene
		area.BodyEntered += OnSurroundingAreaEntered;
		area.BodyExited += OnSurroundingBodyExited;
	}
}
