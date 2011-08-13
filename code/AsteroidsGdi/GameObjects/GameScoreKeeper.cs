using System.Drawing;

namespace AsteroidsGdiApp.GameObjects
{
    public class GameScoreKeeper
    {
        private SoundManager _soundManager = new SoundManager();
        private int _score;
        private int _lives = 3;
        private int _level;
        private bool _gotFreeLive;
        private bool _standAloneMode = true;
        private InsertCoinWarningView _insertCoinWarningView = new InsertCoinWarningView();

        public int Lives 
        {
            get 
            {
                return _lives;
            }
        }

        public int Level
        {
            get 
            {
                return _level;
            }
        }

        public void UpdateScore(int points)
        {
            if(_standAloneMode)
                return;

            if (_lives < 1)
                return;

            _score += points;

            if (!_gotFreeLive && _score > 10000)
            {
                IncrementNumberOfLives();
                _gotFreeLive = true;
                _soundManager.PlayFreeLiveSound();
            }
        }

        public void SetStandaloneMode()
        {
            _standAloneMode = true;
        }

        public void StartGame()
        {
            _lives = 3;
            _level = 0;
            _score = 0;
            _standAloneMode = false;
        }

        protected void IncrementNumberOfLives()
        {
            _lives++;
        }

        public void DecrementNumberOfLives()
        {
            if(!_standAloneMode)
                _lives--;
        }
        
        public void IncrementLevel()
        {
            _level++;
        }

        public void Draw(Graphics graphics)
        {
            if (_standAloneMode)
            {
                _insertCoinWarningView.Draw(graphics);
            }

            Font _font = new Font("Courier New", 13);
            graphics.DrawString(string.Format("SCORE: {0}       LIVES: {1}       LEVEL: {2}", _score, (_lives - 1 > 0 ? _lives - 1 : 0), _level),
                _font, Brushes.White, new PointF(10, 10));
                
        }
    }
}
