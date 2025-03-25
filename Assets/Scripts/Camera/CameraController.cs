using DG.Tweening;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CameraShakeAttributes currentShakeAttributes;

    private Tween currentShakeTween;

    public void Shake(CameraShakeAttributes shakeAttributes) {

        if(DontPlayShake(shakeAttributes)) {
            return;
        }

        currentShakeTween?.Complete();
        currentShakeTween = transform.DOShakePosition(
            duration: shakeAttributes.Duration,
            strength: shakeAttributes.Magnitude,
            vibrato: shakeAttributes.Vibrato,
            fadeOut: true);
    }

    private bool DontPlayShake(CameraShakeAttributes newShake) {
        return currentShakeAttributes != null &&
            currentShakeAttributes.Magnitude >= newShake.Magnitude &&
            currentShakeTween.ElapsedPercentage() < .5f;
    }
}
