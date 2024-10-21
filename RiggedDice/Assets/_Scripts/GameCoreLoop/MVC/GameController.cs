using UnityEngine;
using DiceGame.Events;
using System.Collections;

namespace DiceGame.MVC
{
    public class GameController : MonoBehaviour
    {
        //////  The CONTROLLER part of the architecture for the MVC pattern //////
        ///
        [Header("MVC References")]
        [SerializeField] private GameModel gameModel;
        [SerializeField] private GameView  gameView;

        private void OnEnable()
        {
            EventManager.OnRollDice += RollDice;
        }

        private void OnDisable()
        {
            EventManager.OnRollDice -= RollDice;
        }
        public void RollDice()
        {
            if(gameModel.CanRoll())
            {
                gameModel.RollCount++;

                // Every roll of the dice has a 50% chance of being rigged.
                var riggedDiceChance = Random.Range(1, 3);

                if (riggedDiceChance == 1)
                {
                    RiggedDiceChecker(); 
                }
                // After the dice are rolled, the rollCount is checked to see if it is the end of the range.
                else
                {
                    switch (gameModel.RollCount)
                    {
                        case int rollCount when rollCount == 10 && !gameModel.IsFirstRiggedDiceComplete:
                            OnRiggedDice(gameModel.SelectedNumbers[0]);
                            gameModel.IsFirstRiggedDiceComplete = true;
                            break;

                        case int rollCount when rollCount == 15 && !gameModel.IsSecondRiggedDiceComplete:
                            OnRiggedDice(gameModel.SelectedNumbers[1]);
                            gameModel.IsSecondRiggedDiceComplete = true;
                            break;

                        case int rollCount when rollCount == 20 && !gameModel.IsThirdRiggedDiceComplete:
                            OnRiggedDice(gameModel.SelectedNumbers[2]);
                            gameModel.IsThirdRiggedDiceComplete = true;
                            break;

                        default:
                            RollRandomDices(gameModel.RemainRandomRollCount);
                            break;
                    }
                }

                gameModel.CalculateTotalSum();
                gameView.UpdateTexts(gameModel.DiceTotal, gameModel.RollCount, gameModel.TotalSum, gameModel.Dices);
            }
        }

        // The interval is checked to check whether the shot is fraudulent or not.
        // If the number of shots is within the given range, a trick shot will occur.
        private void RiggedDiceChecker()
        {
            switch (gameModel.RollCount)
            {
                case int rollCount when isNumberInRange(rollCount, 1, 10) && !gameModel.IsFirstRiggedDiceComplete:
                    OnRiggedDice(gameModel.SelectedNumbers[0]);
                    gameModel.IsFirstRiggedDiceComplete = true;
                    break;

                case int rollCount when isNumberInRange(rollCount, 5, 15) && !gameModel.IsSecondRiggedDiceComplete:
                    OnRiggedDice(gameModel.SelectedNumbers[1]);
                    gameModel.IsSecondRiggedDiceComplete = true;
                    break;

                case int rollCount when isNumberInRange(rollCount, 10, 20) && !gameModel.IsThirdRiggedDiceComplete:
                    OnRiggedDice(gameModel.SelectedNumbers[2]);
                    gameModel.IsThirdRiggedDiceComplete = true;
                    break;

                default:
                    RollRandomDices(gameModel.RemainRandomRollCount);
                    break;
            }
        }

        private bool isNumberInRange(int rollCount, int min, int max)
        {
            if (rollCount >= min && rollCount <= max)
                return true;
            else
                return false;
        }

        // Rolls 3 dice according to the remaining number of random dice rolls and calculates the dice total. 
        // If the dice total is not valid according to the remaining number of rolls , the dice are re-rolled. 
        // If the dice total is valid, the remaining number of dice throws and the total are updated. 
        // If this is the last roll, the rigged dice function is activated
        private void RollRandomDices(int remainRandomRollCount)
        {
            if (remainRandomRollCount > 1)
            {
                do
                {
                    gameModel.Dices[0] = Random.Range(1, 7);
                    gameModel.Dices[1] = Random.Range(1, 7);
                    gameModel.Dices[2] = Random.Range(1, 7);

                    gameModel.CalculateDiceTotal(gameModel.Dices[0], gameModel.Dices[1], gameModel.Dices[2]);

                } while (RandomDicesRiggedSumChecker(remainRandomRollCount));

                gameModel.RemainRandomRollCount -= 1;
                gameModel.RemainTotal -= gameModel.DiceTotal;
            }
            else
            {
                OnRiggedDice(gameModel.RemainTotal);
                gameModel.RemainRandomRollCount -= 1;
                gameView.GameFnishUpdate();
            }
        }

        // The basic logic of a rigged throw;
        // The value obtained from the first two dice is subtracted from the last dice
        // The dices are rolled until the last dice result is between 1 and 6.
        private void OnRiggedDice(int targetNumber)
        {
            Debug.Log(gameModel.RollCount + ". Roll is Rigged.");

            int lastDice;
            do
            {
                gameModel.Dices[0] = Random.Range(1, 7);
                gameModel.Dices[1] = Random.Range(1, 7);
                lastDice = targetNumber - (gameModel.Dices[0] + gameModel.Dices[1]);

            } while (lastDice < 1 || lastDice > 6);

            gameModel.Dices[2] = lastDice;
            gameModel.CalculateDiceTotal(gameModel.Dices[0], gameModel.Dices[1], gameModel.Dices[2]);
        }

        // Checks whether the dice total is valid according to the number of remaining throws.
        // The dice total must be within a certain range according to the number of remaining throws. 
        // If the dice total is greater or less, the dice will be re-rolled.
        private bool RandomDicesRiggedSumChecker(int remainRandomRollCount)
        {
            return (remainRandomRollCount - 1 ) * 18 < (gameModel.RemainTotal - gameModel.DiceTotal) || (gameModel.RemainTotal - gameModel.DiceTotal) < (remainRandomRollCount - 1 )* 3;
        }

       
    }
}
