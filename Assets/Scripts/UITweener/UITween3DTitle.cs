using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITween3DTitle : MonoBehaviour
{
    public float finalY;
    public float duration;
    public Ease ease;

    private float y;

    private void OnEnable()
    {
        UIBiomeBtn.OnBiomeMoved += SimulateKeyPress;
    }

    private void OnDisable()
    {
        UIBiomeBtn.OnBiomeMoved -= SimulateKeyPress;
    }

    private void Start()
    {
        y = transform.position.y;

        transform.DOMoveY(finalY, duration).SetEase(ease);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            (finalY, y) = (y, finalY);
            transform.DOMoveY(finalY, duration).SetEase(ease);
        }
    }

    public void SimulateKeyPress(Transform biome, float amount, float d, Ease e)
    {
        (finalY, y) = (y, finalY);
        transform.DOMoveY(finalY, duration).SetEase(ease).OnComplete(() => MoveBiome(biome, amount, d, e));
    }

    public void MoveBiome(Transform biome, float amount, float duration, Ease ease)
    {
        biome.DOMoveX(amount, duration).SetEase(ease).OnComplete(() => EndMotion());
    }

    public void EndMotion()
    {
        (finalY, y) = (y, finalY);
        transform.DOMoveY(finalY, duration).SetEase(ease);
    }
}
