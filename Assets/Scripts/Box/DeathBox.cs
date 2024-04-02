using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    [Serializable]
    public class DeathBox 
    {
        [SerializeField]
        bool isBad;
        [SerializeField]
        int amount;

        public bool IsBad { get => isBad; set => isBad = value; }
        public int Amount { get => amount; set => amount = value; }

        public bool Unbox(out int amount)
        {
            amount = this.amount;
            return isBad;
        }

        public static DeathBox CreateBox(int minAmount,int maxAmount, float multiplier = 1)
        {
            return new DeathBox()
            {
                isBad = UnityEngine.Random.Range(0, 100) < 80 ? true : false,
                amount = (int)(UnityEngine.Random.Range(minAmount, maxAmount) * multiplier)
            };
        }
    }
}
