using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.deathbox.jam
{
    public class UIControls : MonoBehaviour, IUIControls
    {
        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        List<GameObject> gameObjects = new List<GameObject>();

        bool isLocked = false;

        public void Draw()
        {
            if(isLocked) return;
            gameManager.Draw();
        }

        public void Open()
        {
            if (isLocked) return;
            gameManager.Open();
        }

        public void Pass()
        {
            if (isLocked) return;
            gameManager.Pass();
        }

        public void AddCardToHand(ACard drawnCard, GameObject cardObject, CardParam cardParam, Player activePlayer)
        {
            // Instantiate a card UI element and add it to the player's hand
            gameObjects.Add(cardObject);
            int cardID = cardObject.GetInstanceID();

            // Add a button component to the card object
            Button button = cardObject.AddComponent<Button>();
            button.onClick.AddListener(() => OnCardClicked(drawnCard, cardParam, activePlayer, cardID));
        }

        private void OnCardClicked(ACard card, CardParam cardParam, Player activePlayer, int cardID)
        {
            //card.Execute(cardParam);

            // Remove the card from the player's hand
            activePlayer.Cards.Remove(card);

            // Destroy the card UI element
            Destroy(gameObjects.Find(cardObject => cardObject.GetInstanceID() == cardID));
        }

        private void Awake()
        {
            gameManager.OnActivePlayer += OnActivePlayer;
            gameManager.OnNextActivePlayer += OnNextActivePlayer;
        }

        private void OnNextActivePlayer(Player player)
        {
            if (!player.IsMine)
            {
                gameObjects.ForEach(gameObject => gameObject.SetActive(false));
            }
        }

        private void OnActivePlayer(Player player)
        {
            isLocked = !player.IsMine;

            if (player.IsMine)
            {
                gameObjects.ForEach(gameObject => gameObject.SetActive(true));
            } 
        }


    }

    interface IUIControls
    {
        void Pass();
        void Draw();
        void Open();
    }
}
