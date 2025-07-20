using System.Linq.Expressions;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
partial class SunMeter : VisualElement
{
    private const float MaxAngle = 180.0f;

    [SerializeField, DontCreateProperty]
    float m_Progress;

    [UxmlAttribute, CreateProperty]
    public float progress
    {
        get => m_Progress;
        set
        {
            m_Progress = Mathf.Clamp(value, 0.01f, 100.0f);
            MarkDirtyRepaint();
        }
    }

    public SunMeter()
    {
        generateVisualContent += GenerateVisualContent;
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        float width = contentRect.width;
        float height = contentRect.height;

        var painter = context.painter2D;
        painter.BeginPath();
        painter.lineWidth = 10.0f;
        painter.Arc(new Vector2(width * 0.5f, height), width * 0.5f, 180.0f, 0.0f);
        painter.ClosePath();
        painter.fillColor = Color.white;
        painter.Fill(FillRule.NonZero); 
        painter.Stroke();

        //Fill
        painter.BeginPath();
        painter.LineTo(new Vector2(width * 0.5f, height)); 
        painter.lineWidth = 10.0f;

        float amount = 180.0f * ((100.0f - progress) / 100f);

        painter.Arc(new Vector2(width * 0.5f, height), width * 0.5f, 180.0f, 0.0f - amount); 
        painter.ClosePath();
        painter.fillColor = Color.yellow;
        painter.Fill(FillRule.NonZero);
        painter.Stroke();
    }
}