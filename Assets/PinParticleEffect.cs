using UnityEngine;

public class PinCollision : MonoBehaviour
{
    public GameObject hitEffectPrefab;

    private bool hit = false;

    void OnCollisionEnter(Collision collision)
    {

        if (hit) return;
        if (collision.gameObject.name == "Ball")
        {
            hit = true;
            Instantiate(hitEffectPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            CameraShake.Instance.Shake();

        }
    }
}