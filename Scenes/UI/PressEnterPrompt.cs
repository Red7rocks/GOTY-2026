using Godot;
using System;

public partial class PressEnterPrompt : Control
{
	public void FlashObject()
	{
		// Ensure Modulate is initialized
		Modulate = new Color(Modulate, 1); 
		
		Tween _tween = CreateTween();
		// Set loops to 0 for infinite, or a specific number
		_tween.SetLoops(); 
		
		// Tween alpha from 1 (opaque) to 0 (transparent) and back
		_tween.TweenProperty(this, "modulate:a", 0.0f, 0.5f);
		_tween.TweenProperty(this, "modulate:a", 1.0f, 0.5f);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FlashObject();
	}
}
