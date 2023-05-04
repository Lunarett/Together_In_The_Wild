using System.Collections;
using System.Collections.Generic;
using PulsarDevKit.Scripts.Debug;
using UnityEngine;

public class LogTester : MonoBehaviour
{
    public Font font;
    
    void Start()
    {
        StartCoroutine(PrintLogs());
    }

    IEnumerator PrintLogs()
    {
        PulseLogger.Error("this is a message to test the logger");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("Super logger in da hood!");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("this message is to test the warn function");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("Dont sweat this aint a real error XDDD");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("Dont sweat this aint a real error XDDD");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("Dont sweat this aint a real error XDDD");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("Dont");
        yield return new WaitForSeconds(0.1f);
        PulseLogger.Error("Dont sweat this aint a real error XDDD wefi jwefo wof woe fowi fejowie fwo feiow efowi efow ");
    }
}