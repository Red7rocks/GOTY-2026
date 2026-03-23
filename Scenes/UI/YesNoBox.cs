using Godot;
using System;

public partial class YesNoBox : Node2D
{
	private SceneTree tree;	//Private reference to the current tree
	bool canLeave = true;
	bool isBoxActive = false;
	AnimatedSprite2D selectionArrow;
	PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/World/level.tscn");	
	PackedScene targetScene;

	public bool isActive(){
		return isBoxActive;
	}
	public void moveArrowLeft(){
		selectionArrow.Position = new Vector2(-50, -40);	//Set arrow to left position, which corresponds to the green check mark
		canLeave = true;									//Allow player to leave if enter is pressed
	}
	public void moveArrowRight(){
		selectionArrow.Position = new Vector2(50, -40);		//Set arrow to right position, which corresponds to the red X
		canLeave = false;									//Delete YesNoBox if enter is pressed
	}
	public void setTargetScene(PackedScene scene){
		targetScene = scene;
	}
	public void setPrompt(String promptText){
		GetNode<RichTextLabel>("PlayerPrompt").Text = promptText;
	}
	public void setupSelectionBox(String prompt, Vector2 position, PackedScene targetScene){
		setPrompt(prompt);
		Position = position + new Vector2(200, -100);
		setTargetScene(targetScene);
		isBoxActive = true;
	}
	public override void _Ready()
	{
		tree = GetTree();		//Give scene reference to itself
		selectionArrow = GetNode<AnimatedSprite2D>("SelectionArrow");	
		selectionArrow.Position = new Vector2(-50, -40);	//Set initial position of selection arrow to green check mark
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("left"))
		{
			 moveArrowLeft();	//If left arrow on keyboard is pressed, move arrow to left position. Multiple presses do nothing.
		}
		if (@event.IsActionPressed("right"))
		{
			 moveArrowRight();	//If right arrow on keyboard is pressed, move arrow to right position. Multiple presses do nothing.
		}
		if (@event.IsActionPressed("enter") && canLeave)	//If enter is pressed and arrow is over green check mark
		{
			Global.Party.AddPlayersToScene(Global.Instance);	//Add players back to scene transition node
			tree.CallDeferred("change_scene_to_packed", targetScene);	//Change back to overworld
		}
		if (@event.IsActionPressed("enter") && !canLeave)	//If enter is pressed and arrow is over red X
		{
			QueueFree();	//Despawn YesNoBox. Need to account for Node being inactive in whichever scene originally spawned. 
		}
	}
}
