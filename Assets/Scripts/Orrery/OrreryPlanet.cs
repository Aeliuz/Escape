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
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<SphereCollider>().isTrigger = false;
            inPosition = true;
        }
    }
}
