using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();

        //TextAsset data = Resources.Load (file) as TextAsset;
        TextAsset = file;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
public class MonScript : MonoBehaviour
{
    // 몬스터 기본 정보
    public string monName;
    //float은 소수점
    public float monMaxHp;
    //컨트롤 d를 누르면 줄이 통째로 복사된다
    public Image hpImage;

    public float Atk;

    public MonScript target;

    public TextMeshProUGUI hpText;

    public int reCnt;//웬만하면 변수명 풀로 하기

    public float monCurHp;
    //HideInInspector:숨기기

    public float coolTime;

    // public float i = 10;
    // i = 변수

    string die = "fuck";





    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = monName;
        monCurHp = monMaxHp;
        hpText.text = monCurHp + " / " + monMaxHp;
    }

    float cTime;

    // Update is called once per frame: 매 프레임마다 업데이트 됨
    void Update()
    {   
        if(monCurHp > 0)
            if (cTime >= coolTime)//실행할 것이 한개만 들어가면 중괄호 안써도 된다
            {
                //monCurHp--;//몬스터 피를 먼저 깎는다
                //float fill = monCurHp / monMaxHp;
                //hpImage.fillAmount = fill;
                ////1보다 커지면 이미지를 줄이면서 0으로 다시 초기화 시킨다
                //cTime = 0;
                //kjhT /= 2f;
                ////f를 붙이는건 float이다라는 뜻
                ///
                target.OnDamage( Atk );
                //타겟의 몬스터에 접근하고 계의 온데미지 함수를 실행시켜
                cTime = 0;
            }
            else
            {
                cTime += Time.deltaTime;

            }
    }

    public void OnDamage(float _Atk)//매개변수가 없으면 공란으로 비어두면 됨
    {
        monCurHp -= _Atk;//몬스터 피를 먼저 깎는다
        float fill = monCurHp / monMaxHp;
        hpImage.fillAmount = fill;
        if (monCurHp <= 0)
        {
            if (reCnt > 0)
            {
                monCurHp = monMaxHp / 2f; // monCurHp = monMaxHp * 0.5f;
                reCnt--;
                hpText.text = monCurHp + " / " + monMaxHp;
            }
            else
            {
                hpText.text = die;
            }
        }
        else
        {
            hpText.text = monCurHp + " / " + monMaxHp;
        }
        // 함수를 void라고 한다


    }
}   


