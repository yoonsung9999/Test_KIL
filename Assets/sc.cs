using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//int   float   bool    string
public class Monster : MonoBehaviour
{
    //몬스터 기본정보
    public string monName;
    public float monMaxHp;
    public Image hpImage;
    public Monster target;
    public float monCurHp;
    public float coolTime;
    public float monAtkPower;
    public TextMeshProUGUI hpText;
    public string die;
    
    public void OnDamage( float _monAtkPower ) 
    {

        monCurHp -= _monAtkPower;
        float fill = monCurHp / monMaxHp;
        hpImage.fillAmount = fill;
        {
            hpText.text = die;
        }
    }



    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = monName;
        hpText.text = monCurHp + " / " + monMaxHp;
    }
    float cTime;
    void Update()
    {
        if (monCurHp > 0)
        {
            cTime = Time.deltaTime;
            if (cTime >= coolTime)
            {
                target.OnDamage(monAtkPower);
                cTime = 0;
            }
            else
            {
                cTime += Time.deltaTime;
            }
        }
    }
}
   



  


