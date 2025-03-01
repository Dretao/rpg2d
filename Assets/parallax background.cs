using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxbackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1-parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distance, transform.position.y);

        if(distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if(distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }//如果超过了边界，就把背景图移到另一边
    }
}
