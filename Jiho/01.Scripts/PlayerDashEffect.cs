using UnityEngine;

public class PlayerDashEffect : MonoBehaviour
{
    public static bool isDash;

    private readonly float _showDelay = 0.05f;
    private float _currentTime;

    private GameObject _ghost;
    private SpriteRenderer _ghostSpriteRenderer;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        _ghost = transform.Find("Ghost").gameObject;
        _ghostSpriteRenderer = _ghost.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        ApplyAfterImage();
    }

    private void ApplyAfterImage()
    {
        if (!isDash)
        {
            _ghostSpriteRenderer.sprite = null;
            return;
        }
        
        _currentTime -= Time.deltaTime;

        if (!(_currentTime <= 0))
            return;

        var currentGhost = Instantiate(_ghost, transform.position, transform.rotation);
        _ghost.SetActive(true);
        _ghostSpriteRenderer.sprite = _spriteRenderer.sprite;
        Destroy(currentGhost, 0.18f);
        _currentTime = _showDelay;
    }
}