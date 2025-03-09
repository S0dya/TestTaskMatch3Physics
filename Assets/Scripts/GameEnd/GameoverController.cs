using Datas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Windows;
using Zenject;

namespace GameEnd
{
    public class GameoverController : MonoBehaviour
    {
        [SerializeField] private Button nextWindowButton;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Inject] private WindowsController _windowsController;

        private void Awake()
        {
            nextWindowButton.onClick.AddListener(() => _windowsController.OpenWindow(0));
        }

        private void OnEnable()
        {
            scoreText.text = GameData.GameResults.Score.ToString();
        }
    }
}
