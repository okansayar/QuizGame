using System.Collections;
using System.Collections.Generic;
using System.Linq; // sorualrý listeye atmak ve listeyi düzenlemek için oluþturulan claslar bulunmakta.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // buton yer yön deðiþtirme


public class GameManager : MonoBehaviour
{
    public Soru[] sorular; // soru scriptini gamemanagera baðlamak için
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

    IEnumerator SorularArasýBekleRouttine() // sorular arasý beklemek için
    {

        cevaplanmamisSorular.Remove(gecerliSoru); // her soru sorulduðun da geçerli soru kalkýcak
        yield return new WaitForSeconds(1f);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ayný sahneyi eklemek için 

        if (cevaplanmamisSorular.Count <= 0)
        {
            SonucPaneli.SetActive(true); // sonuc paneli, cevaplanmamýþsorulare bölümü sýfýr olunca aktifleþir

            sonucManager = Object.FindObjectOfType<SonucManager>(); // sonucmanageri sonuç paneli aktif olduktan sonra ulaþmalýyýz 
            sonucManager.SonuclarýYazdir(dogruAdet, yanlisAdet, toplamPaun); // bu deðerleri göstermeyi istiyoruz.
        }
        else
        {
            RastgeleSoruSec();
        }
    }

    void RastgeleSoruSec() // sorular içinden rastgele soru seçmak için 
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(320f, .2f); // soruyu doðru cevapladýðýmýz da yanlýþ butonu ekrandan kaybolup tekrar gelicek
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-320f, .2f);


        int RandomSoruIndexi = Random.Range(0, cevaplanmamisSorular.Count);
        gecerliSoru = cevaplanmamisSorular[RandomSoruIndexi];

        soruText.text = gecerliSoru.soru;

        if(gecerliSoru.dogrumu) // soruyu cevapladýðýnýz da sonuç doðru veya yanlýþ ise sonucu belirticektir
        {
           
            dogruCevapText.text = "DOÐRU CEVAPLADINIZ";
            yanlisCevapText.text = "YANLIÞ CEVAPLADINIZ";
        }
        else
        {
            dogruCevapText.text = "YANLIÞ CEVAPLADINIZ";
            yanlisCevapText.text = "DOÐRU CEVAPLADINIZ";
        }
    }

    public void dogruButonaBasildi()
    {
        if (gecerliSoru.dogrumu)
        {
            dogruAdet++;
            toplamPaun += 100; // doðru cevap verildiðin de 100 puan artýrýlýcak
        }
        else
        {
            yanlisAdet++;
        }
            StartCoroutine(SorularArasýBekleRouttine()); // butona basýldýktan sonra baþka soru gelmesi için 
            yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f, .2f); // butonu saða çekmek için 
        
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
        StartCoroutine(SorularArasýBekleRouttine());
    }

    
}
