using Godot;
using System;

public partial class Saloon : StaticBody2D
{
	public void doorAnimation(){
		GetNode<AnimatedSprite2D>("Sprite2D").Play();
	}
	public override void _Ready()
	{
		Position = new Vector2(650, 100);
	}
	public override void _Process(double delta)
	{
	}
}
