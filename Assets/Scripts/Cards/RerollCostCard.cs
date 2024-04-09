using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class RerollCostCard : ACard
    {
        [SerializeField]
        int increaseAmount = 3;

        public override void Execute(CardParam param)
        {
            param.Box.Amount = (int)(UnityEngine.Random.Range(5, 10) * param.Box.RiskMultiplier);
        }
    }
}