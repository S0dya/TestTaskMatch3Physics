using UnityEngine;
using UnityEngine.UI;
using Windows;
using Zenject;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button nextWindowButton;
        [Space]
        [SerializeField] private MenuAnimation menuAnimation;

        [Inject] private WindowsController _windowsController;

        private void Awake()
        {
            nextWindowButton.onClick.AddListener(() => _windowsController.OpenWindow(1));
        }

        private void OnEnable()
        {
            menuAnimation.StartAnimation();
        }
        private void OnDisable()
        {
            menuAnimation.StopAnimation();
        }
    }
}