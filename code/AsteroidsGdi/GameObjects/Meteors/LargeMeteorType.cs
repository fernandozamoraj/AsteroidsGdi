namespace AsteroidsGdiApp.GameObjects.Meteors
{
    public class LargeMeteorType : MeteorType
    {
        public override float Radius
        {
            get
            {
                return 30;
            }
        }

        public override int MinimumNumberOfPoints
        {
            get { return 7; }
        }

        public override MeteorType GetNextSmallerSize()
        {
            return new MediumMeteorType();
        }

        public override int Score
        {
            get { return 400; }
        }

        public override string Text
        {
            get { return "L"; }
        }
    }
}