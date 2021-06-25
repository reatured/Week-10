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

    public int[] colorLevel = new int[] { 50, 180, 255 };

    //public Gradient gradient = new Gradient();
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
                spawnObject = Instantiate(PlacedPrefab, hitPose.position, Quaternion.Euler(Vector3.up));



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


            colorHolder = new Color(getRandomNum(), getRandomNum(), getRandomNum());

            var fireDarkPSMain = spawnObject.transform.GetChild(0).GetChild(3).GetComponent<ParticleSystem>().main;
            fireDarkPSMain.startColor = colorHolder;

            //var trailPSMain = spawnObject.GetComponent<particleSystemController>().getPS(1).trails;


            //gradient.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(colorHolder, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            //);

            //Debug.Log("gradient: " + trailPSMain.colorOverTrail.gradient.colorKeys);

            //trailPSMain.colorOverTrail = gradient;

            spawnObject.GetComponent<particleSystemController>().playAllOnce();
        }

    }

    public int getRandomNum()
    {

        return colorLevel[Random.Range(0,3)];
    }



}
