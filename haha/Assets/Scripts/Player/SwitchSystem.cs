using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSystem : MonoBehaviour
{

	int index = 0;
	GameObject[] barrels;

	void Start()
	{
		barrels = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			barrels[i] = transform.GetChild(i).gameObject;
		}
	}

	void Update ()
	{
		if (Input.GetButtonDown("WpnUp"))
		{
			Rotate(1);
        }
		else if (Input.GetButtonDown("WpnDown"))
		{
			Rotate(-1);
		}
	}

	void Rotate(int dir)
	{
		barrels[index].SetActive(false);

		index += dir;
		if (index < 0)
		{
			index = barrels.Length - 1;
		}
		else if (index > barrels.Length - 1)
		{
			index = 0;
		}

		barrels[index].SetActive(true);
	}
}
