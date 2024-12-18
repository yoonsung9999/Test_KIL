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
    // ���� �⺻ ����
    public string monName;
    //float�� �Ҽ���
    public float monMaxHp;
    //��Ʈ�� d�� ������ ���� ��°�� ����ȴ�
    public Image hpImage;

    public float Atk;

    public MonScript target;

    public TextMeshProUGUI hpText;

    public int reCnt;//�����ϸ� ������ Ǯ�� �ϱ�

    public float monCurHp;
    //HideInInspector:�����

    public float coolTime;

    // public float i = 10;
    // i = ����

    string die = "fuck";





    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = monName;
        monCurHp = monMaxHp;
        hpText.text = monCurHp + " / " + monMaxHp;
    }

    float cTime;

    // Update is called once per frame: �� �����Ӹ��� ������Ʈ ��
    void Update()
    {   
        if(monCurHp > 0)
            if (cTime >= coolTime)//������ ���� �Ѱ��� ���� �߰�ȣ �Ƚᵵ �ȴ�
            {
                //monCurHp--;//���� �Ǹ� ���� ��´�
                //float fill = monCurHp / monMaxHp;
                //hpImage.fillAmount = fill;
                ////1���� Ŀ���� �̹����� ���̸鼭 0���� �ٽ� �ʱ�ȭ ��Ų��
                //cTime = 0;
                //kjhT /= 2f;
                ////f�� ���̴°� float�̴ٶ�� ��
                ///
                target.OnDamage( Atk );
                //Ÿ���� ���Ϳ� �����ϰ� ���� �µ����� �Լ��� �������
                cTime = 0;
            }
            else
            {
                cTime += Time.deltaTime;

            }
    }

    public void OnDamage(float _Atk)//�Ű������� ������ �������� ���θ� ��
    {
        monCurHp -= _Atk;//���� �Ǹ� ���� ��´�
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
        // �Լ��� void��� �Ѵ�


    }
}   


