using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(720,1280,true);
        Application.targetFrameRate = 60;
    }
}
