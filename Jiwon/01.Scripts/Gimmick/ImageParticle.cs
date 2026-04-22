using DG.Tweening;
using UnityEngine;

public class ImageParticle : MonoBehaviour
{
    [SerializeField] private float duration;
    private ParticleSystem _particle;
    
    private Vector2 _startPoint;
    private Tween _tween;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void PlayParticle(Vector3 playerPoint)
    {
        if (_tween.IsActive())
        {
            _tween.Complete();
            _particle.Stop();
        }
        
        
        float dist = Vector3.Distance(playerPoint, transform.position);
        float dur = dist / duration;
        
        _particle.Play();
        
        _tween = transform.DOMove(playerPoint + (Vector3.up / 2), dur).SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                transform.localPosition = Vector3.zero;
                _particle.Stop();
            });
    }
}
