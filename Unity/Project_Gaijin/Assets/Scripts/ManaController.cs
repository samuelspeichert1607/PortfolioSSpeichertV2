using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{

    [SerializeField]
    private float maxMana;
    public float MaxMana
    {
        get
        {
            return maxMana;
        }

        set
        {
            maxMana = value;
        }
    }
   
    [SerializeField]
    private float mana;
    public float Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }
}
