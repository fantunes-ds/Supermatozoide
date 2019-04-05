using UnityEngine;

public class PickUpTrigger : MonoBehaviour
{
    [SerializeField] private string m_tagName;

    private void OnTriggerEnter2D(Collider2D p_other)
    {
        if (p_other.gameObject.CompareTag(m_tagName))
        {
            GetComponent<Controller>().AddProgesterone();
            Destroy(p_other.gameObject);
        }
    }
}
