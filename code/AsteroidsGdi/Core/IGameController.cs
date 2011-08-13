namespace AsteroidsGdiApp.Core
{
    public interface IGameController
    {
        MouseState MouseState { get; set; }
        KeyboardState KeyboardState{ get; set;}
        bool Standalone { get; set; }
    }
}