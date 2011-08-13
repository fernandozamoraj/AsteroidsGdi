using System;
using System.Drawing;
using AsteroidsGdiApp.GameObjects;

namespace AsteroidsGdiApp.Core
{
    public class EnemyShipShooterService
    {
        EnemyShip _enemyShip;
        Photon[] _photons = new Photon[20];
        Random _randomGenerator = new Random();
        DateTime _lastShotFired = DateTime.Now;
        PhotonTimeManager _photonTimeManager = new PhotonTimeManager(200);

        public EnemyShipShooterService(EnemyShip enemyShip)
        {
            _enemyShip = enemyShip;

            for (int i = 0; i < 20; i++)
            {
                _photons[i] = new Photon();
            }
        }
        
        public void FireRound()
        {
            if (!_enemyShip.IsActive)
                return;

            foreach (Photon photon in _photons)
            {
                if (!photon.IsActive)
                {
                    photon.Fire(_enemyShip.Location, _randomGenerator.Next(360), Constants.MaxEnemyBulletDistance, _photonTimeManager);
                    _lastShotFired = DateTime.Now;
                }
            }
        }

        public bool PhotonCollidesWithShip(PlayerShip playerShip)
        {
            foreach (Photon photon in _photons)
            {
                if (photon.IsActive)
                {
                    if (playerShip.IsPointWithin(photon.Location))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Update()
        {
            foreach (Photon photon in _photons)
            {
                photon.Update();
            }
        }

        public void Draw(Graphics graphics)
        {
            foreach (Photon photon in _photons)
            {
                photon.Draw(graphics);
            }
        }
    }
}
