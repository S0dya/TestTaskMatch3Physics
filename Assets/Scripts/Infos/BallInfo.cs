using UnityEngine;

namespace Infos
{
    [CreateAssetMenu(menuName = "Infos/BallInfo", fileName = "Ball Info")]
    public class BallInfo : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int index;
        [SerializeField] private int score = 1;

        public Color Color => color;
        public int Index => index;
        public int Score => score;
    }
}
