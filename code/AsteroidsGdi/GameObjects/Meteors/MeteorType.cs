namespace AsteroidsGdiApp.GameObjects.Meteors
{
    public abstract class MeteorType
    {
        public abstract float Radius{ get;}
        public abstract int MinimumNumberOfPoints { get; }
        public abstract MeteorType GetNextSmallerSize();
        public abstract int Score { get; }
        public abstract string Text { get; }
    }
}