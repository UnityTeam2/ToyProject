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
            Destroy(gameObject);

            // 점수 상속
            ScoreManager.instance.AddScore(50);
        }
    }
    
    
}
