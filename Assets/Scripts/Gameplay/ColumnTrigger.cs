using System;
using UnityEngine;

namespace Gameplay
{
    public class ColumnTrigger : MonoBehaviour
    {
        private Action<int, Ball> _ballTriggeredAction;
        private int _columnIndex;

        public void Init(int columnIndex, Action<int, Ball> ballTriggeredAction)
        {
            _ballTriggeredAction = ballTriggeredAction;
            _columnIndex = columnIndex;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Ball ball) && !ball.HasCollided)
            {
                ball.OnCollided();

                _ballTriggeredAction.Invoke(_columnIndex, ball);
            }
        }
    }
}