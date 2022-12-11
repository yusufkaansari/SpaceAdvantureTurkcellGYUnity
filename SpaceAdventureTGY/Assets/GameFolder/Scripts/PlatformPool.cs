using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab = default;

    List<GameObject> platforms = new List<GameObject>();

    Vector2 platformPozisyon;

    [SerializeField]
    float platformArasiMesafe = default; 

    private void Start()
    {
        PlatformUret();
    }
    private void Update()
    {
        //Ekran�n en �st noktas�, �uan list'in i�erisinde olan platformlar�n sonuncunun konumuna ula�t� m�.
        if (platforms[platforms.Count - 1].transform.position.y < Camera.main.transform.position.y + EkranHesaplayicisi.instance.Yukseklik)
        {
            PlatformYerlestir();
        }
    }
    void PlatformUret()
    {
        platformPozisyon = new Vector2(0, 0);
        for (int i = 0; i < 10; i++)
        {
            GameObject platform = Instantiate(platformPrefab, platformPozisyon, Quaternion.identity);
            platforms.Add(platform);
            platform.transform.parent = transform;
            platform.GetComponent<Platform>().Hareket = true;
            SonrakiPlatformPozisyon();
             
        }
    }
    void PlatformYerlestir()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject temp;
            // 5.elemandan ba�layacak gezecek
            temp = platforms[i + 5];
            // en ba�taki eleman� 5 kademe ileri al�n�r
            platforms[i + 5] = platforms[i];
            platforms[i] = temp;
            platforms[i + 5].transform.position = platformPozisyon;
            SonrakiPlatformPozisyon();
        }
    }
    void SonrakiPlatformPozisyon()
    {
        // platformlar aras� dikey mesafe ayarlan�r
        platformPozisyon.y += platformArasiMesafe;
        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.5f)
        {
            platformPozisyon.x = EkranHesaplayicisi.instance.Genislik / 2;
        }
        else
        {
            platformPozisyon.x = -EkranHesaplayicisi.instance.Genislik / 2;
        }
    }
}
