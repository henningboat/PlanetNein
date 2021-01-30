using System;
using PlanetNein.Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.Utils
{
    public class EnableInGameState : MonoBehaviour
    {
        [SerializeField] private GameLoop.GameState _requiredState;

        private void Update()
        {
            gameObject.SetActive(_requiredState==GameLoop.Instance.CurrentGameState);
        }
    }
}
