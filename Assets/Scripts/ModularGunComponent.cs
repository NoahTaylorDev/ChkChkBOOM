using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ModularGunComponent : MonoBehaviour
{

    [SerializeField] private GameObject scope;
    [SerializeField] private GameObject stock;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject mag;
    [SerializeField] private GameObject grip;
    [SerializeField] private Transform firepoint;

    //scope: range, accuracy, and spread
    //stock: spread, accuracy and kick
    //barrel spread, accuracy, range
    //mag: bullet type, ammo count
    //grip: spread? Visual?

    void Start()
    {
        //add all of the sprite initalizations for the UpdateGunVisuals.
    }

        private void UpdateGunVisuals()
    {
        //update will look something like scopesprite
    }
}
