using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public float dayCount;
    public float dayTime;
    private void Start()
    {
        dayCount = 0;
        dayTime = 5.00f;
        StartCoroutine("DayNight");
        StartCoroutine("Rotation");
    }
    private IEnumerator Rotation()
    {
        while (true)
        {
            transform.Rotate(Vector3.right, 5.0f);
            yield return new WaitForSeconds(1.00f);
        }
    }

    private IEnumerator DayNight()
    {
        while (true)
        {
            dayTime += 1;
            if (dayTime >= 24)
            {
                dayTime = 0;
                dayCount += 1;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
