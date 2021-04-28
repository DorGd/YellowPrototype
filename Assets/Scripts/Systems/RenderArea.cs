using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderArea : MonoBehaviour
{
    public List<GameObject> mStartRenderingList;
    public List<GameObject> mStopRenderingList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        { 
            foreach (GameObject target in mStartRenderingList)
            {
                target.SetActive(true);
            }
            foreach (GameObject target in mStopRenderingList)
            {
                target.SetActive(false);
            }
        }
    }
}
