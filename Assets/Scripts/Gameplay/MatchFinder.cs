using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    class MatchFinder
    {
        public List<Ball> FindMatches(List<Ball>[] columnsBalls)
        {
            HashSet<Ball> matchedBalls = new();

            //vertical matches
            for (int col = 0; col < columnsBalls.Length; col++)
            {
                var columnBalls = columnsBalls[col];
                for (int i = 0; i <= columnBalls.Count - 3; i++)
                {
                    if (columnBalls[i].Index == columnBalls[i + 1].Index && columnBalls[i].Index == columnBalls[i + 2].Index)
                    {
                        matchedBalls.Add(columnBalls[i]);
                        matchedBalls.Add(columnBalls[i + 1]);
                        matchedBalls.Add(columnBalls[i + 2]);
                    }
                }
            }

            //horizontal matches
            for (int row = 0; row < 3; row++)
            {
                if (columnsBalls[0].Count > row && columnsBalls[1].Count > row && columnsBalls[2].Count > row)
                {
                    var b1 = columnsBalls[0][row];
                    var b2 = columnsBalls[1][row];
                    var b3 = columnsBalls[2][row];

                    if (b1.Index == b2.Index && b1.Index == b3.Index)
                    {
                        matchedBalls.Add(b1);
                        matchedBalls.Add(b2);
                        matchedBalls.Add(b3);
                    }
                }
            }

            //diagonal matches
            if (columnsBalls[0].Count > 0 && columnsBalls[1].Count > 1 && columnsBalls[2].Count > 2)
            {
                var b1 = columnsBalls[0][0];
                var b2 = columnsBalls[1][1];
                var b3 = columnsBalls[2][2];

                if (b1.Index == b2.Index && b1.Index == b3.Index)
                {
                    matchedBalls.Add(b1);
                    matchedBalls.Add(b2);
                    matchedBalls.Add(b3);
                }
            }

            if (columnsBalls[0].Count > 2 && columnsBalls[1].Count > 1 && columnsBalls[2].Count > 0)
            {
                var b1 = columnsBalls[0][2];
                var b2 = columnsBalls[1][1];
                var b3 = columnsBalls[2][0];

                if (b1.Index == b2.Index && b1.Index == b3.Index)
                {
                    matchedBalls.Add(b1);
                    matchedBalls.Add(b2);
                    matchedBalls.Add(b3);
                }
            }

            return matchedBalls.ToList();
        }
    }
}