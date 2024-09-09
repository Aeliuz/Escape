using Meta.WitAi;
using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    [SerializeField] float lengthOfYear;
    [SerializeField] string planetName;
    public bool inPosition { get; private set; } = false;
    
    public float rotationRate()
    {
        return 360 / lengthOfYear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inPosition) return;
        if (other.name == planetName)
        {
            Destroy(other.gameObject);
            // ParticleEffect?
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(0).GetComponent<SphereCollider>().isTrigger = false;
            transform.GetChild(1).gameObject.SetActive(false);
            inPosition = true;
        }
    }
}
