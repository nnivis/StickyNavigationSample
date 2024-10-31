using UnityEngine;

namespace CodeBase.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private StickyScrollView _stickyScrollView;
        private void Start() => _stickyScrollView.Initialize();
        private void Update() => _stickyScrollView.UpdateStickyElementPosition();
    }
}
