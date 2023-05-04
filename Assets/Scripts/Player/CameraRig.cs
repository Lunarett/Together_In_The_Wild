using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CameraRig : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private PhotonView pv;

    private void Awake()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        pv = transform.parent.GetComponent<PhotonView>();
    }

    private void Start()
    {
        gameObject.SetActive(pv.IsMine);

        SetFollowTarget(transform.parent);
        SetFollowOffsetZAxis(-10);
    }

    public void SetFollowTarget(Transform followTarget)
    {
        virtualCamera.Follow = followTarget;
    }

    public void SetFollowOffsetZAxis(float offset)
    {
        CinemachineTransposer transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer != null)
        {
            transposer.m_FollowOffset.z = offset;
        }
    }
}