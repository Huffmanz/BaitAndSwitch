using Godot;
using System;
using System.Collections.Generic;

public partial class RandomAudioPlayer : AudioStreamPlayer
{
    [Export] Godot.Collections.Array<AudioStream> audioStreams;
    [Export] bool randomizePitch = true;
    [Export] double minPitch = 0.9;
    [Export] double max_pitch = 1.1;

    public void PlayRandom()
    {
        if (audioStreams.Count == 0) return;
        PitchScale = (float)GD.RandRange(minPitch, max_pitch);
        Stream = audioStreams.PickRandom();
        Play();
    }
}
