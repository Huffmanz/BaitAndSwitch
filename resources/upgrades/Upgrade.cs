using Godot;
using System;

[GlobalClass]
public partial class Upgrade : Resource
{
    [Export] public string id;
    [Export] public int maxQuantity;
    [Export] public int cost;
    [Export] public int costIncreasePerLevel;
    [Export] public string title;
    [Export(PropertyHint.MultilineText)] public string description;
}
