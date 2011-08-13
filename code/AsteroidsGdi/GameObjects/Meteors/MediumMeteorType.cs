namespace AsteroidsGdiApp.GameObjects.Meteors
{
    public class MediumMeteorType : MeteorType
    {
        public override float Radius
        {
            get
            {
                return 20;
            }
        }

        public override int MinimumNumberOfPoints
        {
            get { return 7; }
        }

        public override MeteorType GetNextSmallerSize()
        {
            return new SmallMeteorType();
        }

        public override int Score
        {
            get { return 700; }
        }

        public override string Text
        {
            get { return "M"; }
        }
    }
}