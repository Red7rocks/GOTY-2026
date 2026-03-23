using Godot;
using System;
using System.Collections.Generic;

public partial class ItemData : Resource
{
	[Export] public string Name { get; set; }
	[Export] public Texture2D Icon { get; set; }
	[Export] public bool Stackable { get; set; }
}
public partial class Inventory : Node
{
	private Dictionary<ItemData, int> partyItems = new Dictionary<ItemData, int>();
}
