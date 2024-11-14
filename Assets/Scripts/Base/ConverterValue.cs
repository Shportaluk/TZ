public static class ConverterValue
{
    public static float Map(float range1Val, float min1, float max1, float min2, float max2)
    {
        var oldRange = max1 - min1;
        var newRange = max2 - min2;

        return (range1Val - min1) * newRange / oldRange + min2;
    }
}