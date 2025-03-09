using UnityEngine;

namespace Infos
{
    [CreateAssetMenu(menuName = "Infos/BallInfo", fileName = "Ball Info")]
    public class BallInfo : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int index;

        public Color Color => color;
        public int Index => index;
    }
}
