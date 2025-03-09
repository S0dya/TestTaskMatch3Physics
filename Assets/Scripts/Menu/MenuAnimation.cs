using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class MenuAnimation : MonoBehaviour
    {
        [SerializeField] private Transform boxTransform;
        [SerializeField] private Vector3 maxScale = Vector3.one;
        [SerializeField] private float scaleDuration = 1f;
        [Space]
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int objectCount = 5;
        [SerializeField] private Vector2 spawnAreaSize = new (3f, 3f);
        [SerializeField] private float objectSpeed = 2f;

        private Tween _scaleTween;
        private List<Rigidbody2D> _spawnedObjects = new();
        private Color[] _colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan };

        private void Awake()
        {
            CreateObjects();
        }

        public void StartAnimation()
        {
            StartScaling();
            ResetObjects();
        }
        public void StopAnimation()
        {
            _scaleTween?.Kill();
        }

        private void StartScaling()
        {
            _scaleTween = boxTransform.DOScale(maxScale, scaleDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void CreateObjects()
        {
            for (int i = 0; i < objectCount; i++)
            {
                var obj = Instantiate(objectPrefab, transform);
                obj.GetComponent<SpriteRenderer>().color = _colors[Random.Range(0, _colors.Length)];

                var rb = obj.GetComponent<Rigidbody2D>();
                _spawnedObjects.Add(rb);
            }
        }

        private void ResetObjects()
        {
            foreach (var rb in _spawnedObjects)
            {
                rb.transform.position = new Vector2(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                    Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
                ) + (Vector2)boxTransform.position;

                var randomDirection = new Vector2(
                    Random.Range(-1f, 1f), 
                    Random.Range(-1f, 1f));

                rb.AddForce(randomDirection * objectSpeed);
            }
        }
    }
}
