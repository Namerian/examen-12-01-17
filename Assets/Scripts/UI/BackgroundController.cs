using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
    public float _scrollSpeedModifier = 1;
    public float _tileSize = 6;

    private float _startY;
    private float _halfSize;

    // Use this for initialization
    void Start()
    {
        _startY = this.transform.position.y;
        _halfSize = _tileSize * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = -1 * GameManager.Instance._BaseScrollSpeed * _scrollSpeedModifier * Time.deltaTime;

        Vector3 newPos = this.transform.position;

        if(newPos.y + movement < _startY - _halfSize)
        {
            newPos.y = _startY + _halfSize + ((newPos.y + movement) - (_startY - _halfSize));
        }
        else if(newPos.y + movement > _startY + _halfSize)
        {

        }
        else
        {
            newPos.y += movement;
        }

        this.transform.position = newPos;


        /*float scrollSpeed = -1 * GameManager.Instance._BaseScrollSpeed * this._scrollSpeedModifier;
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
        }*/

        
    }
}
