using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public class SeeThroughCard : ACard
    {
        public override void Execute(CardParam param)
        {
            Debug.Log($"Is it a bad Box: {param.Box.IsBad}");
        }
    }
}