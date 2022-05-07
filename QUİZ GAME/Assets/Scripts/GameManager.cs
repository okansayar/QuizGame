using System.Collections;
using System.Collections.Generic;
using System.Linq; // sorualr� listeye atmak ve listeyi d�zenlemek i�in olu�turulan claslar bulunmakta.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // buton yer y�n de�i�tirme


public class GameManager : MonoBehaviour
{
    public Soru[] sorular; // soru scriptini gamemanagera ba�lamak i�in
    private static List<Soru> cevaplanmamisSorular;

    private Soru gecerliSoru;

    [SerializeField]
    private Text soruText;

    [SerializeField]
    private Text dogruCevapText, yanlisCevapText;

    [SerializeField]
    private GameObject dogruButon, yanlisButon;

    [SerializeField]
    private GameObject SonucPaneli;

    SonucManager sonucManager;

    int dogruAdet, yanlisAdet;

    int toplamPaun;

    void Start()
    {
        
        if(cevaplanmamisSorular==null || cevaplanmamisSorular.Count==0)
        {
            cevaplanmamisSorular = sorular.ToList<Soru>();
        }

        yanlisAdet = 0;
        dogruAdet = 0;
        toplamPaun = 0;

        RastgeleSoruSec();

    
    }

    IEnumerator SorularAras�BekleRouttine() // sorular aras� beklemek i�in
    {

        cevaplanmamisSorular.Remove(gecerliSoru); // her soru soruldu�un da ge�erli soru kalk�cak
        yield return new WaitForSeconds(1f);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ayn� sahneyi eklemek i�in 

        if (cevaplanmamisSorular.Count <= 0)
        {
            SonucPaneli.SetActive(true); // sonuc paneli, cevaplanmam��sorulare b�l�m� s�f�r olunca aktifle�ir

            sonucManager = Object.FindObjectOfType<SonucManager>(); // sonucmanageri sonu� paneli aktif olduktan sonra ula�mal�y�z 
            sonucManager.Sonuclar�Yazdir(dogruAdet, yanlisAdet, toplamPaun); // bu de�erleri g�stermeyi istiyoruz.
        }
        else
        {
            RastgeleSoruSec();
        }
    }

    void RastgeleSoruSec() // sorular i�inden rastgele soru se�mak i�in 
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(320f, .2f); // soruyu do�ru cevaplad���m�z da yanl�� butonu ekrandan kaybolup tekrar gelicek
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-320f, .2f);


        int RandomSoruIndexi = Random.Range(0, cevaplanmamisSorular.Count);
        gecerliSoru = cevaplanmamisSorular[RandomSoruIndexi];

        soruText.text = gecerliSoru.soru;

        if(gecerliSoru.dogrumu) // soruyu cevaplad���n�z da sonu� do�ru veya yanl�� ise sonucu belirticektir
        {
           
            dogruCevapText.text = "DO�RU CEVAPLADINIZ";
            yanlisCevapText.text = "YANLI� CEVAPLADINIZ";
        }
        else
        {
            dogruCevapText.text = "YANLI� CEVAPLADINIZ";
            yanlisCevapText.text = "DO�RU CEVAPLADINIZ";
        }
    }

    public void dogruButonaBasildi()
    {
        if (gecerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPaun += 100; // do�ru cevap verildi�in de 100 puan art�r�l�cak
        }
        else
        {
            yanlisAdet++;
        }
            StartCoroutine(SorularAras�BekleRouttine()); // butona bas�ld�ktan sonra ba�ka soru gelmesi i�in 
            yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f, .2f); // butonu sa�a �ekmek i�in 
        
    }
    
    public void yanlisButonaBasildi()
    {
        if (!gecerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPaun += 100;
        }
        else
        {
            yanlisAdet++;
        }        
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000f, .2f);
        StartCoroutine(SorularAras�BekleRouttine());
    }

    
}
