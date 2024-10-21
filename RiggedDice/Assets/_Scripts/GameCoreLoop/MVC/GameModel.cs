using UnityEngine;
using DiceGame.Events;

namespace DiceGame.MVC
{
    public class GameModel : MonoBehaviour
    {
        ////// Where the data in the architecture for the MVC pattern is collected //////
        #region Datas
        // Variables
        private const int MAX_ROLL_COUNT = 20;
        private const int MAX_SUM = 200;
        private int totalSum = 0;
        private int rollCount = 0;
        private int diceTotal = 0;
     
        private int remainRandomRollCount;
        private int remainTotal;
        private int numbersTotal;

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
        public int RemainRandomRollCount { get => remainRandomRollCount; set => remainRandomRollCount = value; }
        public int RemainTotal { get => remainTotal; set => remainTotal = value; }
        public bool IsFirstRiggedDiceComplete { get => isFirstRiggedDiceComplete; set => isFirstRiggedDiceComplete = value; }
        public bool IsSecondRiggedDiceComplete { get => isSecondRiggedDiceComplete; set => isSecondRiggedDiceComplete = value; }
        public bool IsThirdRiggedDiceComplete { get => isThirdRiggedDiceComplete; set => isThirdRiggedDiceComplete = value; }

        #endregion

        private void OnEnable()
        {
            EventManager.OnInitialize += InitializeAtStart;
            EventManager.OnNumbersAssigment += SelectedNumbersAssignment;
        }

        private void OnDisable()
        {
            EventManager.OnInitialize -= InitializeAtStart;
            EventManager.OnNumbersAssigment -= SelectedNumbersAssignment;
        }
        public void CalculateTotalSum()
        {
            totalSum += diceTotal;
        } 
        public void CalculateDiceTotal(int dice1, int dice2, int dice3)
        {
            diceTotal = dice1 + dice2 + dice3;
        }
        public bool CanRoll()
        {
            return rollCount < MAX_ROLL_COUNT;
        }

        private void InitializeAtStart()
        {
            remainRandomRollCount = MAX_ROLL_COUNT - dices.Length;

            foreach (var dice in selectedNumbers)
            {
                numbersTotal += dice;
            }

            remainTotal = MAX_SUM - numbersTotal; 
        }

        private void SelectedNumbersAssignment(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                selectedNumbers[i] = numbers[i];
            }
        }

    }
}
