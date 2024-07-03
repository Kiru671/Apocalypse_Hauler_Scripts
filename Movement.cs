using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Animations;
public class Movement : MonoBehaviour
{
    [SerializeField]
    float leftForceOffset;
    [SerializeField]
    float rightForceOffset;
    [SerializeField]
    float WASDForce;

    private float xMin = 0, xMax = 1f;

    public Rigidbody rb_;

    public XRGrabInteractable LeverLeft;
    public XRGrabInteractable LeverRight;
    public XRGrabInteractable JoystickLeft;
    public XRGrabInteractable JoystickRight;

    public Animation craneAnim;
    private Animator animator;

    private IEnumerator coroutine;

    private bool rotatingLong;
    private bool rotatingShort;
    public float L;
    public float S;
    public float R;

    private int rotationValLeft;
    private int rotationValRight;
    private int rotationValJoyRight;
    private int rotationValJoyLeft;

    // Start is called before the first frame update
    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        //StartCoroutine("Rotate4", 0.02f);
        //StartCoroutine("Rotate2", 0.02f);
        //StartCoroutine("Rotate3", 0.02f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            craneAnim.Play("Armature|LongArm");
            craneAnim["Armature|LongArm"].speed = 0;
            StartCoroutine("Rotate", 0.02f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            craneAnim.Play("Armature|LongArm");
            craneAnim["Armature|LongArm"].speed = 0;
            StartCoroutine("Rotate4", 0.02f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            craneAnim.Play("Armature|ShortArm");
            craneAnim["Armature|ShortArm"].speed = 0;
            StartCoroutine("Rotate3", 0.02f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            craneAnim.Play("Armature|ShortArm");
            craneAnim["Armature|ShortArm"].speed = 0;
            StartCoroutine("Rotate2", 0.02f);
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            craneAnim.Play("359rotate");
            craneAnim["359rotate"].speed = 0;
            StartCoroutine("Rotate5", 0.02f);
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            craneAnim.Play("359rotate");
            craneAnim["359rotate"].speed = 0;
            StartCoroutine("Rotate6", 0.02f);
        }
        if (Input.GetKey(KeyCode.W))
        { 
            rb_.AddRelativeForce(Vector3.forward * WASDForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb_.AddRelativeForce(Vector3.back * WASDForce, ForceMode.Impulse);
            //rb_.AddTorque(Vector3.up * WASDForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rb_.AddForce(Vector3.forward * WASDForce, ForceMode.Impulse);
            rb_.AddRelativeTorque(Vector3.down * WASDForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rb_.AddForce(Vector3.forward * WASDForce, ForceMode.Impulse);
            rb_.AddRelativeTorque(Vector3.up * WASDForce, ForceMode.Impulse);
        }
        if(true)//JoystickLeft.isSelected &! JoystickRight.isSelected)
        {   
            rotationValJoyLeft = Mathf.RoundToInt(JoystickLeft.transform.localEulerAngles.x);
            if(rotationValJoyLeft >= 180)
            {
                rotationValJoyLeft = 360 - rotationValJoyLeft;
                rotationValJoyLeft = rotationValJoyLeft * -1;
            }
            if(rotationValJoyLeft >= 0.1f && rotationValJoyLeft <= -0.1f)
            {
                StopCoroutine("Rotate2");
                StopCoroutine("Rotate3");
                //StopAllCoroutines();  //This might cause issues when 360 rotation is implemented.
            }
            if(rotationValJoyLeft < -0.5f)
            {
                craneAnim.Play("Armature|ShortArm");
                craneAnim["Armature|ShortArm"].speed = 0;
                StartCoroutine("Rotate2", rotationValJoyLeft / 10f);
            }
            if(rotationValJoyLeft  > 0.5f)
            {
                craneAnim.Play("Armature|ShortArm");
                craneAnim["Armature|ShortArm"].speed = 0;
                StartCoroutine("Rotate3", rotationValJoyLeft / 10f);
            }
        }
        if(true)//JoystickRight.isSelected &! JoystickLeft.isSelected)
        {
            rotationValJoyRight = Mathf.RoundToInt(JoystickRight.transform.localEulerAngles.x);
            if(rotationValJoyRight >= 180)
            {
                rotationValJoyRight = 360 - rotationValJoyRight;
                rotationValJoyRight = rotationValJoyRight * -1;
            }
            //Debug.Log(Mathf.Abs(JoystickRight.transform.localRotation.x));
            if(rotationValJoyRight > 0.1f && rotationValJoyRight < -0.1f)
            {
                StopCoroutine("Rotate");
                StopCoroutine("Rotate4");
            }
            if(rotationValJoyRight < -0.5f)
            {
                craneAnim.Play("Armature|LongArm");
                craneAnim["Armature|LongArm"].speed = 0;
                StartCoroutine("Rotate", JoystickRight.transform.rotation.x / 10f);
            }
            if(rotationValJoyRight  > 0.5f)
            {
                craneAnim.Play("Armature|LongArm");
                craneAnim["Armature|LongArm"].speed = 0;
                StartCoroutine("Rotate4", JoystickRight.transform.rotation.x / 10f);
            }
        }
        ///If any issues arise here, rotationVal being shared between levers might be the culprit
        if(true) //LeverLeft.isSelected)
        {
            rotationValLeft = Mathf.RoundToInt(LeverLeft.transform.localEulerAngles.x);     
            if(rotationValLeft >= 180)
            {
                rotationValLeft = 360 - rotationValLeft;
                rotationValLeft = rotationValLeft * -1;
            }
            rb_.AddRelativeForce(Vector3.forward * rotationValLeft * 9f, ForceMode.Impulse);
            rb_.AddTorque(Vector3.up * rotationValLeft * 12f, ForceMode.Impulse);
        }
        if(true)//LeverRight.isSelected)
        {

            rotationValRight = Mathf.RoundToInt(LeverRight.transform.localEulerAngles.x);
            if(rotationValRight >= 180)
            {
                rotationValRight = 360 - rotationValRight;
                rotationValRight = rotationValRight * -1;
            }
            rb_.AddRelativeForce(Vector3.forward * rotationValRight * 9f, ForceMode.Impulse);
            rb_.AddTorque(Vector3.up * -rotationValRight * 12f, ForceMode.Impulse);
            
        }
    }
    IEnumerator Rotate(float i)
    {
        //Saves value inside a variable to access when playing again, so that the animation doesn't break ;)  <---- KEREM YILDIZ MINDSET TESCİLLİ
        Debug.Log("Rotate");
        craneAnim["Armature|LongArm"].normalizedTime = L;
        craneAnim["Armature|LongArm"].normalizedTime += 0.005f;
        L = craneAnim["Armature|LongArm"].normalizedTime;
        L = Mathf.Clamp(L, xMin, xMax);
        yield return new WaitForSeconds(i);
        StopAllCoroutines();
    }
    IEnumerator Rotate4(float i)
    {
        Debug.Log("Rotate1");
        //Saves value inside a variable to access when playing again, so that the animation doesn't break ;)  <---- KEREM YILDIZ MINDSET TESCİLLİ
        craneAnim["Armature|LongArm"].normalizedTime = L;
        craneAnim["Armature|LongArm"].normalizedTime -= 0.005f;
        L = craneAnim["Armature|LongArm"].normalizedTime;
        L = Mathf.Clamp(L, xMin, xMax);
        yield return new WaitForSeconds(i);
    }
    IEnumerator Rotate2(float i)
    {
        Debug.Log("Rotate2");
        //Saves value inside a variable to access when playing again, so that the animation doesn't break ;) <---- KEREM YILDIZ MINDSET TESCİLLİ
        craneAnim["Armature|ShortArm"].normalizedTime = S;
        craneAnim["Armature|ShortArm"].normalizedTime += 0.005f;
        S = craneAnim["Armature|ShortArm"].normalizedTime;
        S = Mathf.Clamp(S, xMin, xMax);
        yield return new WaitForSeconds(i);
    }
    IEnumerator Rotate3(float i)
    {
        Debug.Log("Rotate3");
        //Saves value inside a variable to access when playing again, so that the animation doesn't break ;)  <---- KEREM YILDIZ MINDSET TESCİLLİ
        craneAnim["Armature|ShortArm"].normalizedTime = S;
        craneAnim["Armature|ShortArm"].normalizedTime -= 0.005f;
        S = craneAnim["Armature|ShortArm"].normalizedTime;
        S = Mathf.Clamp(S, xMin, xMax);
        yield return new WaitForSeconds(i);
    }
    IEnumerator Rotate5(float i)
    {
        Debug.Log("Rotate4");
        //Saves value inside a variable to access when playing again, so that the animation doesn't break ;) <---- KEREM YILDIZ MINDSET TESCİLLİ
        craneAnim["359rotate"].normalizedTime = R;
        craneAnim["359rotate"].normalizedTime += 0.005f;
        R = craneAnim["359rotate"].normalizedTime;
        yield return new WaitForSeconds(i);
    }
    IEnumerator Rotate6(float i)
    {
        //Saves value inside a variable to access when playing again, so that the animation doesn't break ;)  <---- KEREM YILDIZ MINDSET TESCİLLİ
        craneAnim["359rotate"].normalizedTime = R;
        craneAnim["359rotate"].normalizedTime -= 0.005f;
        R = craneAnim["359rotate"].normalizedTime;
        yield return new WaitForSeconds(i);
    }
}
