using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sym4D;

public class Sym4DController : MonoBehaviour
{
    public InputField portNo;
    public InputField roll;
    public InputField pitch;

    //바람 세기
    public InputField windSpeed;

    private int xPort;
    private int wPort;

    private WaitForSeconds ws = new WaitForSeconds(0.2f);

    IEnumerator Start()
    {
        //필드 초기화
        roll.text = "0";
        pitch.text = "0";
        windSpeed.text = "10";

        yield return ws;

        //의자의 포트번호 추출
        xPort = Sym4DEmulator.Sym4D_X_Find();
        portNo.text = xPort.ToString();

        yield return ws;

        //팬의 포트번호 추출
        wPort = Sym4DEmulator.Sym4D_W_Find();

        //회전 각도를 설정 (roll -10 ~ +10, pith -10 ~ +10)
        Sym4DEmulator.Sym4D_X_SetConfig(100, 100);

        //팬속도 설정 (0 ~ 100)
        Sym4DEmulator.Sym4D_W_SetConfig(100);
    }
    /*
        Yaw    Y
        Roll   Z
        Pitch  x
    */


    IEnumerator StartTest()
    {
        int _roll = int.Parse(roll.text);
        int _pitch = int.Parse(pitch.text);

        //의자 동작 준비
        Sym4DEmulator.Sym4D_X_StartContents(xPort);
        yield return ws;

        //의자 동작
        Sym4DEmulator.Sym4D_X_SendMosionData(_roll, _pitch);
        yield return ws;
    }

    public void OnStartTest()
    {
        StartCoroutine(this.StartTest());
    }

}
