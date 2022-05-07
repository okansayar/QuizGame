using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // textleri tanýmlamak için 
using UnityEngine.SceneManagement;

public class SonucManager : MonoBehaviour
{
    [SerializeField]
    private Text dogruText, yanlisText, PuanText;

    [SerializeField]
    private GameObject birinciyildiz, ikinciyildiz, ucuncuyildiz;

    [SerializeField]
    private GameObject yenidenOyna;

    public void SonuclarýYazdir(int dogruAdet, int YanlisAdet, int puan)
    {
        dogruText.text = dogruAdet.ToString();
        yanlisText.text = YanlisAdet.ToString();
        PuanText.text = puan.ToString();

        birinciyildiz.SetActive(false); //ilk baþta yýldýzlar aktif deðil sonuçlar doðru ise aþaðý da aktif hale getiriyoruz.
        ikinciyildiz.SetActive(false);
        ucuncuyildiz.SetActive(false);

        if(dogruAdet==1)
        {
            birinciyildiz.SetActive(true);
        }
        else if(dogruAdet==2)
        {
            birinciyildiz.SetActive(true);
            ikinciyildiz.SetActive(true);
        }
        else
        {
            birinciyildiz.SetActive(true);
            ikinciyildiz.SetActive(true);
            ucuncuyildiz.SetActive(true);
        }
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene("GamePlay");
    }

}
