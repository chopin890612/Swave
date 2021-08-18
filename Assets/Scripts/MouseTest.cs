using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseTest : MonoBehaviour
{
    public Image Image;
    private Spawner spawner;
    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        origin = Image.rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))//WhenDown
        {
            Image.rectTransform.localPosition += new Vector3(Input.GetAxisRaw("Mouse X"),0,0)*3f;
            if(spawner.XXSPPEED < 5 && spawner.XXSPPEED > -5)
            {
                //Debug.Log(Image.rectTransform.localPosition.x);

                if(Image.rectTransform.localPosition.x > 20)
                    spawner.XXSPPEED -= 0.1f;
                if(Image.rectTransform.localPosition.x < -20)
                    spawner.XXSPPEED += 0.1f;
            }
        }
        if (!Input.GetMouseButton(0))//WhenUp
        {
            Image.rectTransform.localPosition = new Vector3( Mathf.Lerp(Image.rectTransform.localPosition.x, origin.x, 0.1f),origin.y,origin.z);
        }
        if (Input.GetMouseButtonUp(0))//UpTrigger
        {
            spawner.XXSPPEED = 0;
        }
    }
}
