using System;
using System.Drawing;
using System.Windows.Forms;
using AsteroidsGdiApp.Core;

namespace AsteroidsGdiApp
{
    public partial class Form1 : Form, IGameForm, IGameController
    {
        Timer _timer = new Timer();
        AsteroidsGame _game;

        public Form1()
        {
            InitializeComponent();
            MouseState = new MouseState
                             {
                                 IsMouseButtonDown = false, 
                                 MousePosition = new Point(-1, -1)
                             };

            KeyboardState = new KeyboardState
                                {
                                    IsLeftKeyDown = false, 
                                    IsRightKeyDown = false
                                };

            _timer.Enabled = true;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
            _timer.Interval = 350;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _gameController.Update();
        }

        public MouseState MouseState
        {
            get; set;
        }

        public KeyboardState KeyboardState
        { get; set;}

        public bool Standalone
        {
            get; set;
        }

        public void Initialize()
        {
            ShowIcon = false;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Black;
            MaximizeBox = false;
            Size = new Size(Constants.CanvasWidth,
                            Constants.CanvasWidth);
        }
        
        RunAloneGameController _gameController = new RunAloneGameController();

        private void Form1_Load(object sender, EventArgs e)
        {
            _game = new AsteroidsGame(this, _gameController);

            GameManager.CreateTheGame(_game);

            _game.Run();
        }

        private void StartNewGame(IGameController gameController)
        {
            GameManager.TheGameManager.ReInitializeGame(gameController);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseState.IsMouseButtonDown = true;
            MouseState.MousePosition = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseState.IsMouseButtonDown = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                KeyboardState.IsRightKeyDown = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                KeyboardState.IsLeftKeyDown = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                KeyboardState.IsUpKeyDown = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                KeyboardState.IsDownKeyDown = true;
            }

            if (e.KeyCode == Keys.F)
            {
                KeyboardState.IsFireKeyDown = true;
            }

            if(e.KeyCode == Keys.S)
            {
                StartNewGame(this);
            }

            if (e.KeyCode == Keys.Space)
            {
                KeyboardState.IsShieldKeyDown = true;
            }

            if (e.KeyCode == Keys.X)
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                KeyboardState.IsRightKeyDown = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                KeyboardState.IsLeftKeyDown = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                KeyboardState.IsUpKeyDown = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                KeyboardState.IsDownKeyDown = false;
            }

            if (e.KeyCode == Keys.F)
            {
                KeyboardState.IsFireKeyDown = false;
            }

            if (e.KeyCode == Keys.Space)
            {
                KeyboardState.IsShieldKeyDown = false;
            }
        }
    }
}