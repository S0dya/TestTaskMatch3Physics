using System;
using UnityEngine;

namespace Windows
{
    public class WindowsController : MonoBehaviour
    {
        [Serializable]
        class WindowsObjects
        {
            [SerializeField] private GameObject[] windows;

            public GameObject[] Windows => windows;
        }

        [SerializeField] private WindowsObjects[] windowsObjects;
        [SerializeField] private int initialWindow;

        private int _curIndex = -1;

        private void Start()
        {
            OpenWindow(initialWindow);
        }

        public void OpenWindow(int index) // can rewrite to enums
        {
            if (_curIndex == index) return;

            CloseWindows();
            ToggleWindows(index, true);

            _curIndex = index;
        }

        private void CloseWindows()
        {
            for (int i = 0; i < windowsObjects.Length; i++) ToggleWindows(i, false);
        }

        private void ToggleWindows(int index, bool toggle)
        {
            foreach (var window in windowsObjects[index].Windows)
            {
                window.SetActive(toggle);
            }
        }
    }
}
