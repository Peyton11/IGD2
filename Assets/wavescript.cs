using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaveScript : MonoBehaviour
{
    public float amplitude = 1.0f;  // A: Amplitude of the wave
    public float wavelength = 2.0f; // λ: Wavelength of the wave
    public float speed = 1.0f;   // V: Velocity of the wave
    public float decaySpeed = 0.1f; // a: Speed of decay

    private Vector3[] originalVertices; // Store original mesh vertices
    private Vector3[] modifiedVertices; // Modified vertices during runtime
    private Mesh mesh;                  // Reference to the water mesh
    private bool isWaveActive = false;  // Track whether waves are active
    private float timeOfImpact;         // Time when the impact occurred
    private Vector3 entryPoint;         // P0(x0, z0): Center point of the wave

    void Start()
    {
        // Get the mesh attached to this object
        mesh = GetComponent<MeshFilter>().mesh;

        // Cache the original vertex positions
        originalVertices = mesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        
        // Keybinds to change values    
        if (Input.GetKeyDown(KeyCode.A)) amplitude += 0.1f;
        if (Input.GetKeyDown(KeyCode.B)) amplitude -= 0.1f;
        if (Input.GetKeyDown(KeyCode.K)) wavelength += 0.1f;
        if (Input.GetKeyDown(KeyCode.L)) wavelength -= 0.1f;
        if (Input.GetKeyDown(KeyCode.V)) speed += 0.1f;
        if (Input.GetKeyDown(KeyCode.N)) speed -= 0.1f;

        // Only calculate waves if they are active
        if (!isWaveActive) return;

        // Time since the wave started
        float t = Time.time - timeOfImpact;

        // Loop through each vertex in the mesh
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Get the original vertex position in local space
            Vector3 vertex = originalVertices[i];

            // Calculate the distance 'r' from the vertex to the wave's center (entryPoint)
            float r = Mathf.Sqrt(
                (transform.TransformPoint(vertex).x - entryPoint.x) * (transform.TransformPoint(vertex).x - entryPoint.x) +
                (transform.TransformPoint(vertex).z - entryPoint.z) * (transform.TransformPoint(vertex).z - entryPoint.z)
            );

            // Apply the wave equation to calculate y-displacement
            float displacement = amplitude * Mathf.Exp(-r * decaySpeed) *
                                 Mathf.Cos(2 * Mathf.PI * (r - speed * t) / wavelength);

            // Modify the y-position of the vertex
            vertex.y = displacement;

            // Add the modified vertex back to the array
            modifiedVertices[i] = vertex;
        }

        // Update the mesh
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals(); // Adjust normals for proper lighting
        mesh.RecalculateBounds(); // Update mesh bounds for accurate calculations
    }

    // Trigger waves when a diver enters the water
    private void OnTriggerEnter(Collider other)
    {
        // Set the entry point of the wave
        entryPoint = other.transform.position;

        // Activate the waves
        isWaveActive = true;

        // Record the time of impact
        timeOfImpact = Time.time;
    }
}
