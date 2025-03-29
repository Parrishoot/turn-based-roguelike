using UnityEngine;

public class CameraShakeAttributes
{
    public float Magnitude { get; private set; } = .3f;

    public float Duration { get; private set; } = .2f;

    public int Vibrato { get; private set; } = 70;

    public CameraShakeAttributes WithMagnitude(float magnitude) {
        this.Magnitude = magnitude;
        return this;
    }

    public CameraShakeAttributes WithDuration(float duration) {
        this.Duration = duration;
        return this;
    }

    public CameraShakeAttributes WithVibrato(int vibrato) {
        this.Vibrato = vibrato;
        return this;
    }
}
