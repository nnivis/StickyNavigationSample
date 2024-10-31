using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class StickyScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _element1;
        [SerializeField] private RectTransform _stickyElement;
        [SerializeField] private Transform _stickyParent;

        private Transform _defaultParent;
        private Vector2 _defaultPosition;
        private int _index;

        public void Initialize() => SetDefaultInfo();

        public void UpdateStickyElementPosition() => HandleStickyElementPosition();

        private void SetDefaultInfo()
        {
            _defaultParent = _stickyElement.transform.parent;
            _defaultPosition = _stickyElement.anchoredPosition;
            _index = Mathf.Max(0, _defaultParent.childCount - 2);
        }

        private void HandleStickyElementPosition()
        {
            Vector3[] element1Corners = new Vector3[4];
            _element1.GetWorldCorners(element1Corners);

            Vector3[] viewportCorners = new Vector3[4];
            _scrollRect.viewport.GetWorldCorners(viewportCorners);

            if (element1Corners[0].y < viewportCorners[1].y)
                UnAttachStickyElement();
            else
                AttachStickyElement();
        }

        private void AttachStickyElement() => _stickyElement.transform.SetParent(_stickyParent);

        private void UnAttachStickyElement()
        {
            _stickyElement.transform.SetParent(_defaultParent);
            _stickyElement.transform.SetSiblingIndex(_index);
            _stickyElement.anchoredPosition = _defaultPosition;
        }
    }
}