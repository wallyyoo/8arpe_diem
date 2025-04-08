using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        int[] arr = {0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10};
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int k = i * 4 + j;
                float x = j * 1.4f - 2.1f;
                float y = i * 1.4f - 4.06f;

                GameObject go = Instantiate(card, new Vector2(0f, -3.5f), new Quaternion(0f, 0f, 0f, 0f), this.transform);
                //go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().setting(arr[k]);
                go.GetComponent<Card>().GetNum(x, y);
            }
        }
    }
}
