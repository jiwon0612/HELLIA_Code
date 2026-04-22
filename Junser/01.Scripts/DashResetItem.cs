using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashResetItem : MonoBehaviour,IInitializable
{
    [SerializeField] private ParticleSystem _getParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            _getParticle.transform.position = transform.position;
            _getParticle.Play();
            player.ResetDashCount();
            gameObject.gameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        gameObject.gameObject.SetActive(true);
    }
}
