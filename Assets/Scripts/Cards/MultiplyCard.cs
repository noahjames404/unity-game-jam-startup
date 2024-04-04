using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class MultiplyCard : ACard
    {
        [SerializeField]
        int multiplier = 2;

        public override void Execute(CardParam param)
        {
            param.Box.Amount *= multiplier;
        }
    }
}
