
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using DiceGame.Events;
using TMPro;

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
                Debug.Log("Dice Rolling..");

                gameModel.RollCountIncrease();

                for (int i = 0; i < 3; i++)
                {
                    gameModel.Dices[i] = Random.Range(1, 7);
                }

                gameModel.CalculateDiceTotal(gameModel.Dices[0], gameModel.Dices[1], gameModel.Dices[2]);
                gameModel.CalculateTotalSum();
                UpdateView();
            }
        }
    }
}
