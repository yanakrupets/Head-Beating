using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalPainter : MonoBehaviour
{
    [SerializeField] private DecalProjector decalProjectorPrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private float decalLifetime = 2f;
    [SerializeField] private float decalExitTime = 1f;
    [SerializeField] private float decalSize = 0.2f;

    public void Paint(Vector3 position, Vector3 normal)
    {
        var decal = Instantiate(decalProjectorPrefab, position, Quaternion.identity, parentTransform);
        decal.transform.localPosition = parentTransform.InverseTransformPoint(position);
        decal.transform.rotation = Quaternion.LookRotation(-normal);
        decal.transform.localScale = Vector3.one * decalSize;
        
        StartCoroutine(FadeAndDestroy(decal));
    }

    private IEnumerator FadeAndDestroy(DecalProjector decal)
    {
        yield return new WaitForSeconds(decalLifetime);
        
        float elapsedTime = 0f;
        var start = decal.fadeFactor;
        while (elapsedTime < decalExitTime)
        {
            var t = elapsedTime / decalExitTime;
            decal.fadeFactor = Mathf.Lerp(start, 0, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        Destroy(decal.gameObject);
    }
}
