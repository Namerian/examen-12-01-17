using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
    public float _scrollSpeedModifier = 1;
    public float _tileSize = 6;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float scrollSpeed = -1 * GameManager.Instance._BaseScrollSpeed * this._scrollSpeedModifier;
        float movement = scrollSpeed * Time.deltaTime;

        float newYPos = this.transform.position.y + movement;

        if (newYPos > this._tileSize)
        {
            Vector3 newPos = this.transform.position;
            newPos.y = -this._tileSize + (newYPos - this._tileSize);
            this.transform.position = newPos;
        }
        else if (newYPos < this._tileSize)
        {
            Vector3 newPos = this.transform.position;
            newPos.y = this._tileSize - (newYPos - this._tileSize);
            this.transform.position = newPos;
        }
        else
        {
            //this.transform.Translate(this.transform.up * movement);
        }

        
    }
}
