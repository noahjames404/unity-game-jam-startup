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
        List<Card> cards;
        [SerializeField]
        string playerName;
        [SerializeField]
        bool isMine = false;

        public int Coins { get => coins; set => coins = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
        public string PlayerName { get => playerName; set => playerName = value; }
        public bool IsMine { get => isMine; set => isMine = value; }
    }
}