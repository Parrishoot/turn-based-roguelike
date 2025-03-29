using System;
using DG.Tweening;
using UnityEngine;

public class OccupantAnimator : MonoBehaviour
{
    [SerializeField]
    private Transform animatedTransformOverride;

    public EventProcessor OnAnimationFinished = new EventProcessor();

    private Tween currentAnimation;

    private void Start()
    {
        if(animatedTransformOverride == null) {
            animatedTransformOverride = transform;
        }
    }

    public void AnimateDamaged(bool stopCurrentAnimation=true) {

        CameraController.Instance.Shake(new CameraShakeAttributes());

        Play(
            animation: animatedTransformOverride.DOShakePosition(
                duration: .3f,
                strength: .2f,
                vibrato: 50
            ).OnComplete(OnAnimationFinished.Process),
            stopCurrentAnimation: stopCurrentAnimation
        );
    }

    public void AnimateAttack(BoardOccupant target, Action onAttack=null, bool stopCurrentAnimation=false) {

        Vector3 startingPosition = animatedTransformOverride.position;

        Vector3 targetPosition = target.Space.WorldPosition;
        targetPosition.y = startingPosition.y;

        Vector3 direction = (targetPosition - animatedTransformOverride.position).normalized;
        float attackDistance = .75f;

        Vector3 attackTarget = startingPosition + (direction * attackDistance);

        Tween attackSequence = DOTween.Sequence()
            .Append(animatedTransformOverride.DOMove(attackTarget, .1f)
                .SetEase(Ease.InCubic)
                .OnComplete(() => onAttack?.Invoke()))
            .Append(animatedTransformOverride.DOMove(startingPosition, .1f)
                .SetEase(Ease.OutCubic))
            .OnComplete(OnAnimationFinished.Process);

        Play(attackSequence, stopCurrentAnimation);
    }

    public void StopCurrentAnimation() {
        if(currentAnimation != null && !currentAnimation.IsComplete()) {
            currentAnimation.Complete();
        }
    }

    public void Play(Tween animation, bool stopCurrentAnimation=false) {

        if(!stopCurrentAnimation) {
            return;
        }

        StopCurrentAnimation();
        animation.Play();
    }
}
