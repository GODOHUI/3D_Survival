using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time; //���� 0~1     0~100����  
    public float fullDayLength;  //��ü���� �Ϸ� ����
    public float startTime = 0.4f; // 0.5 �϶� 12�� ���� ���� 90��
    private float timeRate;  //
    public Vector3 noon;  //���� (90, 0 0)

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor; //���� �׶��̼� 
    public AnimationCurve sunIntensity;  //���� ���� �÷��ֱ�

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]   //2������ ������
    public AnimationCurve lightingIntensityMultiplier; //
    public AnimationCurve reflectionIntensityMultiplier;  //

    void Start()
    {
        timeRate = 1.0f / fullDayLength;  // �Ϸ� ���ð� 
        time = startTime; // ���۰��� 0.4�� ����  9�� ���� �Ѿ�� �ð� 
    }


    void Update()
    {
        //�ð� ����
        time = (time + timeRate * Time.deltaTime) % 1.0f;
        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    //�����  other���� ������Ʈ ���ִ��Լ�

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve) //�ҽ�������� �޾�����ϰ�
    {
        float intensity = intensityCurve.Evaluate(time); // intensityCurve.Evaluate�ȿ� ������ ���� �޾ƿ´�
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f;  //���� ���� 
                                                                                                        // �� 0.25?  0.5 �϶� 90�� �����̴� 360���� 0.5= 180 ��   360���� 0.25= 90 �� ���⼭   0.5 �� �����ָ� ���̱� ������ 0.75 �� ���´� ������(noon) 90�� �������Ѵ�
        lightSource.color = colorGradiant.Evaluate(time); //����ȯ
        lightSource.intensity = intensity;


        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)  //lightSource.intensity == 0 (��)���ǰ��� ��Ⱑ ���� ��������  activeInHierarchy �� ���������� 
        {

            go.SetActive(false);
        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy) //���� �����ְ�   go.activeInHierarchy �� ���������� 
        {
            go.SetActive(true);
        } 
    }
}
