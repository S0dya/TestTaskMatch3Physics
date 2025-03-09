using Infos;
using Tools;
using UnityEngine;

namespace Gameplay
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform ballsParent;
        [SerializeField] private Transform pendulumSpawnPoint;
        [SerializeField] private Rigidbody2D pendulumRb;

        private ObjectPool _ballsPool = new();

        private void Awake()
        {
            _ballsPool.Init(new[]{ ballPrefab }, ballsParent, 9, true);
        }

        public Ball SpawnBall(BallInfo ballInfo)
        {
            var ball = _ballsPool.Get().GetComponent<Ball>();
            ball.Init(ballInfo, pendulumRb);

            ball.transform.SetPositionAndRotation(
                pendulumSpawnPoint.position, 
                pendulumSpawnPoint.rotation);

            ball.ToggleJoint(true);

            ball.gameObject.SetActive(true);

            return ball;
        }

        public void ReturnBall(GameObject ballObject)
        {
            _ballsPool.Set(ballObject);
        }
    }
}
