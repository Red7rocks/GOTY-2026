using Godot;
using System;

public partial class BadGuy : StaticBody2D
{
	public override void _Ready()
	{
		Position = new Vector2(650, 600);	//Set spawn point of Bad Guy on the map
	}
}
