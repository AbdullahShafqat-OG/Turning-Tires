using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CoinData")]
public class CoinData : ScriptableObject
{
    public int value;
    public float moveDistance = 0.5f;
    public float moveDuration = 1.0f;
    public Vector3 rotationAmount = new Vector3(0, 180, 0);
    public float rotationDuration = 1.0f;
    public DG.Tweening.Ease rotationEase = DG.Tweening.Ease.Linear; 
}
