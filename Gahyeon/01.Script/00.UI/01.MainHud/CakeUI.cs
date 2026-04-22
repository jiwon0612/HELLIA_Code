using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CakeUI : UIParents
{
    [SerializeField] private float cakeCountChangeTime = 0.25f;
    private Label _cakeCount;
    public override void OnEnable()
    {
        base.OnEnable();
        _cakeCount = Root.Query<Label>("CakeCount");
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         SetCakeCount(UnityEngine.Random.Range(0, 10));
    //     }
    // }

    public void SetCakeCount(int count)
    {
        StartCoroutine(ChangeCakeCount(count));
    }

    private IEnumerator ChangeCakeCount(int count)
    {
        _cakeCount.AddToClassList("down");
        yield return new WaitForSecondsRealtime(cakeCountChangeTime);
        _cakeCount.text = $"x {count}";
        _cakeCount.RemoveFromClassList("down");
    }
}
