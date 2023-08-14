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

    [Header("Own Tweeening")]
    public float downY = 0.5f;
    public float downDuration = 0.5f;
    public Ease downEase = Ease.OutElastic;

    private float normalY;

    public GameObject ground;

    public delegate void BiomeMoveAction(Transform biome, float amount, float duration, Ease ease);
    public static event BiomeMoveAction OnBiomeMoved;

    private void Start()
    {
        normalY = transform.position.y;
        downY = normalY - downY;
    }

    private void Update()
    {
        offsetX = ground.transform.localScale.x * 10;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicking " + this.name);

        transform.DOMoveY(downY, downDuration).SetEase(downEase);
    }

    private void OnMouseUp()
    {
        transform.DOMoveY(normalY, downDuration).SetEase(downEase);

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
