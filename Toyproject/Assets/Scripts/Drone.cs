using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float HP = 100;

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            //Instantiate(destructionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
