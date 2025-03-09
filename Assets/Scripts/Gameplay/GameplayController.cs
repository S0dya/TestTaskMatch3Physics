using Cysharp.Threading.Tasks;
using Datas;
using Gameplay.Effects;
using Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows;
using Zenject;

using Random = UnityEngine.Random;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Min(0.1f)] private float fallingDelay = 0.1f;
        [SerializeField, Min(0.1f)] private float gameoverDelay = 0.5f;
        [Space]
        [Header("Infos")]
        [SerializeField] private BallInfo[] ballsInfos;
        [Space]
        [Header("Game")]
        [SerializeField] private ColumnTrigger[] columnTriggers;
        [SerializeField] private BallSpawner ballSpawner;
        [Space]
        [Header("Effects")]
        [SerializeField] private GameObject destroyBallEffectPrefab;
        [SerializeField] private Transform destroyBallEffectsParent;
        
        [Inject] private WindowsController _windowsController;

        private List<Ball>[] _columnsBalls;
        private List<Ball> _activeBalls = new();

        private bool _canDropBall;
        private Ball _currentDropBall;

        private MatchFinder _matchFinder = new();

        private EffectsSpawner _effectsSpawner = new();

        private int _score;

        private void Awake()
        {
            _columnsBalls = new List<Ball>[3] { new(), new(), new() };

            for (int i = 0; i < columnTriggers.Length; i++)
            {
                columnTriggers[i].Init(i, OnBallDroppedInColumn);
            }

            _effectsSpawner.Init(new[] { destroyBallEffectPrefab }, destroyBallEffectsParent, 6);
        }

        private void OnEnable()
        {
            _score = 0;

            SpawnBall();
        }
        private void OnDisable()
        {
            _activeBalls.Clear();

            for (int i = 0; i < _columnsBalls.Length; i++)
            {
                while (_columnsBalls[i].Count > 0)
                {
                    RemoveBall(i, _columnsBalls[i][0]);
                }
            }

            RemoveBall(_currentDropBall);
        }

        private void SpawnBall()
        {
            _currentDropBall = ballSpawner.SpawnBall(ballsInfos[Random.Range(0, ballsInfos.Length)]);
            
            _canDropBall = true;
        }

        public void DropBall()
        {
            if (!_canDropBall) return;
            _canDropBall = false;

            _currentDropBall.ToggleJoint(false);

            _activeBalls.Add(_currentDropBall);
        }

        private async UniTask HandleMatches()
        {
            await WaitForFallingBalls();
            
            _activeBalls.Clear();

            if (IsAnyColumnOver())
            {
                GameOver();

                return;
            }

            var matches = _matchFinder.FindMatches(_columnsBalls, out int ballIndex);

            if (matches.Count > 0)
            {
                _score += ballsInfos.First(x => x.Index == ballIndex).Score;

                var ballsToFall = DestroyBalls(matches);
                _activeBalls.AddRange(ballsToFall);
                
                await HandleMatches();
            }
            else if (AreAllColumnsFull())
            {
                GameOver();

                return;
            }
            else
            {
                SpawnBall();
            }
        }

        private async UniTask WaitForFallingBalls()
        {
            await UniTask.WaitForSeconds(fallingDelay);

            _activeBalls.RemoveAll(ball => ball == null);

            await UniTask.WaitUntil(() => _activeBalls.All(ball => !ball.IsFalling));
        }

        private List<Ball> DestroyBalls(List<Ball> ballsToDestroy)
        {
            Dictionary<int, List<Ball>> fallingBalls = new();

            foreach (var ball in ballsToDestroy)
            {
                int columnIndex = GetColumnIndex(ball);

                if (!fallingBalls.ContainsKey(columnIndex))
                    fallingBalls[columnIndex] = new List<Ball>();

                int indexInColumn = _columnsBalls[columnIndex].IndexOf(ball);
                var aboveBalls = _columnsBalls[columnIndex].Skip(indexInColumn + 1).ToList();

                fallingBalls[columnIndex].AddRange(aboveBalls);

                DestroyBall(columnIndex, ball);
            }

            var fallingBallsList = fallingBalls.Values.SelectMany(x => x).ToList();

            return fallingBallsList.Where(x => x.transform != null).ToList();
        }

        private void DestroyBall(int index, Ball ball)
        {
            _effectsSpawner.Spawn(ball.transform.position);

            RemoveBall(index, ball);
        }
        private void RemoveBall(int index, Ball ball)
        {
            _columnsBalls[index].Remove(ball);
            RemoveBall(ball);
        }
        private void RemoveBall(Ball ball)
        {
            ballSpawner.ReturnBall(ball.gameObject);
        }

        private void OnBallDroppedInColumn(int columnIndex, Ball ball)
        {
            _columnsBalls[columnIndex].Add(ball);

            HandleMatches().Forget();
        }

        private int GetColumnIndex(Ball ball)
        {
            return Array.IndexOf(_columnsBalls, _columnsBalls.FirstOrDefault(x => x.Contains(ball)));
        }

        private bool IsAnyColumnOver() => 
            _columnsBalls.Any(column => column.Count > 3);
        private bool AreAllColumnsFull() => 
            _columnsBalls.All(column => column.Count == 3);

        private async void GameOver()
        {
            GameData.GameResults = new(_score);

            await UniTask.WaitForSeconds(gameoverDelay);

            _windowsController.OpenWindow(2);
        }
    }
}
