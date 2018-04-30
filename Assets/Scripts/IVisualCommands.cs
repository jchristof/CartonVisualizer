
namespace Assets.Scripts {
    public interface IVisualCommands {

        void ShowBoundingVolume();
        void HideBoundingVolume();

        void ShowFirst();
        void ShowNext();
        void ShowPrevious();
        void ShowAll();

        void Explode();
        void Compact();

        void RotationOn();
        void RotationOff();

    }
}
