using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> initObjects;
    
    [SerializeField] private LayerMask _whatIsPlayer;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _whatIsPlayer) != 0)
        {
            StageManager.instance.NextStageIdx();
            _collider.enabled = false;
        }
    }

    public void InitObjects()
    {
        foreach (GameObject item in initObjects)
        {
            if(item == null) continue;
            if(item.TryGetComponent(out IInitializable initItem))
            {
                initItem.Initialize();
            }
        }
    }
}
