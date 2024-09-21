using System.IO;
using UnityEditor;
using UnityEngine;


public class ScreenshotMaker : MonoBehaviour
{
    [MenuItem("Flig/Take Screenshot")]
    public static void TakeScreenshot()
    {
        int sceenshotNumber = EditorPrefs.GetInt("ScreenshotCount", 0) + 1;
        string path = Application.dataPath.Replace("/Assets", "") + "/Screenshots/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        ScreenCapture.CaptureScreenshot(path + "Screenshot_" + sceenshotNumber + ".png");
        Debug.Log("Скриншот сохранен по адресу " + path + "Screenshot_" +sceenshotNumber + ".png");
        EditorPrefs.SetInt("ScreenshotCount", sceenshotNumber);
    }
}
