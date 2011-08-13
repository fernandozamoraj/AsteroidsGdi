using System;
using AsteroidsGdiApp.GameObjects.Meteors;

namespace AsteroidsGdiApp.Core
{
    public class GameManager
    {
        private AsteroidsGame _game;
        private static GameManager _gameManager;
        public event EventHandler<MeteorHitEventArgs> MeteorWasHit;

        public static GameManager CreateTheGame(AsteroidsGame game)
        {
            if(_gameManager == null)
            {
                _gameManager = new GameManager(game);
            }

            return _gameManager;
        }

        public static GameManager TheGameManager
        {
            get { return _gameManager; }
        }

        private GameManager(AsteroidsGame game)
        {
            _game = game;
        }

        public void Draw()
        {
            _game.Draw();
        }

        public void ReInitializeGame(IGameController gameController)
        {
            _game.Init(gameController);
        }

        public static void RaiseMeteorHit(Meteor meteor)
        {
            TheGameManager.InvokeMeteorHit(meteor);
        }

        private void InvokeMeteorHit(Meteor meteor)
        {
            if (MeteorWasHit != null)
            {
                MeteorWasHit(this, 
                    new MeteorHitEventArgs {Meteor = meteor });
            }
        }
    }

    public class MeteorHitEventArgs : EventArgs
    {
        public Meteor Meteor;
    }
}