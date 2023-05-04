using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PulsarDevKit.Scripts.Debug
{
    public class DisplayMessageText
    {
        public DisplayMessageText(string message, Color textColor, Color backgroundColor, float duration)
        {
            Message = message;
            MessageColor = textColor;
            BackgroundColor = backgroundColor;
            Duration = 2.0f;
        }

        public string Message;
        public Color MessageColor;
        public Color BackgroundColor;
        public float Duration;
        public float Spacing = 30;
    }

    public class DebugDisplay : MonoBehaviour
    {
        public int Limit = 30;

        private List<DisplayMessageText> OutputTextList = new List<DisplayMessageText>();
        private GUIStyle style;
        private Texture2D backgroundTexture;

        public void PrintMessage(DisplayMessageText displayMessageText)
        {
            OutputTextList.Add(displayMessageText);

            // If we exceed limit pop instantly
            if (OutputTextList.Count >= Limit)
            {
                PopMessage();
                return;
            }
            
            if (displayMessageText.Duration > 0)
                StartCoroutine(PopAfterSeconds(20));
        }

        private void SetBackground(Color backgroundColor)
        {
            const int width = 16;
            const int height = 16;
            
            backgroundTexture = new Texture2D(width, height);
            Color bgColor = backgroundColor;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    backgroundTexture.SetPixel(x, y, bgColor);
                }
            }
            
            backgroundTexture.Apply();
        }
        
        private IEnumerator PopAfterSeconds(float duration)
        {
            yield return new WaitForSeconds(duration);
            PopMessage();
        }

        private void PopMessage()
        {
            if (OutputTextList[0] == null) return;
            OutputTextList.RemoveAt(0);
        }

        private void OnGUI()
        {
            float posY = 0;
            foreach (var text in OutputTextList)
            {
                if (text == null) continue;

                style = new GUIStyle();
                style.normal = new GUIStyleState();
                style.fontSize = 18;
                style.normal.textColor = text.MessageColor;
                SetBackground(text.BackgroundColor);
                style.normal.background = backgroundTexture;

                posY += text.Spacing;
                float width = text.Message.Length * 7.1f;
                
                GUI.Label(new Rect(10, posY, width, 24), text.Message, style);
            }
        }
    }
}