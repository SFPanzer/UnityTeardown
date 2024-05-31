using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

[ExecuteAlways]
public class LineTest : MonoBehaviour
{
    [SerializeField] private Vector3[] controlPoints;

    [FormerlySerializedAs("vertexNumber")] [SerializeField] private int vertexesCount;

    private LineGenerator _lineGenerator;
    private Bezier _bezier;

    // Start is called before the first frame update
    void OnEnable()
    {
        _bezier = new Bezier(controlPoints);
        _lineGenerator = new LineGenerator(vertexesCount);
        gameObject.GetComponent<MeshFilter>().sharedMesh = _lineGenerator.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < vertexesCount; i++)
        {
            var t = 1.0f * i / (vertexesCount - 1);
            _bezier.EvaluateAtParameter(t, out _lineGenerator.Positions[i], out _lineGenerator.Directions[i]);
        }
        _lineGenerator.UpdateMesh();
    }
}