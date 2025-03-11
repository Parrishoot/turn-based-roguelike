using DG.Tweening;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField]
    private float bobHeight = 1f;

    [SerializeField]
    private float bobTime = 1f;

    private Tween tweenAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float startingY = transform.localPosition.y; 

        tweenAnimation = DOTween.Sequence()
            .Append(transform.DOLocalMoveY(startingY + bobHeight, bobTime / 2).SetEase(Ease.InOutSine))
            .Append(transform.DOLocalMoveY(startingY, bobTime / 2).SetEase(Ease.InOutSine))
            .SetLoops(-1)
            .Play();
    }

    void OnDestroy()
    {
        tweenAnimation.Kill();
    }
}
