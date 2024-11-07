using System;
using UnityEngine;

namespace CodeBase.Services.Orientation
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public sealed class OrientationController : MonoBehaviour
    {
        public static bool IsVertical;

        private static event EventHandler<bool> OrientationChanged;

        static OrientationController() => OrientationChanged += (s, e) => IsVertical = e;

        public static void FireOrientationChanged(object s, bool isVertical) =>
            OrientationChanged?.Invoke(s, isVertical);

        public SavedRect verticalRect = new SavedRect();

        public SavedRect horizontalRect = new SavedRect();

        private RectTransform _rect;

        private void OnDestroy() => OrientationChanged -= OnOrientationChanged;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            OrientationChanged += OnOrientationChanged;
            OnOrientationChanged(this, IsVertical);
        }

        private void OnOrientationChanged(object sender, bool isVertical)
        {
            if (isVertical)
                verticalRect.PutDataToRectTransform(_rect);
            else
                horizontalRect.PutDataToRectTransform(_rect);
        }
    }
}