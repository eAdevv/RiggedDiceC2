using UnityEngine.Events;

namespace DiceGame.Events
{
    public static class EventManager 
    {
        public static UnityAction OnRollDice;
        public static UnityAction OnInitialize;
        public static UnityAction<int[]> OnNumbersAssigment;
    }
}
