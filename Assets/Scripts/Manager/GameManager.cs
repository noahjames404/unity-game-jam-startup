using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        int currentRound = 1;

        [SerializeField]
        TextMeshProUGUI  coinsText;
        [SerializeField]
        TextMeshProUGUI  roundText;
        Vector3 infoVector = Vector3.zero;

        static GameManager instance;

        [SerializeField]
        Player activePlayer;
        [SerializeField]
        List<Player> players;
        [SerializeField]
        DeathBox deathbox;
        [SerializeField]
        GameObject cardPrefab;
        RestartScreen restartScreen;
        UIControls uIControls;
        Queue<Player> playersQueue = new();

        public event Action<Player> OnActivePlayer;
        public event Action<Player> OnNextActivePlayer;
        public GameObject canvas;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            uIControls = FindObjectOfType<UIControls>();
            PrepareDisplayInfo();
            LoadPlayers();
            ShuffleFirstMove();
            CreateDeathbox();
            InitiateNextMove();
            UpdatePlayerInfo();
        }

        private void PrepareDisplayInfo(){
            GameObject coinsObject = GameObject.Find("Coins");
            coinsText = coinsObject.GetComponent<TextMeshProUGUI>();
            GameObject roundObject = GameObject.Find("Round");
            roundText = roundObject.GetComponent<TextMeshProUGUI>();
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
            string activeTurn = (activePlayer.IsMine ? "Opponents" : "Your") + " turn";
            TextManager.CreateTextMeshPro(activeTurn, infoVector, canvas.transform, 1f);
            StartCoroutine(NextMoveRoutine());
            UpdatePlayerInfo();
        }

        IEnumerator NextMoveRoutine()
        {
            OnNextActivePlayer?.Invoke(playersQueue.Peek());
            yield return new WaitForSeconds(1.5f);
            playersQueue.Enqueue(activePlayer);
            InitiateNextMove(); 
        }

        void LoadPlayers()
        {
            players.Add(new Player(startingCoin)
            { 
                Cards = new List<ACard>(),
                PlayerName = "You",
                IsMine = true
            });

            players.Add(new Player(startingCoin)
            { 
                Cards = new List<ACard>(),
                PlayerName = "Opponent"
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
            if(activePlayer.Cards.Count < 5){
                ReduceCoin(drawCost);
                drawCard();
                FinishMove();
            } else 
            {
                TextManager.CreateTextMeshPro("You can't draw more Cards", infoVector, canvas.transform, 1f);
            }
        }

        void drawCard()
        {
            //Create random card
            ACard drawnCard = CardFactory.CreateRandomCard();
            Debug.LogError(drawnCard);

            //Fill CardParam
            CardParam cardParam = new CardParam();
            cardParam.Caster = activePlayer; 
            cardParam.Target = null; 
            cardParam.Box = deathbox; 
            activePlayer.Cards.Add(drawnCard);

            if (activePlayer.IsMine){
                //Instantiate the card as a GO
                Transform panelTransform = canvas.transform.Find("Panel");
                GameObject cardObject = Instantiate(cardPrefab, panelTransform);
                uIControls.AddCardToHand(drawnCard, cardObject, cardParam, activePlayer);
            }

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
            Open(activePlayer);
        }

        public void Open(Player player)
        {
            if (deathbox.Unbox(out int amount))
            {
                Debug.Log($"{player.PlayerName} Receives Damage after opening box: {amount}");
                ReduceCoin(amount);
            }
            else
            {
                player.IncreaseCoins(amount);
                Debug.Log($"{player.PlayerName} Receives Coins after opening box: {amount}");
            }
            CreateDeathbox();
            FinishMove();
        }

        public void ReduceCoin(int amount)
        {
            activePlayer.ReduceCoins(amount); 
            if (activePlayer.IsMine && activePlayer.Coins <= 0)
            {
                ShowRestartScreen();
                TextManager.CreateTextMeshPro("GameOver", infoVector, canvas.transform, 1f);
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

         void UpdatePlayerInfo()
        {
            if(activePlayer.IsMine){
                coinsText.text = "Coins: " + activePlayer.Coins.ToString();
                currentRound++;
            }
            roundText.text = "Round: " + currentRound.ToString();
        }

        void ShowRestartScreen()
        {
            if (restartScreen == null)
            {
                restartScreen = new GameObject("RestartScreen").AddComponent<RestartScreen>();
            }
            restartScreen.Show();
        }
    }
}
