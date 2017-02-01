using UnityEngine;
using System.Collections;

public class WeaponUpgrade : MonoBehaviour
{
	[SerializeField]
	private EWeapon _type;

	public EWeapon Type{ get { return _type; } }

	// Update is called once per frame
	void Update ()
	{
		float movement = -1 * GameManager.Instance._BaseScrollSpeed * Time.deltaTime;
		Vector3 position = this.transform.position;
		position.y += movement;
		this.transform.position = position;

		if (this.transform.position.y < -Camera.main.orthographicSize - 1) {
			Destroy (this.gameObject);
		}
	}
}
