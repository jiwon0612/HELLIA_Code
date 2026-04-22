using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCollector : MonoBehaviour,IInitializable
{
    [SerializeField] private CakeUI _cakeUI;
    [SerializeField] [CanBeNull] private CollectItem _item = null;
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private LayerMask _whatIsGround;
    
    private readonly int maxItemCount = 8;
    
    public List<CollectItem> collectItems = new List<CollectItem>();

    private void Start()
    {
        _cakeUI.SetCakeCount(ItemCount);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log(ItemCount);
        }
    }

    public int ItemCount
    {
        get
        {
            int count = 0;
            collectItems.ForEach(item =>
            {
                if (item.HaveItem)
                {
                    count++;
                }
            });
            return count;
        }
    }

    private void TriggerItem(Collider2D collision)
    {
        if (collision.TryGetComponent(out CollectItem collectItem))
        {
            _item = collectItem;
            collectItem.Collect(transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if ((1 << collision.gameObject.layer & _whatIsTarget) != 0)
        {
            Debug.Log("닿");
            TriggerItem(collision);
        }
        if ((1 << collision.gameObject.layer & _whatIsGround) != 0)
        {
            GetCollectItem();
        }
    }
    
   

    private void GetCollectItem()
    {
        if (_item != null)
        {
            _item.GetItem();
            _cakeUI.SetCakeCount(ItemCount);
            _item = null;
        }
    }

    public void Initialize()
    {
        _item = null;
        foreach (CollectItem item in collectItems)
        {
            if(!item.HaveItem&&item.TryGetComponent(out IInitializable initializable))
            {
                initializable.Initialize();
            }
        }
    }
}
