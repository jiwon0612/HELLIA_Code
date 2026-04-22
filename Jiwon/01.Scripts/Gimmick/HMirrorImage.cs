using UnityEngine;

public class HMirrorImage : MirrorImage
{
    public override bool CheckCanSwitch()
    {
         Vector2 rendererPoint = OriginalSprite.bounds.min;
         Vector2 point = new Vector2(rendererPoint.x  + 0.4f + _imageOffset.x, rendererPoint.y + _imageOffset.y);
         Vector2 size = new Vector2(_imageSize.x, _imageSize.y - 0.25f);
         bool canSwitch = Physics2D.OverlapCapsule(point, size , CapsuleDirection2D.Vertical, 0, _whatIsTarget);
         return !canSwitch;
    }

    protected override void OnDrawGizmos()
    {
        if (OriginalSprite == null) return;
        
        Gizmos.color = Color.red;
        Vector2 rendererPoint = OriginalSprite.bounds.min;
        Vector2 point = new Vector2(rendererPoint.x + 0.4f + _imageOffset.x, rendererPoint.y + _imageOffset.y);
        Vector2 size = new Vector2(_imageSize.x, _imageSize.y - 0.25f);
        Gizmos.DrawWireCube(point, size);
    }
}
