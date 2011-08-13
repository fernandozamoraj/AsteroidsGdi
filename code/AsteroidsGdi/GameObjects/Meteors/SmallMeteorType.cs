namespace AsteroidsGdiApp.GameObjects.Meteors
{
    public class SmallMeteorType : MeteorType
    {
        public override float Radius
        {
            get
            {
                return 8;
            }
        }

        public override int MinimumNumberOfPoints
        {
            get { return 7; }
        }

        public override MeteorType GetNextSmallerSize()
        {
            return new NullMeteorType();
        }

        public override int Score
        {
            get { return 1000; }
        }

        public override string Text
        {
            get { return "S"; }
        }
    }
}