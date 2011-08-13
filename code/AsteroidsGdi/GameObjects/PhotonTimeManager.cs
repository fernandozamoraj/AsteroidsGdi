using System;

namespace AsteroidsGdiApp.GameObjects
{
    public class PhotonTimeManager
    {
        private DateTime _lastFired;
        private int _interval;

        public PhotonTimeManager(int intervalInMilliseconds)
        {
            _interval = intervalInMilliseconds;
        }

        public bool EnoughTimeHasPassed()
        {
            return DateTime.Now.Subtract(_lastFired).TotalMilliseconds > _interval;
        }

        public void SetFired()
        {
            _lastFired = DateTime.Now;
        }
    }
}