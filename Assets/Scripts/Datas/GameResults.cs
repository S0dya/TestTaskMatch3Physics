
public class GameResults 
{
    public int Score { get; private set; }

    public GameResults() { } 
    public GameResults(int score)
    {
        Score = score;
    }
}
