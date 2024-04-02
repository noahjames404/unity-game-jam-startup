using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace com.deathbox.jam
{
    public class GameManager : MonoBehaviour, IUIControls
    {
        [SerializeField]
        int startingCoin = 100;

        [SerializeField]
        int drawCost;
        [SerializeField]
        int passCost;

        [SerializeField]
        float riskMultiplier = 1;

        static GameManager instance;

        [SerializeField]
        Player activePlayer;
        [SerializeField]
        List<Player> players;
        [SerializeField]
        DeathBox deathbox;
        Queue<Player> playersQueue = new();

        public event Action<Player> OnActivePlayer;
        public event Action<Player> OnNextActivePlayer;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            LoadPlayers();
            ShuffleFirstMove();
            CreateDeathbox();
            InitiateNextMove();
        }

        private void InitiateNextMove()
        {
            activePlayer = playersQueue.Dequeue();
            OnActivePlayer?.Invoke(activePlayer);
            Debug.Log($"Player {activePlayer.PlayerName} turn");
        }

        void CreateDeathbox()
        { 
            deathbox = DeathBox.CreateBox(5,10, riskMultiplier);
        }

        public void FinishMove()
        {
           
            StartCoroutine(NextMoveRoutine());
        }

        IEnumerator NextMoveRoutine()
        {
            OnNextActivePlayer?.Invoke(playersQueue.Peek());
            yield return new WaitForSeconds(1f);
            playersQueue.Enqueue(activePlayer);
            InitiateNextMove(); 
        }

        void LoadPlayers()
        {
            players.Add(new Player()
            {
                Coins = startingCoin,
                Cards = new List<Card>(),
                PlayerName = "You",
                IsMine = true
            });

            players.Add(new Player()
            {
                Coins = startingCoin,
                Cards = new List<Card>(),
                PlayerName = "Evil Person"
            });

            Debug.Log("Loading Players");
        }

        void ShuffleFirstMove()
        { 
            int index = UnityEngine.Random.Range(0, players.Count);
            QueueByIndex(index);

            switch (index)
            {
                case 0:
                    QueueByIndex(1);
                    break;
                default:
                    QueueByIndex(0);
                    break;
            }

            Debug.Log("Shuffling Turn");
        }

        public void Draw()
        {
            //draw card 
            ReduceCoin(drawCost);
            FinishMove();
        }

        public void Pass()
        {
            riskMultiplier += 0.1f;
            deathbox.Amount += (int)(deathbox.Amount * 0.1f);
            ReduceCoin(passCost);
            FinishMove();
        } 

        public void Open()
        {
            if(deathbox.Unbox(out int amount))
            {
                Debug.Log($"{activePlayer.PlayerName} Receives Damage after opening box: {amount}");
                ReduceCoin(amount);
            }
            else
            {
                activePlayer.Coins += amount;
                Debug.Log($"{activePlayer.PlayerName} Receives Coins after opening box: {amount}");
            }
            CreateDeathbox();
            FinishMove();
        }

        public void ReduceCoin(int amount)
        {
            activePlayer.Coins -= amount; 
            if (activePlayer.Coins <= 0)
            {
                Debug.LogError($"{activePlayer.PlayerName} is Dead!");
            }
        }

        void QueueByIndex(int index)
        {
            Player player = players[index];
            playersQueue.Enqueue(player);
        }

        public static GameManager GetInstance()
        {
            return instance;
        } 
    }
}