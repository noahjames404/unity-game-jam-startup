using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class AIManager : MonoBehaviour
    {
        [SerializeField]
        GameManager gameManager; 
        

        private void Awake()
        {
            gameManager.OnActivePlayer += OnActivePlayer;
        }

        private void OnActivePlayer(Player player)
        {
            if (player.IsMine) return;

            int pick = UnityEngine.Random.Range(0,100);

            if(pick > 90)
            {
                gameManager.Open();
            }else if(pick > 50)
            {
                gameManager.Draw();
            }else 
            {
                gameManager.Pass();
            }
        } 
    }
}
