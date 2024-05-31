using UnityEngine;

namespace Utils
{
    public class LineGenerator
    {
        public readonly Vector3[] Positions;
        public readonly Vector3[] Directions;
        public readonly Mesh mesh;

        private readonly Vector3[] _vertexes;

        public LineGenerator(int vertexesCount, int subDivision = 8)
        {
            Positions = new Vector3[vertexesCount];
            Directions = new Vector3[vertexesCount];
            mesh = new Mesh()
            {
                name = "Line",
                
            };
        }

        public void UpdateMesh()
        {
            
        }
    }
}