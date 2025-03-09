using UnityEngine;

namespace Cameras
{
    public class CameraFOVChanger : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras;
        [SerializeField] private float orthographicFactor = 3f;
        [SerializeField] private bool changeRuntime;

        public void Awake()
        {
            UpdateOrto();
        }

        private void UpdateOrto()
        {
            float screenRatio = (float)Screen.height / Screen.width;

            float orthographicSize = Mathf.Clamp(screenRatio * orthographicFactor, 5.2f, 7.5f);

            foreach (var camera in cameras)
                camera.orthographicSize = orthographicSize;
        }

        private void OnValidate()
        {
            if (Application.isPlaying == false) return;
            if (changeRuntime == false) return;

            UpdateOrto();
        }
    }
}