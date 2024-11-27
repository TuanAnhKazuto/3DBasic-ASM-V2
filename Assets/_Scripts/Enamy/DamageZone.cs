using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeZone : MonoBehaviour
{
    public Collider damegeCollider;
    public int damegeAmount = 20;

    public string targetTag; 
    public List<Collider> colliderTargets = new List<Collider>();


    void Start()
    {
        damegeCollider.enabled = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag) && !colliderTargets.Contains(other))
        {
            colliderTargets.Add(other);
            var go = other.GetComponent<Health>();
            if(go != null)
            {
               go.TakeDamage(damegeAmount);
            }
        }
    }

    private void OntriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag) && !colliderTargets.Contains(other))
        {
            colliderTargets.Add(other);
            var go = other.GetComponent<Health>();
            if(go != null)
            {
                go.TakeDamage(damegeAmount);
            }
        }
    }

    public void BeginAttack()
    {
        colliderTargets.Clear();
        damegeCollider.enabled = true;
    }

    public void EndAttack()
    {
        colliderTargets.Clear();
        damegeCollider.enabled = false;
    }
}