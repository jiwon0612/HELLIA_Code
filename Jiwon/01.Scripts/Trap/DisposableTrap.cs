using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DisposableTrap : FloorTrap
{
    [Header("DisposableSetting")]
    [SerializeField] private float offStartTime;
    [SerializeField] private float offTime;
    public UnityEvent OnOffEvent;
    
    private SpriteRenderer _spriteRenderer;
    private Sequence _offSequence;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void FloorEnter(Collision2D collision)
    {
        _offSequence = DOTween.Sequence();
        _offSequence.AppendInterval(offStartTime)
            .Append(_spriteRenderer.DOFade(0, offTime))
            .AppendCallback(() =>
            {
                OnOffEvent?.Invoke();
                Destroy(gameObject);
            });
    }
}
