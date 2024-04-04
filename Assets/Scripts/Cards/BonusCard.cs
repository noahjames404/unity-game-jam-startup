using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class BonusCard : ACard
    {
        [SerializeField]
        int coins;

        public override void Execute(CardParam param)
        {
            param.Caster.IncreaseCoins(coins);
        }
    }
}
