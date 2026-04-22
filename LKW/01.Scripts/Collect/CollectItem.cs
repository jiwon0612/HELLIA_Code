using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectItem : MonoBehaviour,IInitializable
{
    [SerializeField] private float magneticPower = 1;
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private ParticleSystem _getEffect;

    private Vector3 _originPos;
    
    private bool isFollowing = false;
    public bool HaveItem { get; set; } = false;

    private void Start()
    {
        _originPos = transform.position;
    }

    public void Collect(Transform collector)
    {
        if (isFollowing) return;
        StartCoroutine(FollowCoroutine(collector));
    }
    
    // private IEnumerator CollectCoroutine(Transform collector)
    // {
    //     yield return new WaitForSeconds(0.2f);
    //     isFollowing = true;
    //     while (true)
    //     {
    //         float distance = Vector2.Distance(transform.position, collector.position);
    //         float time = distance / magneticPower;
    //         Vector3 startPosition = transform.position;
    //
    //         float currentTime = 0;
    //         while (currentTime <= time)
    //         {
    //             currentTime += Time.deltaTime;
    //             float t = currentTime / time;
    //             transform.position = Vector3.Lerp(startPosition, collector.position, t*t);
    //             yield return new WaitForSeconds(0.001f);
    //         }
    //
    //         isFollowing = true;
    //         yield return null;
    //     }
    // }


    private IEnumerator FollowCoroutine(Transform collector)
    {
        isFollowing = true;
        while (true)
        {
            Vector2 direction = (collector.position - transform.position).normalized;
        
            transform.position += (Vector3)direction/50;
            yield return null;
        }
    }

    public void ResetItem()
    {
        isFollowing = false;
        transform.position = _originPos;
    }

    public void GetItem()
    {
        _getEffect.transform.position = transform.position;
        _getEffect.Play();
        HaveItem = true;
        gameObject.SetActive(false);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _whatIsTarget) != 0)
        {
           StopAllCoroutines();
           StartCoroutine(FollowCoroutine(collision.transform));
           isFollowing = true;
        }
    }

    public void Initialize()
    {
        StopAllCoroutines();
        HaveItem = false;
        gameObject.SetActive(true);
        transform.position = _originPos;
    }
}
