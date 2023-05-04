using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PulsarDevKit.Scripts.Debug
{
    enum ELogState
    {
        Log,
        Warn,
        Error,
        Success
    }

    public static class PulseLogger
    {
        private static DebugDisplay _debugDisplay;

        public static void Log(string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
        {
            Print(ELogState.Log, message, filePath, memberName, lineNumber, Color.cyan);
        }

        public static void Warn(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Print(ELogState.Warn, message, filePath, memberName, lineNumber, Color.yellow);
        }

        public static void Error(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Print(ELogState.Error, message, filePath, memberName, lineNumber, new Color(1.0f, 0.0f, 0.0f, 1));
        }

        public static void Success(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Print(ELogState.Success, message, filePath, memberName, lineNumber, new Color(0.0f, 1.0f, 0.0f, 1));
        }

        private static void Print(ELogState logState, string message, string filePath, string memberName, int lineNumber, Color color)
        {
            #if UNITY_EDITOR
            // Create out display object if null
            if (_debugDisplay == null)
            {
                GameObject logObj = new GameObject();
                logObj.name = "PulseLoggerDisplay";
                _debugDisplay = logObj.AddComponent<DebugDisplay>();

                if (_debugDisplay == null) return;
            }

            DisplayMessageText displayMessageText = new DisplayMessageText
            (
                FormatString(logState, message, filePath, memberName, lineNumber),
                color,
                new Color(0, 0, 0, 0.8f),
                20.0f
            );

            _debugDisplay.PrintMessage(displayMessageText);

            switch (logState)
            {
                case ELogState.Warn:
                    UnityEngine.Debug.LogWarning(FormatString(logState, message, filePath, memberName, lineNumber));
                    break;
                case ELogState.Error:
                    UnityEngine.Debug.LogError(FormatString(logState, message, filePath, memberName, lineNumber));
                    break;
                default:
                    UnityEngine.Debug.Log(FormatString(logState, message, filePath, memberName, lineNumber));
                    break;
            }
            #endif
        }

        private static string FormatString(ELogState logState, string message, string filePath, string memberName, int lineNumber)
        {
            string className = Path.GetFileName(filePath);
            return $"{logState.ToString()} | Class: {className} | Method: {memberName} | Message: <color=white>{message}</color> | Line: {lineNumber}";
        }
    }
}