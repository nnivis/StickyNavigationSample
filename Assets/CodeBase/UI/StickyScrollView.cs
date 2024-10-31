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

        public void Initialize()
        {
            SetDefaultInfo();
            _scrollRect.onValueChanged.AddListener(_ => HandleStickyElementPosition());
        }

        private void SetDefaultInfo()
        {
            _defaultParent = _stickyElement.transform.parent;
            _defaultPosition = _stickyElement.anchoredPosition;
            _index = Mathf.Max(0, _defaultParent.childCount - 2);
        }

        private void HandleStickyElementPosition()
        {
            var element1Corners = GetCorners(_element1);
            var viewportCorners = GetViewportCorners();

            if (element1Corners[0].y < viewportCorners[1].y)
                UnAttachStickyElement();
            else
                AttachStickyElement();
        }

        private void AttachStickyElement()
        {
            if (_stickyElement.transform.parent != _stickyParent)
            {
                _stickyElement.transform.SetParent(_stickyParent);
                ClampToViewportTop();
            }
        }

        private void UnAttachStickyElement()
        {
            if (_stickyElement.transform.parent != _defaultParent)
            {
                _stickyElement.transform.SetParent(_defaultParent);
                _stickyElement.transform.SetSiblingIndex(_index);
                _stickyElement.anchoredPosition = _defaultPosition;
            }
        }

        private void ClampToViewportTop()
        {
            var viewportCorners = GetViewportCorners();
            var stickyElementCorners = GetCorners(_stickyElement);

            if (stickyElementCorners[1].y > viewportCorners[1].y)
            {
                float offsetY = stickyElementCorners[1].y - viewportCorners[1].y;
                _stickyElement.anchoredPosition -= new Vector2(0, offsetY);
            }
        }

        private Vector3[] GetCorners(RectTransform element)
        {
            Vector3[] element1Corners = new Vector3[4];
            element.GetWorldCorners(element1Corners);
            return element1Corners;
        }

        private Vector3[] GetViewportCorners()
        {
            Vector3[] viewportCorners = new Vector3[4];
            _scrollRect.viewport.GetWorldCorners(viewportCorners);
            return viewportCorners;
        }
    }
}