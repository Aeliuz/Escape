using Meta.WitAi;
using System.Collections.Generic;
using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    [SerializeField] List<Transform> orbiters = new List<Transform>();    
    [SerializeField] float lengthOfYear;
    [SerializeField] string planetName;
    private bool occupied = false;
    public bool inPosition { get; private set; } = false;
    
    public float rotationRate()
    {
        return 360 / lengthOfYear;
    }

    public void AddedPlanet()
    {
        occupied = true;

        foreach (Transform orbiter in orbiters)
        {
            Vector3 magCheck = new Vector3(0.05f, 0.05f, 0.05f);
            if (orbiter.position.magnitude - this.gameObject.transform.GetChild(0).position.magnitude <= magCheck.magnitude)
            {
                if (orbiter.gameObject.name == planetName)
                    inPosition = true;
            }
        }
        Debug.LogWarning("Added Planet! In position? " + inPosition);
    }

    public void RemovedPlanet()
    {
        occupied = false;
        inPosition = false;
        Debug.LogWarning("Removed Planet!");
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (occupied) return;
    //    if (other.gameObject.name == planetName)
    //    {
    //        Destroy(other.gameObject);
    //        // ParticleEffect?
    //        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    //        transform.GetChild(0).GetComponent<SphereCollider>().isTrigger = false;
    //        transform.GetChild(1).gameObject.SetActive(false);
    //        occupied = true;
    //        inPosition = true;
    //    }
    //    else if (other.gameObject.tag == "Planet")
    //    {
    //        other.gameObject.GetComponent<Rigidbody>().useGravity = false;      // Somehow. this must be set to true when the object is grabbed the next time
    //        other.gameObject.transform.position = this.transform.position;
    //        occupied = true;
    //    }
    //    else Debug.LogWarning(other.gameObject.name);
    //}
}
