using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeLock : MonoBehaviour
{
    public GameObject objectToRotate;
    public Text lockNumText;
    private bool rotating;
    private float direction;
    private int lockNum = 0;
    private List<int> selectedNum;
    private int[] password = { 1, 2, 3 };
    public Text passwordCheckerText;

    void Start()
    {
        selectedNum = new List<int>();
        lockNumText.text = "0";
    }
    void Update()
    {
        direction = -1 * Input.GetAxisRaw("Horizontal");
        // lockNum = (direction < 0) ? lockNum - 1 : lockNum + 1;

        if (Mathf.Abs(direction) > 0)
        {
            StartRotation();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedNum.Add(lockNum);
            if (selectedNum.Count == 3)
            {
                passwordChecking();
            }
        }
    }
    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;
        Quaternion startRotation = objectToRotate.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            objectToRotate.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        objectToRotate.transform.rotation = endRotation;
        rotating = false;
    }

    public void StartRotation()
    {
        if (!rotating)
        {
            StartCoroutine(Rotate(new Vector3(0, 0, 18 * direction), 1));
            lockNum = (direction < 0) ? lockNum - 1 : lockNum + 1;
            lockNum = (lockNum < 0) ? 9 : lockNum;
            lockNum = (lockNum > 9) ? 0 : lockNum;
            // Debug.Log(lockNum);
            lockNumText.text = lockNum.ToString();

        }
    }

    private void passwordChecking()
    {
        for (int i = 0; i < password.Length; i++)
        {
            if (!(selectedNum.ToArray()[i] == password[i]))
            {
                selectedNum.Clear();
                passwordCheckerText.text = "Kode Salah";
                return;

            }
        }
        passwordCheckerText.text = "Kode benar";
    }
}
