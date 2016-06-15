using UnityEngine;

[ExecuteInEditMode]
public class SetGlobalShaderVariables : MonoBehaviour
{

    public float spherifyFactor;
    public float sphereRadius;
    public float sphereOriginDistanceFromCamera;
    public float horizontalCurvature;

    void Update()
    {
        Shader.SetGlobalFloat("_SpherifyFactor", spherifyFactor);
        Shader.SetGlobalFloat("_SphereRadius", sphereRadius);
        Shader.SetGlobalFloat("_SphereOriginDistanceFromCamera", sphereOriginDistanceFromCamera);
        Shader.SetGlobalFloat("_HorizontalCurvature", horizontalCurvature);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.x = 0;
        pos.y -= sphereRadius;
        pos.z += sphereOriginDistanceFromCamera;
        Gizmos.DrawWireSphere(pos, sphereRadius);
    }
}
