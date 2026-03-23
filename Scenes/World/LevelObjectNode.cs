using Godot;
using System;

public partial class LevelObjectNode : StaticBody2D
{
	[Export]
	public LevelObject Data;
	
	private bool playerNearby = false;
	private Node2D currentBody;

	public override void _Ready()
	{
		var area = GetNode<Area2D>("SurroundingArea");

		area.BodyEntered += OnBodyEntered;
		area.BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is not Node2D node) return;

		playerNearby = true;
		currentBody = node;

		Data?.OnNearby(node);
	}

	private void OnBodyExited(Node body)
	{
		if (body is not Node2D node) return;

		playerNearby = false;
		currentBody = null;

		Data?.OnNotNearby(node);
	}

//public override void _Process(double delta)
//	{
//		if (playerNearby && Input.IsActionJustPressed("interact"))
//		{
//			Data?.Interact(currentBody);
//		}
//	}
}
