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
        // Keybinds to change values    
        if (Input.GetKeyDown(KeyCode.A)) amplitude += 0.1f;
        if (Input.GetKeyDown(KeyCode.B)) amplitude -= 0.1f;
        if (Input.GetKeyDown(KeyCode.K)) wavelength += 0.1f;
        if (Input.GetKeyDown(KeyCode.L)) wavelength -= 0.1f;
        if (Input.GetKeyDown(KeyCode.V)) speed += 0.1f;
        if (Input.GetKeyDown(KeyCode.N)) speed -= 0.1f;
        
        // Update time alive for wave propagation
        timeAlive += Time.deltaTime;

        // Calculate the wave's displacement based on the time
        float waveDistance = speed * timeAlive; // How far the wave has propagated
        
        // Set the wave position in the shader or material
        waveMaterial.SetFloat("_WaveDistance", waveDistance);
        waveMaterial.SetFloat("_Amplitude", amplitude);
        waveMaterial.SetFloat("_Wavelength", wavelength);
        
        // Debug.Log($"WaveDistance: {waveDistance}, Amplitude: {amplitude}, Wavelength: {wavelength}");
    }
    
    // Detect when the diver collides with the water
    private void OnTriggerEnter(Collider other)
    {
        // Capture the diver's position as the entry point
        entryPoint = other.transform.position;

        // Start the wave at the diver's entry position
        StartWave(entryPoint);

        Debug.Log("Diver hit the water! Wave started at: " + entryPoint);
    }


    // Function to start the wave from the point where the diver enters
    public void StartWave(Vector3 entryPoint)
    {
        // entryPoint.y = 0f; // Set the y position to 0
        this.entryPoint = entryPoint;
        timeAlive = 0f;
        // transform.position = entryPoint; // Position the wave at the entry point
        
        // Debug.Log("Wave successfully started at: " + entryPoint);
    }

    // Optional: Change the wave parameters (Amplitude, Wavelength, Speed)
    public void SetWaveParameters(float newAmplitude, float newWavelength, float newSpeed)
    {
        amplitude = newAmplitude;
        wavelength = newWavelength;
        speed = newSpeed;
    }
    
    
}
    