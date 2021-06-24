using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


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
    void Update()
    {
        if(GetTouch(out Vector2 touch_pos)== false)
        {
            return;
        }

        if(m_raycastmanager.Raycast(touch_pos, hits, TrackableType.Planes))
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
}
