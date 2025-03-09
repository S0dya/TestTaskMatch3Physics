using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class InputManager : MonoBehaviour
    {
        [Inject] private GameplayController _gameplayController;

        private void Update()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                _gameplayController.DropBall();
        }
    }
}
