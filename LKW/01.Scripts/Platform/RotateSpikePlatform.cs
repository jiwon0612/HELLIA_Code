using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RotateSpikePlatform : FloorTrap
{
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float shakeTime = 0.5f;
    [SerializeField] private float returnTime = 3f;
    private bool isFilp = false;

    private Sequence _reveres;


    protected override void FloorExit(Collision2D collision)
    {
        base.FloorExit(collision);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    private void Reveres()
    {
        _reveres = DOTween.Sequence();
        _reveres.AppendInterval(waitTime)
            .Append(transform.DOShakePosition(waitTime, new Vector3(0.1f, 0, 0), 15, 50f, false))
            .AppendInterval(waitTime)
            .AppendCallback(() => ReveresGround(isFilp))
            .AppendInterval(returnTime)
            .AppendCallback(() => ReveresGround(isFilp));
    }

    private void ReveresGround(bool filp)
    {
        
        if(isFilp == false)
        {
            transform.DORotate(new Vector3(0,0,180f),0.2f,RotateMode.Fast);
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast);

        }
        isFilp = !filp;

    }

    protected override void FloorEnter(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & whatIsTarget) != 0 &&  !_reveres.IsActive())
        {
            Reveres();
        }
    }
}
