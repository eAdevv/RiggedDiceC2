using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiceGame.NumberSelection
{
    public class ButtonNumberHandler : MonoBehaviour
    {
        [SerializeField]protected int number;
        public int Number { get => number; set => number = value; }
    }
}
