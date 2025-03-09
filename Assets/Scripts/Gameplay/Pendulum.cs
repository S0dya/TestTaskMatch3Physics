using UnityEngine;

namespace Gameplay
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private float swingSpeed = 2f;
        [SerializeField] private float maxAngle = 45f;
        
        [SerializeField] private Transform pendulumTransform;

        private float _swingTime;

        private void OnEnable()
        {
            _swingTime = 0;

            SetAngle();
        }

        private void Update()
        {
            _swingTime += Time.deltaTime * swingSpeed;

            SetAngle();
        }

        private void SetAngle()
        {
            float angle = maxAngle * Mathf.Sin(_swingTime);
            pendulumTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}