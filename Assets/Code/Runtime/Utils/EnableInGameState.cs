using System;
using PlanetNein.Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.Utils
{
    public class EnableInGameState : MonoBehaviour
    {
        [SerializeField] private GameLoop.GameState _requiredState;
        [SerializeField] private Behaviour _target;

        private void Update()
        {
            _target.enabled=(_requiredState==GameLoop.Instance.CurrentGameState);
        }
    }
}
