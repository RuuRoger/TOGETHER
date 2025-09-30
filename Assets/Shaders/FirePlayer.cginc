float4 Brightness (float4 color, float brightness)
{
    float minimumBrigghtness = 0.2f;
    float4 result = color;

    result.rgb *= max(brightness, minimumBrigghtness);

    return result;
}