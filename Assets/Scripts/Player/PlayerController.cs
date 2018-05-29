using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	public float maxSpeed = 3;
	public Rigidbody rb;
	public GameObject body;

	void Start()
	{
		moveSpeed = this.gameObject.GetComponent<PlayerStats> ().moveSpeed;
	}

	void Update() 
	{
		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
		transform.position = new Vector3 (body.transform.position.x, body.transform.position.y, body.transform.position.z);

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		//body.transform.Translate(x, 0, z);
		rb.AddForce(new Vector3(x,0,z) * moveSpeed);


		Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane=new Plane(Vector3.up, Vector3.zero);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			Vector3 target=ray.GetPoint(distance);
			Vector3 direction=target-transform.position;
			float rotation=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
			body.transform.rotation=Quaternion.Euler(0, rotation, 0);
		}
	}
}