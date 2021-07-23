using UnityEngine;
using UnityEngine.VFX;

public class WaveAnimation : MonoBehaviour
{
    [SerializeField] ComputeShader _compute = null;

    GraphicsBuffer _buffer;

    void Start()
    {
        _buffer = new GraphicsBuffer
          (GraphicsBuffer.Target.Structured, 64 * 64, 4 * sizeof(float));
        GetComponent<VisualEffect>().SetGraphicsBuffer("PointBuffer", _buffer);
    }

    void OnDestroy()
    {
        _buffer?.Dispose();
        _buffer = null;
    }

    void Update()
    {
        _compute.SetFloat("Time", Time.time);
        _compute.SetBuffer(0, "PointBuffer", _buffer);
        _compute.Dispatch(0, 8, 8, 1);
    }
}
