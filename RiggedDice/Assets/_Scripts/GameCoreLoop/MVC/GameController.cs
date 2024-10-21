using UnityEngine;
using DiceGame.Events;
using System.Collections;

namespace DiceGame.MVC
{
    public class GameController : MonoBehaviour
    {
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

                var riggedDiceChance = Random.Range(1, 3);

                if (riggedDiceChance == 1)
                {
                    RiggedDiceChecker(); 
                }                            
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

                gameView.UpdateTexts(gameModel.DiceTotal, gameModel.RollCount, gameModel.TotalSum, gameModel.Dices);
                gameModel.CalculateTotalSum();
                
            }
        }

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

        private bool RandomDicesRiggedSumChecker(int remainRandomRollCount)
        {
            return (remainRandomRollCount - 1 ) * 18 < (gameModel.RemainTotal - gameModel.DiceTotal) || (gameModel.RemainTotal - gameModel.DiceTotal) < (remainRandomRollCount - 1 )* 3;
        }

       
    }
}
