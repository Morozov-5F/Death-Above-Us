using UnityEngine;
using System.Collections;

public class ExplosionRemover : MonoBehaviour {
    public float delay = 3f;

	void Update () {
        if (delay <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }
}
