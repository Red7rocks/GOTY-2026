using Godot;
using System;

public partial class ShopkeeperPrompt : Node2D
{
	bool isBoxActive = false;
	public void setupShopkeeperPrompt(Vector2 position) {
		Position = position + new Vector2(200, -100);
		isBoxActive = true;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("enter"))	//If enter is pressed
		{
			GD.Print("menu");
			//Add logic to open shopkeeper menu
		}
	}
}
