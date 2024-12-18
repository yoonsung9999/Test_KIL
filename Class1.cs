using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    [SerializeField] TextAsset gachaTable;
    [SerializeField] TextAsset chaTable;

    Dictionary<int, Gacha> gachaRateDic;
    Dictionary<int, Character> chaDic;

    void SettingGachaRate(List<Dictionary<string, object>> csvFile)
    {
        for (int i = 0; i < csvFile.Count; i++)
        {
            Gacha newGacha = new Gacha();
            int _id = (int)csvFile[i]["id"]; // 정수형으로 바꿔
            int _rate = (int)csvFile[i]["rate"];
            newGacha.gachaId = _id;

            if (!gachaRateDic.ContainsKey(_id))
            {
                gachaRateDic.Add(_id, newGacha);
            }

            gachaRateDic[_id].gradeRate.Add(_rate);
        }
    }

    void SettingCha(List<Dictionary<string, object>> readData)
    {
        for (int i = 0; i < readData.Count; i++)
        {
            int _id = (int)readData[i]["id"];
            string _name = readData[i]["name"].ToString();
            int _grade = (int)readData[i]["grade"];
            int _hp = (int)readData[i]["hp"];
            int _atkPower = (int)readData[i]["atkPower"];
            int _criChan = (int)readData[i]["criChan"];
            int _criMul = (int)readData[i]["criMul"];

            Character newChar = new Character(_id, _grade, _hp, _atkPower,
                _criChan, _criMul, _name);

            chaDic.Add(_id, newChar);
        }
    }

    private void Start()
    {
        SettingGachaRate(CSVReader.Read(gachaTable));
        SettingCha(CSVReader.Read(chaTable));
    }

}

public class Gacha
{
    public int gachaId;
    public List<int> gradeRate;
}


public class Character
{
    int chaId;
    int grade;
    int hp;
    int atkPower;
    int criChan, criMul;
    string name;

    public Character(int _id, int _grade, int _hp, int _atkPower,
        int _criChan, int _criMul, string _name)
    {
        chaId = _id;
        grade = _grade;
        hp = _hp;
        atkPower = _atkPower;
        criChan = _criChan;
        criMul = _criMul;
        name = _name;
    }

}