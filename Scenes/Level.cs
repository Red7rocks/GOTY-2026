using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
	PackedScene cowboyScene = ResourceLoader.Load<PackedScene>("res://Scenes/cowboy.tscn");
	PackedScene alchemistScene = ResourceLoader.Load<PackedScene>("res://Scenes/alchemist.tscn");
	PackedScene mageScene = ResourceLoader.Load<PackedScene>("res://Scenes/mage.tscn");
	PackedScene tankScene = ResourceLoader.Load<PackedScene>("res://Scenes/tank.tscn");

	List<CharacterBody2D> party = new List<CharacterBody2D>();
	List<Vector2> positions = new List<Vector2>();
	Vector2 lastRecordedPosition;
	int followSpacing = 15;
	int playerSpeed = 400;

	void MoveLeader()
	{
		Vector2 direction = Input.GetVector("a", "d", "w", "s");	//Grab input from WASD keys 
		party[0].Velocity = direction * playerSpeed;
		party[0].MoveAndSlide();
	}
	void recordPosition()
	{
		if (positions.Count == 0 || party[0].GlobalPosition.DistanceTo(lastRecordedPosition) > 0)
		{
			positions.Insert(0, party[0].GlobalPosition);		//Record current position if no positions have been recorded yet,
			lastRecordedPosition = party[0].GlobalPosition;		//or if distance from last recorded position is non-zero
		}
		int maxHistory = followSpacing * party.Count;		//We give ourselves as many position points in our vector as there are spaces between players
		if (positions.Count > maxHistory)
		{
			positions.RemoveAt(positions.Count - 1);			//Memory management so vector doesn't grow infinitely
		}
	}
	void MoveFollowers()
	{
		for (int i = 1; i < party.Count; i++)
		{
			int index = i * followSpacing;		//Setting index of current player to where leading player was 'followingSpacing' positions ago
			if (index < positions.Count)		//Make sure position exists. > Count would be outside of the array
			{
				Vector2 target = positions[index];										//Lerp = Linear interpolation. smoothly transitions position from 
				party[i].GlobalPosition = party[i].GlobalPosition.Lerp(target, 0.25f);	//current position to target position at a rate of 0.25 per tick
			}
		}
	}
	void CreateCharacter(PackedScene scene)
	{
		CharacterBody2D newScene = scene.Instantiate<CharacterBody2D>();
		GetNode<Node2D>("Party").AddChild(newScene);
		party.Add(newScene);
	}
	public override void _Ready()
	{
		CreateCharacter(cowboyScene);
		CreateCharacter(alchemistScene);
		CreateCharacter(mageScene);
		CreateCharacter(tankScene);
	}
	public override void _PhysicsProcess(double delta)
	{
		MoveLeader();
		recordPosition();
		MoveFollowers();
	}
}
