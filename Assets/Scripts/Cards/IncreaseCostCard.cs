using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class IncreaseCostCard : ACard
    {
        [SerializeField]
        int increaseAmount = 3;

        public override void Execute(CardParam param)
        {
            param.Box.Amount += increaseAmount;
        }
    }
}