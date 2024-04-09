using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class BoxValueCard : ACard
    {
        public override void Execute(CardParam param)
        {
            Debug.Log($"The value of the Box is : {param.Box.Amount}");
        }
    }
}