using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class InputManager : MonoBehaviour
    {
        [Inject] private GameplayController _gameplayController;

        private void Update()
        {
#if UNITY_EDITOR
            HandleMouseInput();
#elif UNITY_ANDROID
            HandleTouchInput();
#endif
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0)) _gameplayController.DropBall();
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    _gameplayController.DropBall();
            }
        }
    }
}
