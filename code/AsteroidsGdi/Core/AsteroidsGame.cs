using System.Drawing;
using System.Media;
using System.Collections.Generic;
using AsteroidsGdiApp.GameObjects;
using AsteroidsGdiApp.GameObjects.Meteors;
using AsteroidsGdiApp.Core;
using System;

namespace AsteroidsGdiApp.Core
{
    public class AsteroidsGame : Game
    {
        EnemyShipShooterService _enemyShipShooterService;
        PlayerShip _ship = new PlayerShip();
        private Photon[] _photons;
        private IGameController _gameController = new RunAloneGameController();
        private List<Meteor> _meteors;
        GameManager _gameManager;
        private GameScoreKeeper _gameScoreKeeper = new GameScoreKeeper();
        EnemyShip _enemyShip = new EnemyShip();
        PhotonTimeManager _shipPhotonTimeManager = new PhotonTimeManager(150);
        ScoredPointsDisplay _scorePointsDisplay = new ScoredPointsDisplay();
        DateTime _enemyShipLastShown;

        public AsteroidsGame(IGameForm parent, IGameController gameController) : base(parent)
        {
            StartNewRound();
            _gameController = gameController;
            
        }

        private void StartNewRound()
        {
            _enemyShipLastShown = DateTime.Now;
            _gameScoreKeeper.IncrementLevel();
            _photons = new Photon[20];
            _meteors = new List<Meteor>();

            _enemyShipShooterService = new EnemyShipShooterService(_enemyShip);

            for(int i = 0; i < _photons.Length; i++)
            {
                _photons[i] = new Photon();
            }

            _photons[0] = new TracerPhoton();

            for (int i = 0; i < 4+ _gameScoreKeeper.Level; i++)
            {
                _meteors.Add( new Meteor(new LargeMeteorType(), new Point(0, 0)));
            }
        }

        //TODO Get rid of event driven design
        protected void GameManager_MeteorWasHit(object sender, MeteorHitEventArgs e)
        {
            _gameScoreKeeper.UpdateScore(e.Meteor.Score);
            _scorePointsDisplay.Display(e.Meteor.Location, e.Meteor.Score);

            if (!(e.Meteor.GetNextSmallerMeteor() is NullMeteorType))
            {
                _meteors.Add(new Meteor(e.Meteor.GetNextSmallerMeteor(), e.Meteor.Location));
                _meteors.Add(new Meteor(e.Meteor.GetNextSmallerMeteor(), e.Meteor.Location));
            }
        }

        protected override void Update()
        {
            InitializeTheGameManager();
            HandleControllerInputs();
            UpdateTheShip();
            UpdatePhotons();
            UpdateMeteors();
            DetectBulletCollisions();
            DetectShipToMeteorCollisions();
            StartRoundOverIfNecessary();
            ReactivateAShipIfPlayerHasLivesLeft();
            SetGameOverIfNecessary();
            SendOffEnemyShip();
            _enemyShip.Update();
            _enemyShipShooterService.Update();
            _enemyShipShooterService.FireRound();
            _scorePointsDisplay.Update();
        }

        private void SendOffEnemyShip()
        {
            if (!_enemyShip.IsActive && DateTime.Now.Subtract(_enemyShipLastShown).TotalSeconds > Constants.EnemyShipInterval)
            {
                _enemyShip.Activate();
                _enemyShipLastShown = DateTime.Now;
            }
        }

        private void SetGameOverIfNecessary()
        {
            if (_gameScoreKeeper.Lives == 0)
            {
                _gameScoreKeeper.SetStandaloneMode();
            }
        }

        private void InitializeTheGameManager()
        {
            if (_gameManager == null)
            {
                _gameManager = GameManager.TheGameManager;
                _gameManager.MeteorWasHit += new System.EventHandler<MeteorHitEventArgs>(GameManager_MeteorWasHit);
            }
        }

        private void UpdateTheShip()
        {
            _ship.Update();
        }

        private void HandleControllerInputs()
        {
            if(_gameController.KeyboardState.IsLeftKeyDown)
                _ship.RotateLeft();

            if(_gameController.KeyboardState.IsRightKeyDown)
                _ship.RotateRight();
            
            if(_gameController.KeyboardState.IsUpKeyDown)
                _ship.Thrust();
            else if(_gameController.KeyboardState.IsDownKeyDown)
                _ship.SlowDown();
            
            if (_gameController.KeyboardState.IsFireKeyDown)
                Fire();
            
            if (_gameController.KeyboardState.IsShieldKeyDown)
                _ship.TurnOnShield();

            if (_gameController.KeyboardState.IsUpKeyDown)
                _ship.TurnOnThruster();
            else
                _ship.TurnOffThruster();
        }

        private void ReactivateAShipIfPlayerHasLivesLeft()
        {
            if (!_ship.IsActive && _gameScoreKeeper.Lives > 0)
            {
                _ship.Activate();
            }
        }

        private void StartRoundOverIfNecessary()
        {
            if(RoundIsComplete())
                StartNewRound();
        }

        private bool RoundIsComplete()
        {
            bool thereAreNoMoreActiveMeteors = true;

            foreach (Meteor meteor in _meteors)
            {
                if (meteor.IsActive)
                    thereAreNoMoreActiveMeteors = false;
            }

            return thereAreNoMoreActiveMeteors;
        }

        private void DetectBulletCollisions()
        {
            for(int meteorIndex = 0; meteorIndex < _meteors.Count; meteorIndex++)
            {
                for (int bulletIndex = 0; bulletIndex < _photons.Length; bulletIndex++)
                {
                    //TODO get rid of event drive desing here
                    _meteors[meteorIndex].TestIfShot(_photons[bulletIndex]);
                }
            }

            //Test if player ship has been hit by the enemy ships bullet
            if (_enemyShipShooterService.PhotonCollidesWithShip(_ship))
            {
                BlowUpShip();
            }

            //Test to see if the players bullet has hit the enemy ship
            foreach (Photon photon in _photons)
            {
                if (photon.IsActive && _enemyShip.IsActive)
                {
                    if (_enemyShip.IsPointWithin(photon.Location))
                    {
                        _enemyShip.Inactivate();
                        _gameScoreKeeper.UpdateScore(2000);
                    }
                }
            }
        }

        private void DetectShipToMeteorCollisions()
        {
            if (!_ship.IsActive)
                return;

            for (int meteorIndex = 0; meteorIndex < _meteors.Count; meteorIndex++)
            {
                if (_meteors[meteorIndex].CollidesWithShip(_ship))
                {
                    _meteors[meteorIndex].IsActive = false;
                    GameManager.RaiseMeteorHit(_meteors[meteorIndex]);
                    BlowUpShip();
                }
            }
        }

        private void DetectShipToShipCollision()
        {
            if (_enemyShip.IsPointWithin(_ship.Location))
            {
                BlowUpShip();
            }
        }

        private void BlowUpShip()
        {
            _ship.SetInactive();
            _gameScoreKeeper.DecrementNumberOfLives();
            _ship.BlowUpShip();
        }
                   
        private void UpdatePhotons()
        {
            _photons.Update();
        }

        private void UpdateMeteors()
        {
            _meteors.Update();
        }

        private void DrawPhotons(Graphics graphics)
        {
            _photons.Draw(graphics);
        }

        private void DrawMeteors(Graphics graphics)
        {
            _meteors.Draw(graphics);
        }

        private void Fire()
        {
            if (!_ship.IsActive)
                return;

            foreach (var photon in _photons)
            {
                if (!photon.IsActive)
                {
                    photon.Fire(_ship.Location, _ship.ShipDirection, Constants.MaxPlayerBulletDistance, _shipPhotonTimeManager);
                    break;
                }
            }
        }

        public override void Draw()
        {
            Bitmap backbuffer = new Bitmap(Constants.CanvasWidth, Constants.CanvasWidth);

            using (Graphics graphics = Graphics.FromImage(backbuffer))
            {
                ClearTheCanvas(graphics);
                DrawShips(graphics);
                DrawPhotons(graphics);
                DrawMeteors(graphics);
                DrawHeadsUpDisplay(graphics);
                _enemyShipShooterService.Draw(graphics);
                _scorePointsDisplay.Draw(graphics);
            }

            //Draw onto the window
            using (Graphics windowGraphics = ParentForm.CreateGraphics())
            {
                windowGraphics.DrawImageUnscaled(backbuffer, 0, 0);
            }
        }

        private void ClearTheCanvas(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, 0, 0, Constants.CanvasWidth, Constants.CanvasWidth);
        }

        private void DrawHeadsUpDisplay(Graphics graphics)
        {
            _gameScoreKeeper.Draw(graphics);
        }

        private void DrawShips(Graphics graphics)
        {
            _ship.Draw(graphics);
            _enemyShip.Draw(graphics);
        }

        public void Init(IGameController controller)
        {
            _gameController = controller;
            _gameScoreKeeper.StartGame();
            _ship.Activate();
            StartNewRound();
        }
    }
}