using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        Text text;                      // Reference to the Text component.


        void Awake ()
        {
            // Set up the reference.
            text = GetComponent <Text> ();
        }


        void Update ()
        {
			// Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + Score.score;
        }
    }

	public class Score
	{
		public static int score = 0;

		public static void AddScore(int add)
		{
			score += add;
		}
	}
}