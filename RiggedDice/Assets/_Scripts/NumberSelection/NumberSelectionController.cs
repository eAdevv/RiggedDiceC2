using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DiceGame.Events;

namespace DiceGame.NumberSelection
{
    public class NumberSelectionController : MonoBehaviour
    {
        [SerializeField] private ButtonNumberHandler[] numberHandlers;
        [SerializeField] private TextMeshProUGUI[] numberTexts;
        [SerializeField] private Button gameStartButton;
        [SerializeField] private GameObject numberSelectionPanel;
        [SerializeField] private GameObject gamePanel;

        private int numberSelectCount = 0;
        private int[] numbers = new int[3];

      
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
                numbers[numberSelectCount] = numberHandler.Number;
                numberSelectCount++;
                if(numberSelectCount > 2) gameStartButton.interactable = true;
            }
        }

        private void GameStart()
        {
            numberSelectionPanel.SetActive(false);
            gamePanel.SetActive(true);
            EventManager.OnNumbersAssigment?.Invoke(numbers);
            EventManager.OnInitialize?.Invoke();
        }

      
    }
}
