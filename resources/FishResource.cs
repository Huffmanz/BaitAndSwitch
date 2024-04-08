using Godot;

[GlobalClass]
public partial class FishResource : Resource
{
    [Export] public string Name { get; private set; }
    [Export] public CompressedTexture2D texture { get; private set; }
    [Export] public AtlasTexture icon { get; private set; }
    [Export] public int Weight { get; private set; }
    [Export] public int minCoins { get; private set; }
    [Export] public int maxCoins { get; private set; }
}
