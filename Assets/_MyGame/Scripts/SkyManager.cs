using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public GameObject Sun;
    public GameObject Moon;
    public GameObject Stars;
    public GameObject rainFX;
    public Material CloudMaterial;
    public Material SkyMaterial;




    public void ChangeSky (bool TimeState)
    {
        if (TimeState)
        {
            //gündüz
            Sun.SetActive(true);
            Moon.SetActive(false);
            Stars.SetActive(false);
            rainFX.SetActive(false);
            CloudMaterial.mainTextureOffset = new Vector2(1f, 0);
            SkyMaterial.mainTextureOffset = new Vector2(1f, 0);
        }
        else
        {
            // gece
            Sun.SetActive(false);
            Moon.SetActive(true);
            Stars.SetActive(true);
            rainFX.SetActive(true);
            CloudMaterial.mainTextureOffset = new Vector2(1.51f, 0);
            SkyMaterial.mainTextureOffset = new Vector2(1.36f, 0);
        }

    }
}
