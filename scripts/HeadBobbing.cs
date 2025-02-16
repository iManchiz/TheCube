using System.Collections;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public float midpoint = 2.0f;

    private float timer = 0.0f;
    private float waveslice;
    private float horizontal;
    private float vertical;
    private Vector3 cSharpConversion;
    private float totalAxes;
    private float translateChange;

    void Update()
    {
        waveslice = 0.0f;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }

        if (waveslice != 0)
        {
            translateChange = waveslice * bobbingAmount;
            totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion = transform.localPosition;
            cSharpConversion.y = midpoint + translateChange;
            transform.localPosition = cSharpConversion;
        }
        else
        {
            cSharpConversion = transform.localPosition;
            cSharpConversion.y = midpoint;
            transform.localPosition = cSharpConversion;
        }
    }
}
