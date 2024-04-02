using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
