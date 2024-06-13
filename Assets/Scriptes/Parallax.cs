using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    //public Camera cam;

    /* float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject)/ clippingPlane; */

    public void start()
    {
        /* startPosition = transform.position;
        startZ = transform.position.z; */
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void FixedUpdate()
    {
        /* Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x,newPos.y,startZ); */
        //float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        //if(temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -=length;
    }

}
