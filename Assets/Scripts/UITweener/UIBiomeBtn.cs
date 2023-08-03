using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIBiomeBtn : MonoBehaviour
{
    public GameObject biome;
    public float offsetX;
    public float duration = 1f;
    public Ease ease = Ease.Linear;

    public GameObject ground;

    public delegate void BiomeMoveAction(Transform biome, float amount, float duration, Ease ease);
    public static event BiomeMoveAction OnBiomeMoved;

    private void Update()
    {
        offsetX = ground.transform.localScale.x * 10;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicking " + this.name);

        if (this.name.Contains("Right"))
        {
            OnBiomeMoved(biome.transform, biome.transform.position.x - offsetX, duration, ease);
            //biome.transform.DOMoveX(biome.transform.position.x - offsetX, duration).SetEase(ease).OnComplete(() => OnBiomeMoved());
        }
        else if (this.name.Contains("Left"))
        {
            OnBiomeMoved(biome.transform, biome.transform.position.x + offsetX, duration, ease);
            //biome.transform.DOMoveX(biome.transform.position.x + offsetX, duration).SetEase(ease).OnComplete(() => OnBiomeMoved());
        }
    }
}
