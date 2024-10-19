using DiceGame.Events;
using DiceGame.MVC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiceGame
{
    public class GameView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Button throwButton;

        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI totalSumText;
        [SerializeField] private TextMeshProUGUI rollCountText;
        [SerializeField] private TextMeshProUGUI diceTotalText;
        [SerializeField] private TextMeshProUGUI[] dicesTexts;

        private void Awake()
        {
            throwButton.onClick.AddListener(() =>
            {
                EventManager.OnRollDice?.Invoke();  
            });
        }
  
        public void UpdateTotalSumText(int sum)
        {
            totalSumText.text = sum.ToString();
        }

        public void UpdateRollCountText(int count)
        {
            rollCountText.text = count.ToString();
        }

        public void UpdateDiceTotalText(int diceTotal)
        {
            diceTotalText.text = diceTotal.ToString();  
        }

        public void UpdateDiceTexts(int[] dices)
        {
            for (int i = 0; i < dices.Length; i++)
            {
                dicesTexts[i].text = dices[i].ToString();   
            }
        }
    }
}
