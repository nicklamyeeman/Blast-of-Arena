using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextAnimation : MonoBehaviour
{
    private Text _text;
    private bool _visible = false;
    [SerializeField]
    private float _time = 0.5f;

    void Awake()
    {
        _text = GetComponent<Text>();
        Invoke("TintText", _time);
    }

    private void TintText()
    {
        Color color = _text.color;
        color.a = (_visible == false) ? 1f : 0f;
        _text.color = color;
        _visible = !_visible;
        Invoke("TintText", _time);
    }
}
