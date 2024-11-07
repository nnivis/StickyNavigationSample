using System;
using UnityEngine;

namespace CodeBase.Services.Orientation
{
    [Serializable]
    public class SavedRect
    {
        public bool isInitialized;
        public Vector3 anchoredPosition;
        public Vector2 sizeDelta;
        public Vector2 minAnchor;
        public Vector2 maxAnchor;
        public Vector2 pivot;
        public Vector3 rotation;
        public Vector3 scale;

        public void SaveDataFromRectTransform(RectTransform rect)
        {
            if (rect == null)
                return;

            isInitialized = true;

            anchoredPosition = rect.anchoredPosition3D;
            sizeDelta = rect.sizeDelta;
            minAnchor = rect.anchorMin;
            maxAnchor = rect.anchorMax;
            pivot = rect.pivot;
            rotation = rect.localEulerAngles;
            scale = rect.localScale;
        }

        public void PutDataToRectTransform(RectTransform rect)
        {
            if (rect == null || !isInitialized)
                return;

            rect.anchoredPosition3D = anchoredPosition;
            rect.sizeDelta = sizeDelta;
            rect.anchorMin = minAnchor;
            rect.anchorMax = maxAnchor;
            rect.pivot = pivot;
            rect.localEulerAngles = rotation;
            rect.localScale = scale;
        }
    }
}