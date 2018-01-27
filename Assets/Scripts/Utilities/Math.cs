using UnityEngine;

namespace Utilities {
    public class Math {
        public static float GetAngleBetweenVector2(Vector2 vectorA, Vector2 vectorB) {
            float angle =
                Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(vectorA, vectorB) / (vectorA.magnitude * vectorB.magnitude));

            return angle;
        }

        public static float GetRandomFromVector2(Vector2 vector2) {
            return Random.Range(vector2.x, vector2.y);
        }
        
        // In Deg, from x axis
        public static float GetAngle(float x, float y) {
            float angle = Mathf.Atan(y / x);
            angle = angle * Mathf.Rad2Deg; // convert to deg
            if (x < 0 && y > 0) {
                angle += 180f;
            } else if (x < 0 && y < 0) {
                angle += 180f;
            } else if (x > 0 && y < 0) {
                angle += 360f;
            }

            return angle;
        }
        
        public static float GetMagnitude(Vector2 vector2) {
            return Mathf.Sqrt(Mathf.Pow(vector2.x, 2) + Mathf.Pow(vector2.y, 2));
        }
    }
}