using UnityEngine;

namespace Utils
{
    public class Bezier
    {
        private readonly Vector3[] _controlPoints;

        public Bezier(Vector3[] controlPoints)
        {
            _controlPoints = controlPoints;
        }

        public void EvaluateAtParameter(float t, out Vector3 position, out Vector3 direction)
        {
            t = Mathf.Clamp01(t);
            position = Vector3.zero;
            direction = Vector3.zero;

            for (var i = 0; i < _controlPoints.Length; i++)
            {
                position += _controlPoints[i] * Bernstein(_controlPoints.Length - 1, i, t);
            }
        }

        private static int Factorial(int n)
        {
            var result = 1;
            for (var i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        private static float Bernstein(int n, int i, float t)
        {
            return 1.0f * Factorial(n) / (Factorial(i) * Factorial(n - i)) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
        }
    }
}