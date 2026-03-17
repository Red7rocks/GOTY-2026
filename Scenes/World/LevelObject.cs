using Godot;
using System;

public partial class LevelObject : StaticBody2D
{
	[Signal]
	public delegate void nearbyEventHandler();
	
	[Signal]
	public delegate void notNearbyEventHandler();
	
	private void OnSurroundingAreaEntered(Node2D body)
	{	
		GetNode<AnimatedSprite2D>("AnimationSprite2D").Play();
		EmitSignal(SignalName.nearby);
	}
	private void OnSurroundingBodyExited(Node2D body)
	{	
		GetNode<AnimatedSprite2D>("AnimationSprite2D").Stop();
		EmitSignal(SignalName.notNearby);
	}
}
