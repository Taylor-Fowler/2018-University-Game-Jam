using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutronController : Enemy
{
    public bool GivesEnergy = false;

    private void Start()
    {
        Invoke("DestroyAfterDelay", 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if(playerController != null)
        {
            GameManager.OnDestroyNeutron(transform.position);
            if(GivesEnergy)
            {
                GameManager.OnGainEnergy();
            }
            Destroy(gameObject);
        }
    }

    private void DestroyAfterDelay()
    {
        GameManager.OnDestroyNeutron(transform.position);
        Destroy(gameObject);
    }
}
