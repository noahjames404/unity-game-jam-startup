using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class ShieldCard : ACard
    {
        [SerializeField]
        int coins; 

        public override void Execute(CardParam param)
        {
            param.Caster.HasShield = true;
        }
    }
}
