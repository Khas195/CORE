using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ultilities
{
    // Outdated, use Mathf.Lerp instead 
    public static float CalculateAsymptoticAverage(float currentValue, float target, float percentage)
    {
        var result = 0.0f;
        result = (1 - percentage) * currentValue + percentage * target;
        return result;
    }
    public static Vector3 CalculateMoveDirection(float horizontalInput, float forwardInput, Vector3 forwardDirection, Vector3 sideDirection)
    {
        Vector3 movedir;
        var forwardDir = forwardDirection * forwardInput;
        var sideDir = sideDirection * horizontalInput;
        movedir = forwardDir + sideDir;
        return movedir;
    }
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name.Equals(paramName))
            {
                return true;
            }
        }

        return false;
    }
    public static void SetValueInAnimator(this Animator animator, string paramName, float value)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetFloat(paramName, value);
        }
    }
    public static void SetValueInAnimator(this Animator animator, string paramName, int value)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetInteger(paramName, value);
        }
    }
    public static void SetValueInAnimator(this Animator animator, string paramName, bool value)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetBool(paramName, value);
        }
    }
    public static void SetValueInAnimator(this Animator animator, string paramName)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetTrigger(paramName);
        }
    }
}
