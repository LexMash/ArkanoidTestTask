namespace Arkanoid.UI
{
    public struct NewScoreData
    {
        public int HighScore;
        public int CurrentScore;

        public NewScoreData(int highScore, int currentScore)
        {
            HighScore = highScore;
            CurrentScore = currentScore;
        }
    }
}
