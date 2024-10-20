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
        #region Datas
        // Variables
        private const int MAX_ROLL_COUNT = 20;
        private int totalSum = 0;
        private int rollCount = 0;
        private int diceTotal = 0;
        private int toReachSum = 200;

        // Arrays
        private int[] selectedNumbers = new int[3];
        private int[] dices = new int[3];

        // Dice Condition Reference
        private bool isFirstRiggedDiceComplete;
        private bool isSecondRiggedDiceComplete;
        private bool isThirdRiggedDiceComplete;

        #endregion

        #region Getter Setter

        public int[] SelectedNumbers { get => selectedNumbers; set => selectedNumbers = value; }
        public int[] Dices { get => dices; set => dices = value; }
        public int TotalSum { get => totalSum; set => totalSum = value; }
        public int RollCount { get => rollCount; set => rollCount = value; }
        public int DiceTotal { get => diceTotal; set => diceTotal = value; }
        public bool IsFirstRiggedDiceComplete { get => isFirstRiggedDiceComplete; set => isFirstRiggedDiceComplete = value; }
        public bool IsSecondRiggedDiceComplete { get => isSecondRiggedDiceComplete; set => isSecondRiggedDiceComplete = value; }
        public bool IsThirdRiggedDiceComplete { get => isThirdRiggedDiceComplete; set => isThirdRiggedDiceComplete = value; }
        public int ToReachSum { get => toReachSum; set => toReachSum = value; }

        #endregion

        public void CalculateTotalSum()
        {
            totalSum += diceTotal;
        }

        public void RollCountIncrease()
        {
            rollCount += 1;
        }

        public void CalculateDiceTotal(int dice1, int dice2, int dice3)
        {
            diceTotal = dice1 + dice2 + dice3;
        }

        public bool CanRoll()
        {
            return rollCount < MAX_ROLL_COUNT;
        }

       
    }
}
