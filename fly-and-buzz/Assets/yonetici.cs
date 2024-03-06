using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class yonetici : MonoBehaviour
{
    public GameObject sinek;

    public Text skor_txt;
    public Text saniye_txt;

    int skor = 10;
    int saniye = 50;

    List<GameObject> sinekler;

    public AudioSource ses;
    public GameObject yeniden_oyna_pnl;

    public float sinek_hizi = 5.0f;
    float sinek_ekleme_hizi = 1.0f;

    public GameObject patlama_efekti;
    public List<GameObject> patlama_efektleri_listesi;

    void Start()
    {
        skor_txt.text = skor.ToString();
        saniye_txt.text = saniye.ToString();

        sinekler = new List<GameObject>();

        patlama_efektleri_listesi = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject y_efekt = Instantiate(patlama_efekti);
            patlama_efektleri_listesi.Add(y_efekt);
            y_efekt.SetActive(false);
        }

        for (int i = 0; i < 20; i++)
        {
            float rast = Random.Range(-3.5f, 3.5f);

            GameObject y_sinek = Instantiate(sinek, new Vector3(rast, 0, 1.0f), Quaternion.Euler(0f, 0f, 180.0f));

            sinekler.Add(y_sinek);
            y_sinek.SetActive(false);
        }

        InvokeRepeating("sinek_goster", 0.0f, sinek_ekleme_hizi);
        InvokeRepeating("saniye_azalt", 0.0f, 1.0f);
        InvokeRepeating("zorlugu_artir", 30.0f, 30.0f);
    }

    void zorlugu_artir()
    {
        sinek_ekleme_hizi -= 0.1f;
        sinek_hizi += 1.0f;

        if (sinek_ekleme_hizi <= 0.2f)
        {
            sinek_ekleme_hizi = 0.2f;
        }

        if (sinek_hizi >= 10.0f)
        {
            sinek_hizi = 10.0f;
        }

        CancelInvoke("sinek_goster");
        InvokeRepeating("sinek_goster", 0.0f, sinek_ekleme_hizi);
    }

    void saniye_azalt()
    {
        saniye--;
        saniye_txt.text = saniye.ToString();

        if (saniye <= 0 || skor <= 0)
        {
            yeniden_oyna_pnl.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void skoru_degistir(int deger)
    {
        skor += deger;
        skor_txt.text = skor.ToString();

        if (skor <= 0 || saniye <= 0)
        {
            yeniden_oyna_pnl.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void saniyeyi_degistir(int deger)
    {
        saniye += deger;
        saniye_txt.text = saniye.ToString();

        if (saniye <= 0 || skor <= 0)
        {
            yeniden_oyna_pnl.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void sinek_goster()
    {
        foreach (GameObject bl in sinekler)
        {
            if (bl.activeSelf == false)
            {
                bl.SetActive(true);
                float rast = Random.Range(-5.7f, 5.7f);
                bl.transform.position = new Vector3(rast, -3.0f, 1.0f);
                break;
            }
        }
    }

    public void yeniden_oyna_btn()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        Debug.Log("QUIT");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}



