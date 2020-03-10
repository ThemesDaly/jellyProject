using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Data

    public enum State
    {
        Start,
        Stop
    }

    [SerializeField] private State state = State.Stop;

    [Header("Locomotion")]
    public Transform rootPlayer;
    public float speed = 5f;

    [Header("Size")]
    public float currentValue;
    public float smoothLerp = 4f;
    private const float minValue = 0f;
    private const float maxValue = 2f;
    private const float defaultValue = 2f;

    public Transform target;
    private Vector3 targetScale;
    private Vector3 targetPosition;

    public List<DynamicBone> listBoneUp = new List<DynamicBone>();

    #endregion

    #region Core

    public void Initialization(float initValue)
    {
        currentValue = initValue;
        speed = 0;

        rootPlayer.position = Vector3.zero;
        state = State.Stop;
    }

    public void CoreUpdate()
    {
        ControlSize();

        if (state == State.Start)
        {
            speed = Mathf.Clamp(speed, 10f, 25f);
            currentValue += InputManager.magnitude;
            Locomotion();
        }
    }

    private void ControlSize()
    {
        currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

        targetScale.x = defaultValue + maxValue - currentValue;
        targetScale.y = defaultValue + currentValue;
        targetScale.z = defaultValue;

        target.localScale = Vector3.Lerp(target.localScale, targetScale, smoothLerp * Time.deltaTime);

        targetPosition = target.localPosition;
        targetPosition.y = targetScale.y / 2;

        Vector3 boneForce = Vector3.zero;
        boneForce.y = 0.01f - ((currentValue * 100f / maxValue) / 10000f) * 2;

        foreach (DynamicBone bone in listBoneUp)
        {
            bone.m_Force = Vector3.Lerp(bone.m_Force, boneForce, smoothLerp * Time.deltaTime);
        }
    }

    private void Locomotion()
    {
        rootPlayer.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void StartGame()
    {
        state = State.Start;
    }

    public void StopGame()
    {
        state = State.Stop;
    }

    #endregion
}
