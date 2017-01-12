using UnityEngine;
using System.Collections;

public class WeaponUpgrade : MonoBehaviour
{
    public Weapon _type;

    // Use this for initialization
    /*void Start()
    {

    }*/

    // Update is called once per frame
    void Update()
    {
        float movement = -1 * GameManager.Instance._BaseScrollSpeed * Time.deltaTime;
        Vector3 position = this.transform.position;
        position.y += movement;
        this.transform.position = position;

        if (this.transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
            playerController.UpgradeWeapon(_type);

            Destroy(this.gameObject);
        }
    }
}
