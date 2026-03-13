using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
	PackedScene cowboyScene = ResourceLoader.Load<PackedScene>("res://Scenes/cowboy.tscn");
	CharacterBody2D cowboy;
	List<CharacterBody2D> party = new List<CharacterBody2D>();
	public int speed = 400;
	
	public void readInput()
	{
		Vector2 direction = Input.GetVector("a", "d", "w", "s");
		foreach(CharacterBody2D character in party){
			character.Velocity = direction * speed;
			character.MoveAndSlide();
		}
	}
	public override void _Ready()
	{
		cowboy = cowboyScene.Instantiate<CharacterBody2D>();
		AddChild(cowboy);
		party.Add(cowboy);
	}
	public override void _Process(double delta)
	{
		readInput();
	}
}
