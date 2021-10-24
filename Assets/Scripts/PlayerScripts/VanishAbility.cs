using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishAbility : MonoBehaviour
{
    public SpriteRenderer renderer;
    public CanvasGroup healthCanvas;
    public bool isVanished = false;

    public void SetVanish(float alpha)
    {
        renderer.color = new Color(1, 1, 1, alpha);
        healthCanvas.alpha = alpha;
        isVanished = true;
        Invoke("CancelVanish", 2);
    }

    public void CancelVanish()
    {
        renderer.color = new Color(1, 1, 1, 1);
        healthCanvas.alpha = 1f;
        isVanished = false;
    }
}