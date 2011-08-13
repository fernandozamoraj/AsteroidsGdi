using System;

namespace AsteroidsGdiApp.Core
{
    public class RunAloneGameController : IGameController
    {
        public MouseState MouseState { get; set; }
        public KeyboardState KeyboardState { get; set; }
        public bool Standalone { get; set; }
        Random _randomizer = new Random();

        public RunAloneGameController()
        {
            Standalone = true;
            MouseState = new MouseState();
            KeyboardState = new KeyboardState();
        }

        public void Update()
        {
            TurnLeftOrRight();

            SlowDownOrSpeedUp();

            Fire();
        }

        private void Fire()
        {
            if(_randomizer.Next(10)%2==0)
            {
                KeyboardState.IsFireKeyDown = true;
            }
            else
            {
                KeyboardState.IsFireKeyDown = false;
            }
        }

        private void SlowDownOrSpeedUp()
        {
            if (_randomizer.Next(4) % 2 == 0)
            {
                KeyboardState.IsUpKeyDown = true;
            }
            else if (_randomizer.Next(4) % 3 == 0)
            {
                KeyboardState.IsDownKeyDown = true;
            }
            else
            {
                KeyboardState.IsLeftKeyDown = KeyboardState.IsRightKeyDown = false;
            }
        }

        private void TurnLeftOrRight()
        {
            if(_randomizer.Next(4)%4==0)
            {
                KeyboardState.IsRightKeyDown = true;
            }
            else if (_randomizer.Next(4) % 3 == 0)
            {
                KeyboardState.IsLeftKeyDown = true;
            }
            else
            {
                KeyboardState.IsLeftKeyDown = KeyboardState.IsRightKeyDown = false;
            }
        }
    }
}