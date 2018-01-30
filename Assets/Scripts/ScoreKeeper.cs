using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	public int score = 0;

	private Text scoreText;

	private void Start()
	{
		// Store the attached text component from the inspector to scoreText
		scoreText = GetComponent<Text>();
		scoreText.text = "0";
	}

	void Score(int points)
	{
		// Add the new points to the existing score and update the text
		score += points;
		scoreText.text = score.ToString();
	}
	
	void Reset () {
		score = 0;
	}
}
