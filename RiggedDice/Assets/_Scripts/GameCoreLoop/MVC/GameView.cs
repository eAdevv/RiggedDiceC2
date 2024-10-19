using DiceGame.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiceGame
{
    public class GameView : MonoBehaviour
    {
        [Header("MVC References")]
        [SerializeField] private GameController gameController;

        [Header("UI References")]
        [SerializeField] private Button throwButton;

        private void Awake()
        {
            throwButton.onClick.AddListener(() =>
            {
                gameController?.RollDice();
            });
        }
    }
}
