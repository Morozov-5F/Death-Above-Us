using UnityEngine;

public class Turret : MonoBehaviour 
{
    public float MAX_ANGLE = 65f;
    public uint MAX_HP = 100;
	public float INPUT_SENSITIVITY = 1f;
    protected Weapon weapon;
    protected float hp;
	public Transform turret;
    
    
	public void Start () 
	{
        hp = (float)MAX_HP;
        weapon = GetComponentInChildren<Weapon>();
        turret = transform.Find("turret");
    }

    public void Rotate(float angle)
    {
        var currentRotation = turret.transform.localEulerAngles.z;
        currentRotation += angle * INPUT_SENSITIVITY;
        currentRotation = Mathf.Clamp(currentRotation > 180 ? currentRotation - 360 : currentRotation, -MAX_ANGLE, MAX_ANGLE);
        turret.transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
    }

    public void OnBulletHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log("Test");
            Destroy(gameObject);
        }   
    }

}
