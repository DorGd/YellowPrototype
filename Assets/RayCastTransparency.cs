using System.Collections.Generic;
using UnityEngine;

public class RayCastTransparency : MonoBehaviour
{
    public Transform raytarget; // Create a "field" for the raytarget (ie the camera) to be placed
    public LayerMask solidWallLayerMask;
    public LayerMask transparentWallLayerMask;

    private List<HighWall> fadedWalls = new List<HighWall>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Declaring variables that are going to be used
        RaycastHit hitWall; // where the hit target is going to be stored

        Vector3 toCamera; // create the Vector3 we're going to use
        toCamera = raytarget.position - transform.position; // transform.position is the xyz coordinate inside the room of the calling object... so subtracting the target gives us a VECTOR! yay!

        //Debug.DrawRay(transform.position, toCamera, Color.green, 20f); // Draw the ray

        Ray checkRay = new Ray(transform.position, toCamera); // Create the ray variable to be used in the next line
        if (Physics.Raycast(checkRay, out hitWall, 100, solidWallLayerMask))
        {
            HighWall highwall = hitWall.transform.GetComponent<HighWall>();
            if (highwall != null)
            {
                highwall.FadeOut(0.4f);
                fadedWalls.Add(highwall);
            }
        }
        else if (!Physics.Raycast(checkRay, 100, transparentWallLayerMask))
        {
            foreach (HighWall wall in fadedWalls)
            {
                wall.FadeIn();
            }
            fadedWalls.Clear();
        }

    }
}