using UnityEngine;

namespace Assets._project.CodeBase.Interface
{
    public interface IItemControll
    {
        void Activate();
        void Deactivate();
        void SetPosition(Vector3 position);
    }
}
