using Meta.WitAi;
using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    [SerializeField] float lengthOfYear;
    [SerializeField] string planetName;
    private bool occupied = false;
    public bool inPosition { get; private set; } = false;
    
    public float rotationRate()
    {
        return 360 / lengthOfYear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (occupied) return;
        if (other.gameObject.name == planetName)
        {
            Destroy(other.gameObject);
            // ParticleEffect?
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(0).GetComponent<SphereCollider>().isTrigger = false;
            transform.GetChild(1).gameObject.SetActive(false);
            occupied = true;
            inPosition = true;
        }
        else if (other.gameObject.tag == "Planet")
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;      // Somehow. this must be set to true when the object is grabbed the next time
            other.gameObject.transform.position = this.transform.position;
            occupied = true;
        }
        else Debug.LogWarning(other.gameObject.name);
    }
}
