using System.Collections;
using Hellmade.Sound;
using UnityEngine;

public class FlyItem : MonoBehaviour
{
    [SerializeField] private GimmickSoundDataSO _gimmickSoundDataSO;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] float radius = 0.5f;

    [SerializeField] private float flyTime;

    //초기화
    private Collider2D _itemCol;
    private SpriteRenderer _visual;
    private float _provGravityScale;
    private ParticleSystem _getItemEffect;
    
    private void Awake()
    {
        _itemCol = GetComponent<Collider2D>();
        _visual = transform.Find("Visual").GetComponent<SpriteRenderer>();
        _getItemEffect = transform.Find("FlyItemEffect").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            var player = other.GetComponent<Player>();
            
            //사운드
            _gimmickSoundDataSO.PlaySound(SoundType.WingCollect);
            _gimmickSoundDataSO.PlaySound(SoundType.WingUsing);
            
            player.transform.position = transform.position - Vector3.up * 0.5f;
            player?.ChangeState("Fly");
            
            StartCoroutine(StopFly());
            
            _getItemEffect.Play();
            
            _itemCol.enabled = false;
            _visual.color = new Color(1, 1, 1, 0.4f);
        }
    }

    IEnumerator StopFly()
    {
        yield return new WaitForSeconds(flyTime);
        
        _itemCol.enabled = true;
        _visual.color = new Color(1, 1, 1, 1);
    }
}