namespace AsteroidsGdiApp.GameObjects.Meteors
{
    public class NullMeteorType : MeteorType
    {
        public override float Radius
        {
            get
            {
                return 1;
            }
        }

        public override int MinimumNumberOfPoints
        {
            get { return 1; }
        }

        public override MeteorType GetNextSmallerSize()
        {
            return new NullMeteorType();
        }

        public override int Score
        {
            get { return 0; }
        }

        public override string Text
        {
            get { return "U"; }
        }
    }
}