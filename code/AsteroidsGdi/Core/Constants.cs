namespace AsteroidsGdiApp.Core
{
    public class Constants
    {
        private Constants()
        {
        }

        public static readonly int EnemyShipInterval = 50; 
        public static readonly int CanvasWidth = 700;
        public static readonly int BulletSpeed = 15;
        public static readonly int MaxPlayerBulletDistance = CanvasWidth/2;
        public static readonly int MaxEnemyBulletDistance = CanvasWidth/2 + CanvasWidth/4;
        public static readonly int ShieldCountDown = 200;
    }
}