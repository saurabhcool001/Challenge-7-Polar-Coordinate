using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PolarToCartesian : MonoBehaviour
{
    // Sliders for polar coordinates
    public Slider radiusSlider;
    public Slider thetaSlider; // Angle in degrees for Cylindrical or azimuthal angle for Spherical
    public Slider phiOrHeightSlider;   // Polar angle (Spherical) or height (Cylindrical)

    // Texts to display slider values
    public TMP_Text radiusText;
    public TMP_Text thetaText;
    public TMP_Text phiOrHeightText;

    // Toggle to switch between modes
    public Toggle sphericalModeToggle; // If on, use Spherical mode; if off, use Cylindrical mode
    public TMP_Text modeText;

    // Object to position
    public Transform targetObject;

    // Update is called once per frame
    void Update()
    {
        // Get values from sliders
        float radius = radiusSlider.value;
        float theta = thetaSlider.value; // Angle in degrees
        float phiOrHeight = phiOrHeightSlider.value; // Angle in degrees (Spherical) or height (Cylindrical)

        // Update mode text based on the toggle
        if (sphericalModeToggle.isOn)
        {
            modeText.text = "Mode: Spherical";

            // Update text values for Spherical coordinates
            radiusText.text = "Radius: " + radius.ToString("F2");
            thetaText.text = "Theta: " + theta.ToString("F2") + "°";
            phiOrHeightText.text = "Phi: " + phiOrHeight.ToString("F2") + "°";

            // Convert Spherical coordinates to Cartesian
            Vector3 cartesianPosition = SphericalToCartesian(radius, theta, phiOrHeight);

            // Apply position to the target object
            targetObject.position = cartesianPosition;

            Debug.Log("Spherical Cartesian Position: " + cartesianPosition);
        }
        else
        {
            modeText.text = "Mode: Cylindrical";

            // Update text values for Cylindrical coordinates
            radiusText.text = "Radius: " + radius.ToString("F2");
            thetaText.text = "Theta: " + theta.ToString("F2") + "°";
            phiOrHeightText.text = "Height: " + phiOrHeight.ToString("F2");

            // Convert Cylindrical coordinates to Cartesian
            Vector3 cartesianPosition = CylindricalToCartesian(radius, theta, phiOrHeight);

            // Apply position to the target object
            targetObject.position = cartesianPosition;

            Debug.Log("Cylindrical Cartesian Position: " + cartesianPosition);
        }
    }

    // Function to convert spherical coordinates to Cartesian coordinates
    private Vector3 SphericalToCartesian(float radius, float theta, float phi)
    {
        // Convert angles from degrees to radians
        float thetaRad = Mathf.Deg2Rad * theta;
        float phiRad = Mathf.Deg2Rad * phi;

        // Calculate Cartesian coordinates
        float x = radius * Mathf.Sin(phiRad) * Mathf.Cos(thetaRad);
        float y = radius * Mathf.Sin(phiRad) * Mathf.Sin(thetaRad);
        float z = radius * Mathf.Cos(phiRad);

        return new Vector3(x, y, z);
    }

    // Function to convert cylindrical coordinates to Cartesian coordinates
    private Vector3 CylindricalToCartesian(float radius, float theta, float height)
    {
        // Convert angle from degrees to radians
        float thetaRad = Mathf.Deg2Rad * theta;

        // Calculate Cartesian coordinates
        float x = radius * Mathf.Cos(thetaRad);
        float y = radius * Mathf.Sin(thetaRad);
        float z = height;

        return new Vector3(x, y, z);
    }
}
