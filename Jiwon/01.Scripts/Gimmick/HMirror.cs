using UnityEngine;

public class HMirror : Mirror
{
    protected override void Drawing()
    {
        base.Drawing();
        Vector2 playerLocalPoint = transform.InverseTransformPoint(target.transform.position);
        
        _image.transform.localPosition = new Vector2(playerLocalPoint.x, -playerLocalPoint.y); 
    }
    
    public override void SwitchPlayerAndImage()
    {
        base.SwitchPlayerAndImage();
        
        if (!_isDrawing) return;
        
        if (_image.CheckCanSwitch() == false)
        {
            return;
        }
        
        Vector2 imagePoint = _image.OriginalSprite.bounds.min;
        
        target.transform.position = new Vector3(target.transform.position.x, imagePoint.y, target.transform.position.z);
        _image.ImageParticle.PlayParticle(target.transform.position);
    }
}
