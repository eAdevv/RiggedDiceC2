using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DiceGame.Events;
using Unity.VisualScripting;
using System.Collections;

namespace DiceGame.MVC
{
    public class GameView : MonoBehaviour
    {
        //////  The VIEW part of the architecture for the MVC pattern //////
        ///
        [Header("UI References")]
        [SerializeField] private Button rollButton;
        [SerializeField] private GameObject gameFnishPanel;

        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI totalSumText;
        [SerializeField] private TextMeshProUGUI rollCountText;
        [SerializeField] private TextMeshProUGUI diceTotalText;
        [SerializeField] private TextMeshProUGUI[] dicesTexts;

        public TextMeshProUGUI[] DicesTexts { get => dicesTexts; set => dicesTexts = value; }

        private void Awake()
        {
            rollButton.onClick.AddListener(() =>
            {
                StartCoroutine(RollAnimation());
            });
        }
  
        public void GameFnishUpdate()
        {
            gameFnishPanel.SetActive(true);
            rollButton.interactable = false;
        }

        // Texts are updated
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

        // Animation to be activated when the dice is rolled.
        private IEnumerator RollAnimation()
        {
            rollButton.interactable = false;
            for (int i = 0; i < 3; i++)
            {
                DicesTexts[0].text = Random.Range(1, 7).ToString();
                DicesTexts[1].text = Random.Range(1, 7).ToString();
                DicesTexts[2].text = Random.Range(1, 7).ToString();
                yield return new WaitForSeconds(0.1f);
            }
            rollButton.interactable = true;
            EventManager.OnRollDice?.Invoke();
        }

    }

}
