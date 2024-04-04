using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    //turns the box into it's opposite e.g from bad to good. 
    public class ReverseCard : ACard
    {
        public override void Execute(CardParam param)
        {
            param.Box.IsBad = !param.Box.IsBad;
        }
    }
}
