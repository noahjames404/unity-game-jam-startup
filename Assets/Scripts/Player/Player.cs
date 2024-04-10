using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    [Serializable]
    public class Player
    {
        [SerializeField]
        int coins; 
        [SerializeField]
        List<ACard> cards;
        [SerializeField]
        string playerName;
        [SerializeField]
        bool isMine = false;
        [SerializeField]
        bool hasShield = false;

        public event Action OnDefended;
        public event Action<int> OnTakeDamage;
        public event Action<int> OnReduceCoins;
        public event Action<int> OnIncreaseCoins;

        public Player(int coins)
        {
            this.coins = coins;
        }

        public void TakeDamage(int coins)
        {
            if(HasShield)
            {
                OnDefended?.Invoke();
                HasShield = false;
                return;
            }
            this.coins -= coins;
            OnTakeDamage?.Invoke(coins);
        }

        public void ReduceCoins(int reduceAmount)
        {
            coins -= reduceAmount;
            OnReduceCoins?.Invoke(coins);
        }

        public void IncreaseCoins(int increaseAmount)
        {
            coins += increaseAmount;
            OnIncreaseCoins?.Invoke(coins);
        }

        public int Coins { get => coins; }
        public List<ACard> Cards { get => cards; set => cards = value; }
        public string PlayerName { get => playerName; set => playerName = value; }
        public bool IsMine { get => isMine; set => isMine = value; }
        public bool HasShield { get => hasShield; set => hasShield = value; }
    }
}