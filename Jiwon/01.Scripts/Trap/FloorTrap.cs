using UnityEngine;

public abstract class FloorTrap : MonoBehaviour
{
    [Header("TrapSetting")] 
    [SerializeField] protected LayerMask whatIsTarget;

    protected bool _isPlayerEnter;

    protected virtual void Awake()
    {
        _isPlayerEnter = false;
    }
    
    protected abstract void FloorEnter(Collision2D collision);

    protected virtual void FloorExit(Collision2D collision)
    {
        _isPlayerEnter = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsTarget) != 0)
        {
            _isPlayerEnter = true;
            FloorEnter(collision);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsTarget) != 0)
        {
            FloorExit(collision);
        }
    }
}
