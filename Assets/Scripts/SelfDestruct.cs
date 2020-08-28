using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public enum DestroyType
    {
        None,
        Timer,
        Fade,
        FadeAfterTimer,
    }

    public DestroyType destroyType = DestroyType.None;
    public float destructionTimer = 1;
    public float fadeSpeed = 1;

    private void OnEnable()
    {
        switch (destroyType)
        {
            case DestroyType.Timer:
                Destroy(gameObject, destructionTimer);
                break;

            case DestroyType.Fade | DestroyType.FadeAfterTimer:
                StartCoroutine(DestroyWithFade());
                break;

            default:
                break;
        }
    }

    //for animations
    void SelfDestruction()
    {
        Destroy(gameObject);
    }

    public IEnumerator DestroyWithFade()
    {
        if (destroyType == DestroyType.FadeAfterTimer)
            yield return new WaitForSeconds(destructionTimer);
        SetMaterialsTransparent();
        float fadeAmount = 1;
        while (fadeAmount >= 0)
        {
            foreach (UnityEngine.Material m in gameObject.GetComponentInChildren<Renderer>().materials)
            {
                fadeAmount = m.color.a - (fadeSpeed * Time.fixedDeltaTime);

                m.color = new Color(m.color.r, m.color.g, m.color.b, fadeAmount);
            }
            yield return null;
        }

        Destroy(gameObject);
        yield return null;

    }

    void SetMaterialsTransparent()
    {
        foreach (UnityEngine.Material m in gameObject.GetComponentInChildren<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
        }
    }
}
