using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DiceGame.Events;
using DiceGame.Singleton;

namespace DiceGame.MVC
{
    public class GameModel : MonoSingleton<GameModel>
    {
        private int totalSum = 0;
        private int throwCount = 0;
        private int diceTotal = 0;
        private int[] selectedNumbers = new int[3];
        private int[] dices = new int[3];

        public int[] SelectedNumbers { get => selectedNumbers; set => selectedNumbers = value; }
        public int TotalSum { get => totalSum; set => totalSum = value; }
        public int ThrowCount { get => throwCount; set => throwCount = value; }
        public int DiceTotal { get => diceTotal; set => diceTotal = value; }
        public int[] Dices { get => dices; set => dices = value; }

        public void TotalSumIncrease(int diceTotal)
        {
            totalSum += diceTotal;
            EventManager.OnTotalSumChange?.Invoke();
        }

        public void ThrowCountIncrease()
        {
            throwCount += 1;
            EventManager.OnThrowCountChange?.Invoke();
        }

        public void OnDiceTotal(int dice1, int dice2, int dice3)
        {
            diceTotal = dice1 + dice2 + dice3;
            EventManager.OnDiceTotalChange?.Invoke();
        }

       
    }
}
