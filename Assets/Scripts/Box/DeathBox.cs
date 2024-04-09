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
        [SerializeField]
        float riskMultiplier =  1;

        public bool IsBad { get => isBad; set => isBad = value; }
        public int Amount { get => amount; set => amount = value; }
        public float RiskMultiplier { get => riskMultiplier; set => riskMultiplier = value; }

        public bool Unbox(out int amount)
        {
            amount = this.amount;
            return isBad;
        }

        public static DeathBox CreateBox(int minAmount,int maxAmount, float riskMultiplier)
        {
            return new DeathBox()
            {
                isBad = UnityEngine.Random.Range(0, 100) < 80 ? true : false,
                amount = (int)(UnityEngine.Random.Range(minAmount, maxAmount) * riskMultiplier)
            };
        }
    }
}
