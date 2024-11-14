using System.Collections.Generic;
using UnityEngine;

public class PointsLerp
{
    private readonly List<Data> _data = new List<Data>();


    public void Add(float normalizedTime, Vector3 point)
    {
        _data.Add(new Data() { normalizedTime = normalizedTime, point = point });
    }

    public Vector3 Lerp(float t)
    {
        (Data from, Data to) = GetFromTo(t);
        float localT = ConverterValue.Map(t, from.normalizedTime, to.normalizedTime, 0, 1);
        return Vector3.Lerp(from.point, to.point, localT);
    }

    (Data, Data) GetFromTo(float t)
    {
        for (int i = 0; i < _data.Count - 1; i++)
        {
            var from = _data[i];
            var to = _data[i + 1];

            if (t >= from.normalizedTime && t <= to.normalizedTime)
            {
                return (from, to);
            }
        }

        return (_data[0], _data[1]);
    }

    private struct Data
    {
        public float normalizedTime;
        public Vector3 point;
    }
}