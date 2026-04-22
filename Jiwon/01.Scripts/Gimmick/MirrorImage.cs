using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorImage : MonoBehaviour
{
    [Header("MirrorSetting")]
    //[SerializeField] private float 
    
    protected Vector2 _imageSize;
    protected Vector2 _imageOffset;
    protected LayerMask _whatIsTarget;
    private Entity _target;
    public SpriteRenderer OriginalSprite { get; private set; }
    public ImageParticle ImageParticle { get; private set; }

    private void Awake()
    {
        OriginalSprite = transform.Find("Visual").GetComponent<SpriteRenderer>();
        ImageParticle = GetComponentInChildren<ImageParticle>();
    }

    public void Initialized(CapsuleCollider2D collider, LayerMask whatIsTarget, Entity target)
    {
        _imageSize = collider.size;
        _imageOffset = collider.offset;
        _whatIsTarget = whatIsTarget;
        _target = target;
    }

    public void Mirror()
    {
        OriginalSprite.sprite = _target.GetCompo<EntityRenderer>().EntitySprite.sprite;
        transform.rotation = _target.transform.rotation;
    }
    
    public virtual bool CheckCanSwitch()
    {
        // Collider2D cal = Physics2D.OverlapCapsule(transform.position, _imageSize, CapsuleDirection2D.Vertical, 0, _whatIsTarget);
        // if (cal != null)
        //     Debug.Log(cal.name);
        
        Vector2 point = new Vector2(transform.position.x + _imageOffset.x, transform.position.y + _imageOffset.y);
        Vector2 size = new Vector2(_imageSize.x, _imageSize.y - 0.025f);
        bool canSwitch = Physics2D.OverlapCapsule(point, size , CapsuleDirection2D.Vertical, 0, _whatIsTarget);
        return !canSwitch;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Vector2 point = new Vector2(transform.position.x + _imageOffset.x, transform.position.y + _imageOffset.y);
        Vector2 size = new Vector2(_imageSize.x, _imageSize.y - 0.025f);
        Gizmos.DrawWireCube(point, size);
    }
}
