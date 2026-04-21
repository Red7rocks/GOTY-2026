using Godot;
using System;

public partial class TextPrompt : Control
{
	public string getPrompt(String action){
		return "test";
	}
	public void FlashObject()
	{
		Tween _tween = CreateTween();	//Tweens are objects that allow gradual transitions between numbers
		_tween.SetLoops(); 	//set loops to default for infinite
		
		// Tween alpha from 1 (opaque) to 0 (transparent) and back
		_tween.TweenProperty(this, "modulate:a", 0.0f, 1.0f);	//shift opacity to 0 over the coarse of 1.0f seconds
		_tween.TweenProperty(this, "modulate:a", 1.0f, 1.0f);	//shift opacity to 1 over the coarse of 1.0f seconds
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FlashObject();
	}
}
