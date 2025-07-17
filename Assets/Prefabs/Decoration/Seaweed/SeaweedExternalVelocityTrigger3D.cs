using System.Collections;
using UnityEngine;
public class SeaweedExternalVelocityTrigger3D : MonoBehaviour
{


    private SeaweedVelocityController _seaweedVelocityController;
    public GameObject _player;
    public Material _material;
    private Rigidbody _playerRB;

    private bool _easeInCoroutineRunning;
    private bool _easeOutCoroutineRunning;

    private int _fishlInfluence = Shader.PropertyToID("_FishInfluence");

    private float _startingXVelocity;
    private float _velocityLastFrame;


    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRB = _player.GetComponent<Rigidbody>();
        _seaweedVelocityController = GetComponentInParent<SeaweedVelocityController>();

        _material = GetComponent<SpriteRenderer>().material;
        _startingXVelocity = _material.GetFloat(_fishlInfluence);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == _player)
        {
            if (!_easeInCoroutineRunning && Mathf.Abs(_playerRB.linearVelocity.x) > Mathf.Abs(_seaweedVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseIn(_playerRB.linearVelocity.x * _seaweedVelocityController.FishInfluenceStrength));
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == _player)
        {
            StartCoroutine(EaseOut());
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == _player)
        {
            if (Mathf.Abs(_velocityLastFrame) > Mathf.Abs(_seaweedVelocityController.VelocityThreshold) &&
                Mathf.Abs(_playerRB.linearVelocity.x) < Mathf.Abs(_seaweedVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseOut());
            }

            else if (Mathf.Abs(_velocityLastFrame) < Mathf.Abs(_seaweedVelocityController.VelocityThreshold) &&
                Mathf.Abs(_playerRB.linearVelocity.x) > Mathf.Abs(_seaweedVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseIn(_playerRB.linearVelocity.x * _seaweedVelocityController.FishInfluenceStrength));
            }

            else if (!_easeInCoroutineRunning && !_easeOutCoroutineRunning &&
                Mathf.Abs(_playerRB.linearVelocity.x) > Mathf.Abs(_seaweedVelocityController.VelocityThreshold))
            {
                _seaweedVelocityController.InfluenceSeaweed(_material, _playerRB.linearVelocity.x * _seaweedVelocityController.FishInfluenceStrength);
            }


            _velocityLastFrame = _playerRB.linearVelocity.x;
        }
    }


    private IEnumerator EaseIn(float XVelocity)
    {
        _easeInCoroutineRunning = true;

        float elapsedTime = 0f;
        while (elapsedTime < _seaweedVelocityController.EaseInTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(_startingXVelocity, XVelocity, (elapsedTime / _seaweedVelocityController.EaseInTime));
            _seaweedVelocityController.InfluenceSeaweed(_material, lerpedAmount);

            yield return null;
        }

        _easeInCoroutineRunning = false;
    }

    private IEnumerator EaseOut()
    {
        _easeOutCoroutineRunning = true;
        float currentXInfluence = _material.GetFloat(_fishlInfluence);

        float elapsedTime = 0f;
        while (elapsedTime < _seaweedVelocityController.EaseOutTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(currentXInfluence, _startingXVelocity, (elapsedTime / _seaweedVelocityController.EaseOutTime));
            _seaweedVelocityController.InfluenceSeaweed(_material, lerpedAmount);

            yield return null;
        }

        _easeOutCoroutineRunning = false;
    }


}


