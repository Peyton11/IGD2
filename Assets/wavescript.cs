using UnityEngine;

public class Wave : MonoBehaviour
{
    public float amplitude = 0.5f; // Amplitude of the wave
    public float wavelength = 5f;  // Wavelength of the wave
    public float speed = 2f;      // Speed of the wave
    public Vector3 entryPoint;    // The point where the diver enters the water
    
    private float timeAlive = 0f; // Time since wave started

    private Material waveMaterial;

    void Start()
    {
        // Get the material to modify the wave effect
        waveMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Update time alive for wave propagation
        timeAlive += Time.deltaTime;

        // Calculate the wave's displacement based on the time
        float waveDistance = speed * timeAlive; // How far the wave has propagated
        
        // Set the wave position in the shader or material
        waveMaterial.SetFloat("_WaveDistance", waveDistance);
        waveMaterial.SetFloat("_Amplitude", amplitude);
        waveMaterial.SetFloat("_Wavelength", wavelength);
    }

    // Function to start the wave from the point where the diver enters
    public void StartWave(Vector3 entryPoint)
    {
        this.entryPoint = entryPoint;
        transform.position = entryPoint; // Position the wave at the entry point
    }

    // Optional: Change the wave parameters (Amplitude, Wavelength, Speed)
    public void SetWaveParameters(float newAmplitude, float newWavelength, float newSpeed)
    {
        amplitude = newAmplitude;
        wavelength = newWavelength;
        speed = newSpeed;
    }
}
