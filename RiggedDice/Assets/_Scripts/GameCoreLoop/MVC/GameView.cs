using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DiceGame.Events;
using Unity.VisualScripting;

namespace DiceGame.MVC
{
    public class GameView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Button rollButton;
        [SerializeField] private GameObject gameFnishPanel;

        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI totalSumText;
        [SerializeField] private TextMeshProUGUI rollCountText;
        [SerializeField] private TextMeshProUGUI diceTotalText;
        [SerializeField] private TextMeshProUGUI[] dicesTexts;


        private void Awake()
        {
            rollButton.onClick.AddListener(() =>
            {
                EventManager.OnRollDice?.Invoke();  
            });
        }
  
        public void GameFnishUpdate()
        {
            gameFnishPanel.SetActive(true);
            rollButton.interactable = false;
        }
        public void UpdateTexts(int diceTotal, int count , int sum, int[] dices)
        {
            totalSumText.text = sum.ToString();
            rollCountText.text = count.ToString();
            diceTotalText.text = diceTotal.ToString();
            for (int i = 0; i < dices.Length; i++)
            {
                dicesTexts[i].text = dices[i].ToString();
            }
        }
        
    }

}
