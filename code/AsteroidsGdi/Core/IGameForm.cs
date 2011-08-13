using System.Drawing;

namespace AsteroidsGdiApp.Core
{
    public interface IGameForm
    {
        int FormWidth { get; set; }
        int FormHeight { get; set; }
        void Initialize();
        Graphics CreateGraphics();
    }
}