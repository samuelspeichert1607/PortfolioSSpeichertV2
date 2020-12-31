using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void Apply(GameObject caster,GameObject target);
    }

}


