using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    [SerializeField]
    private float maxHealth;
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }
   
    [SerializeField]
    private float health;
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;

            if(health <= 0)
            {
                //For now, the character destroys itself (with the camera as well lol)
                //but this is here that we will have to program the player's death
                //with the issue "As the player, I want to be able to die."
                Destroy(gameObject);
            }
                
        }
    }
}
