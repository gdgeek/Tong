using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public Number _number = null;
    public void setScore(int score) {
        _number.number = score;
    }
}
