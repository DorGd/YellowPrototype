using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighWall : MonoBehaviour
{
    public float startAlpha = 1f;
    public int renderOrder = 0;

    private Coroutine _activeFadingCoroutine = null;
    private List<Material> _materials = new List<Material>();
    private List<Color> _colors = new List<Color>();
    private float _currentAlpha = 0.0f;

    void Awake()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _currentAlpha = startAlpha;
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            foreach (Material material in meshRenderer.materials)
            {
                material.renderQueue += renderOrder;
                Color color = material.color;
                color.a = startAlpha;
                _materials.Add(material);
                _colors.Add(color);
            }
        }
        SetTranspMaterialColour();
    }

    //There is no point in setting the colour every frame
    private void SetTranspMaterialColour()
    {
        for (int i = 0; i < _materials.Count; i++)
        {
            Material material = _materials[i];
            material.SetColor("_BaseColor", _colors[i]);
        }
    }

    public void FadeOut(float alpha)
    {
        gameObject.layer = LayerMask.NameToLayer("TransparentWall");
        if (_activeFadingCoroutine != null)
        {
            StopCoroutine(_activeFadingCoroutine);
        }
        _activeFadingCoroutine = StartCoroutine(GraduallyFadeOut(alpha));
    }

    public void FadeIn()
    {
        gameObject.layer = LayerMask.NameToLayer("Wall");
        if (_activeFadingCoroutine != null)
        {
            StopCoroutine(_activeFadingCoroutine);
        }
        _activeFadingCoroutine = StartCoroutine(GraduallyFadeIn());
    }

    private IEnumerator GraduallyFadeIn()
    {
        while (_currentAlpha < startAlpha)
        {
            _currentAlpha += 0.01f;
            for (int i = 0; i < _colors.Count; i++)
            {
                Color color = _colors[i];
                color.a = _currentAlpha;
                _colors[i] = color;
            }
            SetTranspMaterialColour();
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < _colors.Count; i++)
        {
            Color color = _colors[i];
            color.a = startAlpha;
            _colors[i] = color;
        }
        SetTranspMaterialColour();
    }

    private IEnumerator GraduallyFadeOut(float targetAlpha)
    {
        while (_currentAlpha > targetAlpha)
        {
            _currentAlpha -= 0.01f;
            for (int i = 0; i < _colors.Count; i++)
            {
                Color color = _colors[i];
                color.a = _currentAlpha;
                _colors[i] = color;
            }
            SetTranspMaterialColour();
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < _colors.Count; i++)
        {
            Color color = _colors[i];
            color.a = targetAlpha;
            _colors[i] = color;
        }
        SetTranspMaterialColour();
    }
}