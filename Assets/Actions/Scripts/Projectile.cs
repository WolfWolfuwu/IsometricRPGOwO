using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Actions/Projectile")]
public class Projectile : ScriptableAction
{
    public GameObject Particleprefab;
    public int Damage;
    private GameObject particleinstance;
    private ParticleSystem particle;

    public override void CallMethod(GameObject Source) {
        if(particleinstance == null) {
            particleinstance = Instantiate(Particleprefab, Source.transform.position, Quaternion.identity);
            particle = particleinstance.GetComponent<ParticleSystem>();
            particleinstance.transform.SetParent(Source.transform);
        }
        particleinstance.transform.rotation = MouseManager.instance.PointAtMouse(Source.transform.position);
        particle.Play();
    }
}
