using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class DelayRandom : MonoBehaviour
{

    private Renderer _myRenderer;
    private MaterialPropertyBlock _propertyBlock;

    public float ScaleRandom = 1f;

    private static readonly int Random = Shader.PropertyToID("_Random");

    private void Awake()
    {

        _myRenderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();

        SetRandomValue();
    }

    private void SetRandomValue()
    {

        float randomValue = UnityEngine.Random.value * ScaleRandom;
        _propertyBlock.SetFloat(Random, randomValue);

        _myRenderer.SetPropertyBlock(_propertyBlock);
    }
}
