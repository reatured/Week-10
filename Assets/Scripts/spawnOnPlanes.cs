using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


[RequireComponent(typeof(ARRaycastManager))]


public class spawnOnPlanes : MonoBehaviour
{
    [SerializeField]
    GameObject PlacedPrefab;

    GameObject spawnObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARRaycastManager m_raycastmanager;

    // Start is called before the first frame update
    void Awake()
    {
        m_raycastmanager = GetComponent<ARRaycastManager>();
    }

    bool GetTouch(out Vector2 touch_pos)
    {
        if (Input.touchCount > 0)
        {
            touch_pos = Input.GetTouch(0).position;
            return true;
        }
        touch_pos = default;
        return false;

    }
    // Update is called once per frame
    void FixedUpdate()
    {


        

        if(GetTouch(out Vector2 touch_pos)== false)
        {
            return;
        }

        bool isOverUI = touch_pos.IsPointOverUIObject();

        if (isOverUI)
        {

            Debug.Log("Stop Raycast cause UI hit");
            return;
        }


        if (m_raycastmanager.Raycast(touch_pos, hits, TrackableType.Planes))
        {
            


            var hitPose = hits[0].pose;

            if(spawnObject == null)
            {
                spawnObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);



            }
            else
            {
                spawnObject.transform.position = hitPose.position;

            }

        }
    }

    public void ChangeColor()
    {
        if (spawnObject != null)
        {
            Color colorHolder = new Color(getRandomNum(), getRandomNum(), getRandomNum());
            var main = spawnObject.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().main;
            main.startColor = colorHolder;
            Debug.Log("Fire Color: " + colorHolder.ToString());


            colorHolder = new Color(getRandomNum(), getRandomNum(), getRandomNum());
            var fireDarkPSMain = spawnObject.transform.GetChild(0).GetChild(3).GetComponent<ParticleSystem>().main;
            fireDarkPSMain.startColor = colorHolder;
            Debug.Log("Fire Dark Color: " + colorHolder.ToString());
        }

    }

    public int getRandomNum()
    {

        return Random.Range(0, 250);
    }

    //public Color getRandomColor()
    //{
    //    List<Color> colors = new List<Color>();

    //    colors[0] = 


    //    return colorHolder;
    //}

   
}
