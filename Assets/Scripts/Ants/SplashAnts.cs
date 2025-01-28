using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAnts : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private string antTag = "Ant";
    [SerializeField] private float lifeDuration = 1f;

    public void Start()
    {
        // circle cast and get all the colliders tagged as Ant and hit them
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == antTag)
            {
                hitCollider.GetComponent<Ant>().Damage();
            }
        }
    }

    public void Update()
    {
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
