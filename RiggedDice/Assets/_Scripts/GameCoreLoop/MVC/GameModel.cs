using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DiceGame.Singleton;

namespace DiceGame.MVC
{
    public class GameModel : MonoSingleton<GameModel>
    {
        private int totalSum = 0;
        private int throwCount = 0;
        private int diceTotal = 0;
        private int[] selectedNumbers = new int[3];

        public int[] SelectedNumbers { get => selectedNumbers; set => selectedNumbers = value; }
        public int TotalSum { get => totalSum; set => totalSum = value; }
        public int ThrowCount { get => throwCount; set => throwCount = value; }
        public int DiceTotal { get => diceTotal; set => diceTotal = value; }

        private void OnTotalSumIncrease(int diceTotal)
        {
            totalSum += diceTotal;
        }

        private void OnThrowCountIncrease()
        {
            throwCount += 1;
        }

        private void OnDiceTotal(int number1, int number2, int number3)
        {
            diceTotal = number1 + number2 + number3;
        }
    }
}
