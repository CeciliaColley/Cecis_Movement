using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Displacement
{
    private float secondsToMaxSpeed;
    private float secondsToStandstill;
    public float turnResistance;

    private float speedPercentage;
    private float turnDamping;

    public Displacement(float secondsToMaxSpeed, float secondsToStandstill, float turnResistance)
    {
        this.secondsToMaxSpeed = secondsToMaxSpeed;
        this.secondsToStandstill = secondsToStandstill;
        this.turnResistance = turnResistance;
    }

    public float GetSpeedPercentage()
    {
        return Mathf.Clamp(speedPercentage, 0f, 1f);
    }

    public IEnumerator Accelerate(bool speedUp)
    {
        float acelerationDuration = GetAccelerationDuration(speedUp);
        if (acelerationDuration == 0) { yield break; }

        float time = 0.0f;
        float initialSpeedLerpValue = speedPercentage;
        while (time <= acelerationDuration)
        {
            if (speedUp)
            {
                speedPercentage = initialSpeedLerpValue + ((time / acelerationDuration) * (1 - initialSpeedLerpValue));
            }
            else
            {
                speedPercentage = initialSpeedLerpValue - ((time / acelerationDuration) * initialSpeedLerpValue);
            }
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (!(time <= acelerationDuration)) { FinishAcceleration(speedUp); }
    }

    public void ApplyTurnDamping(Vector3 lastDirection, Vector3 inputDirection)
    {
        if (lastDirection == Vector3.zero)
        {
            turnDamping = 1.0f;
            return;
        }
        float turnAngle = Vector3.Angle(lastDirection, inputDirection);
        turnDamping = Mathf.Exp(-turnAngle / 180 * turnResistance);
        speedPercentage *= turnDamping;
    }

    private float GetAccelerationDuration(bool speedUp)
    {
        if (speedUp)
        {
            float secondsToSpeedUp = speedPercentage != 0 ? secondsToMaxSpeed - (speedPercentage * secondsToMaxSpeed) : secondsToMaxSpeed;
            return secondsToSpeedUp;
        }
        else
        {
            float secondsToSlowDown = (speedPercentage != 1) ? secondsToStandstill * speedPercentage : secondsToStandstill;
            return secondsToSlowDown;
        }
    }

    private void FinishAcceleration(bool speedUp)
    {
        if (speedUp)
        {
            speedPercentage = 1;
        }
        if (!speedUp)
        {
            speedPercentage = 0;
        }
    }
}
