
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

        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI totalSumText;
        [SerializeField] private TextMeshProUGUI throwCountText;
        [SerializeField] private TextMeshProUGUI diceTotalText;
        [SerializeField] private TextMeshProUGUI[] dicesTexts;
        
        private void OnEnable()
        {
            EventManager.OnTotalSumChange += OnTotalSumChange;   
            EventManager.OnThrowCountChange += OnThrowCountChange;
            EventManager.OnDiceTotalChange += OnDiceTotalChange;
        }

        private void OnDisable()
        {
            EventManager.OnTotalSumChange -= OnTotalSumChange;
            EventManager.OnThrowCountChange -= OnThrowCountChange;
            EventManager.OnDiceTotalChange -= OnDiceTotalChange;
        }

        private void OnTotalSumChange()
        {
            if (gameModel == null) return;
            UpdateView(totalSumText, gameModel.TotalSum);
        }
        private void OnThrowCountChange() 
        {
            if (gameModel == null) return;
            UpdateView(throwCountText, gameModel.ThrowCount);
        }
        private void OnDiceTotalChange()
        {
            if (gameModel == null) return;
            UpdateView(diceTotalText, gameModel.DiceTotal);
        }

        private void UpdateView(TextMeshProUGUI changedText,int number)
        {
            changedText.text = number.ToString();
        }

        public void RollDice()
        {
            Debug.Log("Dice Rolling..");

            for (int i = 0; i < 3; i++)
            {
                gameModel.Dices[i] = Random.Range(1, 6);
                UpdateView(dicesTexts[i], gameModel.Dices[i]);
            }

            gameModel?.OnDiceTotal(gameModel.Dices[0], gameModel.Dices[1], gameModel.Dices[2]);
            gameModel?.TotalSumIncrease(gameModel.DiceTotal);
            gameModel?.ThrowCountIncrease();
        }
    }
}
