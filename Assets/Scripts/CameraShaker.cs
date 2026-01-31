using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Header("Shake settings")]
    [SerializeField] private float frequency = 10f; // how “fast” it jitters
    [SerializeField] private float amplitude = 0.05f;
    private Vector3 _originalLocalPos;
    private Coroutine _shakeRoutine;

    private void Awake()
    {
        _originalLocalPos = transform.localPosition;
    }

    public void StartContinuousShake()
    {
        if (_shakeRoutine != null) StopCoroutine(_shakeRoutine);
        _shakeRoutine = StartCoroutine(ContinuousShake());
    }

    public void StopShake()
    {
        if (_shakeRoutine != null)
        {
            StopCoroutine(_shakeRoutine);
            _shakeRoutine = null;
        }

        transform.localPosition = _originalLocalPos;
    }

    private IEnumerator ContinuousShake()
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime * frequency;

            // Smooth random-ish jitter (no harsh snapping)
            float x = (Mathf.PerlinNoise(t, 0f) - 0.5f) * 2f;
            float y = (Mathf.PerlinNoise(0f, t) - 0.5f) * 2f;

            transform.localPosition = _originalLocalPos + new Vector3(x, y, 0f) * amplitude;

            yield return null;
        }
    }
}