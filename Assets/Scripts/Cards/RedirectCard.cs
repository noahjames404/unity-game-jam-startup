using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    //open the box and redirect the consequence to someone (wether if its good or bad)
    public class RedirectCard : ACard
    {
        GameManager gameManager => GameManager.GetInstance();
        public override void Execute(CardParam param)
        {
            gameManager.Open(param.Target);
        }
    }
}
