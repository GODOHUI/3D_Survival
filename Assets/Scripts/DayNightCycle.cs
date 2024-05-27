using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time; //범위 0~1     0~100프로  
    public float fullDayLength;  //전체적인 하루 길이
    public float startTime = 0.4f; // 0.5 일때 12시 정오 각도 90도
    private float timeRate;  //
    public Vector3 noon;  //정오 (90, 0 0)

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor; //조명 그라데이션 
    public AnimationCurve sunIntensity;  //조명 강도 늘려주기

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]   //2가지값 값조정
    public AnimationCurve lightingIntensityMultiplier; //
    public AnimationCurve reflectionIntensityMultiplier;  //

    void Start()
    {
        timeRate = 1.0f / fullDayLength;  // 하루 세팅값 
        time = startTime; // 시작값을 0.4로 세팅  9시 조금 넘어가는 시간 
    }


    void Update()
    {
        //시간 증가
        time = (time + timeRate * Time.deltaTime) % 1.0f;
        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    //조명과  other세팅 업데이트 해주는함수

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve) //소스를만들고 받아줘야하고
    {
        float intensity = intensityCurve.Evaluate(time); // intensityCurve.Evaluate안에 보관된 값을 받아온다
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f;  //각도 조정 
                                                                                                        // 왜 0.25?  0.5 일때 90도 정오이다 360도의 0.5= 180 도   360도의 0.25= 90 도 여기서   0.5 를 더해주면 밤이기 때문에 0.75 가 나온다 정오의(noon) 90도 값을곱한다
        lightSource.color = colorGradiant.Evaluate(time); //색상변환
        lightSource.intensity = intensity;


        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)  //lightSource.intensity == 0 (밤)빛의강도 밝기가 완전 없어지고  activeInHierarchy 이 켜져있으면 
        {

            go.SetActive(false);
        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy) //빛이 켜져있고   go.activeInHierarchy 가 꺼져있으면 
        {
            go.SetActive(true);
        } 
    }
}
