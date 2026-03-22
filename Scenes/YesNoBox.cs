using Godot;
using System;

public partial class YesNoBox : Node2D
{
	AnimatedSprite2D selectionArrow;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		selectionArrow = GetNode<AnimatedSprite2D>("SelectionArrow");
		selectionArrow.Position = new Vector2(-50, -40);
		 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
