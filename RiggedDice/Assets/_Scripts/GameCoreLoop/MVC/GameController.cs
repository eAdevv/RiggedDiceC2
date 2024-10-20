
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using DiceGame.Events;
using TMPro;
using System.Linq;
using System.Net.Http.Headers;

namespace DiceGame.MVC
{
    public class GameController : MonoBehaviour
    {
        [Header("MVC References")]
        [SerializeField] private GameModel gameModel;
        [SerializeField] private GameView  gameView;

        private int[] diceRollResults = new int[20];
        private int remainingTotal = 200;

        private void OnEnable()
        {
            EventManager.OnRollDice += RollDice;
        }

        private void OnDisable()
        {
            EventManager.OnRollDice -= RollDice;
        }

        private void UpdateView()
        {
            gameView.UpdateDiceTotalText(gameModel.DiceTotal);
            gameView.UpdateRollCountText(gameModel.RollCount);
            gameView.UpdateTotalSumText(gameModel.TotalSum);
            gameView.UpdateDiceTexts(gameModel.Dices);
        }

        public void RollDice()
        {
            if(gameModel.CanRoll())
            {
                gameModel.RollCountIncrease();

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
                            RollRandomDices();
                            break;
                    }
                }

                gameModel.CalculateDiceTotal(gameModel.Dices[0], gameModel.Dices[1], gameModel.Dices[2]);
                gameModel.CalculateTotalSum();
                UpdateView();

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
                    RollRandomDices();
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


        private void RollRandomDices()
        {
            for (int i = 0; i < 3; i++)
            {
                gameModel.Dices[i] = Random.Range(1, 7);
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
        }
    }
}
