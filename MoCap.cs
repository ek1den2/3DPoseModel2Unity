using UnityEngine;

public class MoCap : MonoBehaviour
{
    Transform[] bones;
    Quaternion[] modelInitialRotations;
    Vector3[] positions;

    void Start()
    {
        // ヒューマノイドのボーン取得
        bones = new Transform[17];
        bones[0] = GetBone(HumanBodyBones.Hips);
        bones[1] = GetBone(HumanBodyBones.LeftUpperLeg);
        bones[2] = GetBone(HumanBodyBones.LeftLowerLeg);
        bones[3] = GetBone(HumanBodyBones.LeftFoot);
        bones[4] = GetBone(HumanBodyBones.RightUpperLeg);
        bones[5] = GetBone(HumanBodyBones.RightLowerLeg);
        bones[6] = GetBone(HumanBodyBones.RightFoot);
        bones[7] = GetBone(HumanBodyBones.Spine);
        bones[8] = GetBone(HumanBodyBones.Chest);
        bones[9] = GetBone(HumanBodyBones.Neck);
        bones[10] = GetBone(HumanBodyBones.Head);
        bones[11] = GetBone(HumanBodyBones.LeftUpperArm);
        bones[12] = GetBone(HumanBodyBones.LeftLowerArm);
        bones[13] = GetBone(HumanBodyBones.LeftHand);
        bones[14] = GetBone(HumanBodyBones.RightUpperArm);
        bones[15] = GetBone(HumanBodyBones.RightLowerArm);
        bones[16] = GetBone(HumanBodyBones.RightHand);

        // 初期回転を保存
        modelInitialRotations = new Quaternion[bones.Length];
        positions = new Vector3[bones.Length];

        for (int i = 0; i < bones.Length; i++)
        {
            if (bones[i] != null)
                modelInitialRotations[i] = bones[i].rotation;
        }

        // 仮のモーションデータを設定
        SetTestMotion();
    }

    void Update()
    {
        ApplyMotion();
    }

    void SetTestMotion()
    {
        // 仮のモーションデータ（歩く動きを再現）
        positions[0] = new Vector3(-0.0606756f ,  0.115487f,  0.662719f);   // Hips
        positions[1] = new Vector3(0.07134668f,  0.12994304f,  0.6566749f); // 左大腿
        positions[2] = new Vector3(0.05708124f, -0.31020805f,  0.6095687f);
        positions[3] = new Vector3(0.05945201f, -0.7037807f,  0.37042585f);
        positions[4] = new Vector3(-0.19269764f,  0.10103098f,  0.66876304f);  // 右大腿
        positions[5] = new Vector3(-0.05494196f, -0.30320993f,  0.5514247f);
        positions[6] = new Vector3(0.04088819f, -0.73459446f,  0.44851726f);
        positions[7] = new Vector3(-0.08588628f,  0.30283433f,  0.7997554f);
        positions[8] = new Vector3(-0.08387301f,  0.40559673f,  1.0353923f);
        positions[9] = new Vector3(-0.09517539f,  0.31227344f,  1.1117905f);
        positions[10] = new Vector3(-0.10536274f,  0.38482493f,  1.200436f);
        positions[11] = new Vector3(0.02505137f,  0.4509517f,  1.169645f); // 左腕
        positions[12] = new Vector3(0.25530457f,  0.3494758f,  1.1621732f);
        positions[13] = new Vector3(0.0545501f,  0.3998108f,  0.9752517f);
        positions[14] = new Vector3(-0.23300102f,  0.45371312f,  1.1634667f);  // 右腕
        positions[15] = new Vector3(-0.45146507f,  0.33436957f,  1.126078f);
        positions[16] = new Vector3(-0.222932f,  0.3937538f,  0.97766155f);
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = new Vector3(positions[i].x, positions[i].y, positions[i].z);
        }
    }

    void ApplyMotion()
    {
        // ルートボーン（Hips）の移動
        if (bones[0] != null)
            bones[0].position = positions[0];

        // 各ボーンの回転を計算
        for (int i = 1; i < bones.Length; i++)
        {
            if (bones[i] == null || bones[i - 1] == null) continue;

            Vector3 targetDirection = positions[i] - positions[i - 1];
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection) * modelInitialRotations[i];

            bones[i].rotation = targetRotation;
        }
    }

    Transform GetBone(HumanBodyBones bone)
    {
        Animator animator = GetComponent<Animator>();
        return animator.GetBoneTransform(bone);
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MotionTrack : MonoBehaviour
// {
//     // 最初のフレーム更新の前にStartが呼び出される
//     // pythonの送信ポートと合わせる
//     public int port = 2469;
//     private UdpClient udpClient;
//     private Thread receiveThread;
//     Transform[] bones;




//     void Start()
//     {
//         udpClient = new UdpClient(port);
//         receiveThread = new Thread(new ThreadStart(ReceiveData));
//         receiveThread.IsBackground = true;
//         receiveThread.Start();

//         // ボーン取得
//         bones = new Transform[17];
//         bones[0] = GetBone(Human)


        
//     }

//     void ApplyMotion(byte[] data)
//     {
//         float[] values
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     Transform GetBone(HumanBodyBones bone)
//     {
//         Animator animator = GetComponent<animator>();
//         return animator.GetBoneTransform(bone);
//     }
// }
