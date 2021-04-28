using System.Collections;
using UnityEngine;

public class HighWall : MonoBehaviour
{
    public float startAlpha = 1f;
    public int renderOrder = 0;

    private bool faded = false;

    private Material _material;
    private Color _color;

    public bool Faded { get => faded;}

    void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.renderQueue += renderOrder;
        _color = _material.color;
        _color.a = startAlpha;
        SetTranspMaterialColour();
    }

    //There is no point in setting the colour every frame
    private void SetTranspMaterialColour()
    {
        _material.SetColor("_BaseColor", _color);
    }

    public void FadeOut(float alpha)
    {
        faded = true;
        gameObject.layer = LayerMask.NameToLayer("TransparentWall");
        StartCoroutine(GraduallyFadeOut(alpha));
    }

    public void FadeIn()
    {
        faded = false;
        gameObject.layer = LayerMask.NameToLayer("Wall");
        StartCoroutine(GraduallyFadeIn());
    }

    private IEnumerator GraduallyFadeIn()
    {
        _color = _material.color;
        while (_color.a < startAlpha)
        {
            _color = _material.color;
            _color.a += 0.01f;
            SetTranspMaterialColour();
            yield return new WaitForEndOfFrame();
        }
        _color.a = startAlpha;
        SetTranspMaterialColour();
    }

    private IEnumerator GraduallyFadeOut(float alpha)
    {
        _color = _material.color;
        while (_color.a > alpha)
        {
            _color = _material.color;
            _color.a -= 0.01f;
            SetTranspMaterialColour();
            yield return new WaitForEndOfFrame();
        }
    }
}