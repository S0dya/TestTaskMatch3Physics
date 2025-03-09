using Infos;
using UnityEngine;

namespace Gameplay
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private FixedJoint2D joint;

        public int Index { get; private set; }

        public bool HasCollided { get; private set; }
        public bool IsFalling { get { return rb.velocity.magnitude > 0.05f; } }

        private void OnEnable()
        {
            HasCollided = false;
        }

        public void Init(BallInfo ballInfo, Rigidbody2D rbToAttach)
        {
            spriteRenderer.color = ballInfo.Color;
            Index = ballInfo.Index;

            if (joint.connectedBody == null) joint.connectedBody = rbToAttach;
        }

        public void ToggleJoint(bool toggle) => joint.enabled = toggle;

        public void OnCollided()
        {
            HasCollided = true;
        }
    }
}