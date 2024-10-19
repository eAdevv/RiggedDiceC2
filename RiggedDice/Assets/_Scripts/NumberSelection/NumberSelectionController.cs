using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DiceGame.MVC;

namespace DiceGame.NumberSelection
{
    public class NumberSelectionController : MonoBehaviour
    {
        [SerializeField] private ButtonNumberHandler[] numberHandlers;
        [SerializeField] private TextMeshProUGUI[] numberTexts;
        [SerializeField] private Button gameStartButton;
        int numberSelectCount = 0;
        [SerializeField] private GameObject numberSelectionPanel;
        [SerializeField] private GameObject gamePanel;
        private void Start()
        {
            numberSelectionPanel.SetActive(true);

            foreach (var numberHandler in numberHandlers)
            {
                numberHandler.GetComponent<Button>().onClick.AddListener(()=> GetNumber(numberHandler));
            }

            gameStartButton.onClick.AddListener(GameStart);
        }
        private void GetNumber(ButtonNumberHandler numberHandler)
        {
            if(numberSelectCount <= 2)
            {
                numberTexts[numberSelectCount].text = numberHandler.Number.ToString();
                GameModel.Instance.SelectedNumbers[numberSelectCount] = numberHandler.Number;
                numberSelectCount++;
            }
        }

        private void GameStart()
        {
            numberSelectionPanel.SetActive(false);
            gamePanel.SetActive(true);
        }

      
    }
}
