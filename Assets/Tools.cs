using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static Vector3 YZero(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }
    public static Vector3 YZero(float x, float y)
    {
        return new Vector3(x, 0, y);
    }

    public static class Timer
    {
        public static float New(float Cooldown)
        {
            return Time.time + Cooldown;
        }

        public static bool Check(float next)
        {
            if (Time.time > next) return true;
            else return false;
        }
    }
}
