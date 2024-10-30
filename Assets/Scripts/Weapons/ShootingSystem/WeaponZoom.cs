using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class WeaponZoom : MonoBehaviour
{   
    [Header("Scope")]
    [SerializeField] bool hasScope;
    [SerializeField] Image scopeImage;
    [SerializeField] Camera weaponCamera;
    public bool isScoping;

    [Header("Post Processing")]
    [SerializeField] Volume postProcessVolume;
    [SerializeField][Range(0, 1)] float normalVignetteIntensity;
    [SerializeField][Range(0, 1)] float zoomedVignetteIntensity;


    [Header("Zoom Parameters")]
    [SerializeField] float zoomedFOV;
    [SerializeField] float zoomTime;
    [SerializeField] float normalFOV;

    Camera mainCam;

    Animator anim;

    Vignette vignette;
    
    
    private void Awake()
    {
       
        postProcessVolume.profile.TryGet<Vignette>(out vignette);
        anim = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        mainCam = Camera.main;
        normalFOV = mainCam.fieldOfView;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isScoping && hasScope)
            {
                anim.SetBool("IsScoped", true);
                StartCoroutine(ScopedZoom());
            }
            else
            {
                NormalZoom();
            }
            
        }
        else if(Input.GetMouseButtonUp(1))
        {
            
            anim.SetBool("IsScoped", false);
            weaponCamera.gameObject.SetActive(true);
            scopeImage.gameObject.SetActive(false);
            weaponCamera.cullingMask |= (1 << LayerMask.NameToLayer("Weapon"));
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFOV, zoomTime);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value,normalVignetteIntensity,zoomTime);
            isScoping = false;
        }
    }

    private void NormalZoom()
    {
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, zoomedFOV, zoomTime);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, zoomedVignetteIntensity, zoomTime);
    }

    private IEnumerator ScopedZoom()
    {   
        yield return new WaitForSeconds(0.15f);
        weaponCamera.gameObject.SetActive(true);
        scopeImage.gameObject.SetActive(true);
        weaponCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Weapon"));
        mainCam.fieldOfView = zoomedFOV;
        isScoping = true; 
    }
}
