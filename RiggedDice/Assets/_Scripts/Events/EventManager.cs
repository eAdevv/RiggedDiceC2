using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DiceGame.Events
{
    public static class EventManager 
    {
        public static UnityAction OnTotalSumChange;
        public static UnityAction OnThrowCountChange;
        public static UnityAction OnDiceTotalChange;
    }
}
