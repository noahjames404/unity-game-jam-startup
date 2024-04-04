using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.deathbox.jam
{
    public abstract class ACard : ScriptableObject
    {
        public abstract void Execute(CardParam param);
    }

    public class CardParam
    {
        Player caster;
        Player target;
        DeathBox box;

        public Player Caster { get => caster; set => caster = value; }
        public Player Target { get => target; set => target = value; }
        public DeathBox Box { get => box; set => box = value; }
    }
}