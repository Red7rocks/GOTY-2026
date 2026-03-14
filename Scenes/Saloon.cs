using Godot;
using System;

public partial class Saloon : StaticBody2D
{
	[Signal]
	public delegate void nearbyEventHandler();

	public void doorAnimation(){
		GetNode<AnimatedSprite2D>("BuildingAnimationSprite").Play();
	}
	private void OnSurroundingAreaEntered(Node2D body)
	{	
		GetNode<AnimatedSprite2D>("BuildingAnimationSprite").Play();
		EmitSignal(SignalName.nearby);
	}
	public override void _Ready()
	{
		Position = new Vector2(650, 100);	//Set spawn point of saloon on map
	}
}
