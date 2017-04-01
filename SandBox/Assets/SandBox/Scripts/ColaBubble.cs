using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

[RequireComponent(typeof(ParticleSystem))]
public class ColaBubble : MonoBehaviour
{
    public event System.Action BubblePourHandler;
    public event System.Action BubbleStillHandler;

    public float lifeTime = 10f;
    public GameObject bubbleLiquid;
    public Transform bubbleVestigital;
    public Material bubbleVestigitalMat;
    public ColaLiquid colaLiquidInCup;

    private bool isEmpty;
    public bool IsEmpty {
       get { return isEmpty; }
       set { isEmpty = value; }
       // get; set;
    }

    private ParticleSystem ps;
    private ParticleCollisionEvent[] collisionEvents;

    /*Particle Main*/
    private float lastLiftTime;
    private float originalSize;
    private float currentSize;
    private float originalAngle;
    private float currentAngle;

    /*Patticle Transfrom*/
    private float rotateOriginalAngleX;
    private float rotateCurrAngleX;
    private float rotateOffsetAngleX;

    private bool isCupLiquidShow;

    private bool isPouringInCup;

    private bool isPsUpdate;
    void Awake() {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[ps.maxParticles];

        lastLiftTime = lifeTime;
        originalSize = currentSize = ps.startSize;

        originalAngle = currentAngle = ps.shape.angle;

        rotateOriginalAngleX = this.transform.rotation.eulerAngles.x;
        rotateCurrAngleX = rotateOriginalAngleX;

        InitPS();

        isEmpty = false;

        LiquidInCupShow(false);

        isPouringInCup = false;

        isPsUpdate = false;
    }

    private void OnEnable()
    {
        colaLiquidInCup.FallOnTheFloor += OnLiquidCupFall;
    }

    private void OnDisable()
    {
        colaLiquidInCup.FallOnTheFloor -= OnLiquidCupFall;
    }

    private void Update() 
    {
        //if (!ps.isPlaying)
        //    return;

        if(!isPsUpdate)
            return;
        
        if (lastLiftTime > 0) 
        {
            currentSize = (originalSize / lifeTime) * 0.1f * lastLiftTime * lastLiftTime;
            ps.startSize = currentSize;
            if (ps.startSize < 0.01f)
            {
                isEmpty = true;
            }
        }

        rotateCurrAngleX = this.transform.rotation.eulerAngles.x;
        if (Mathf.Approximately(rotateCurrAngleX, rotateOriginalAngleX))
            return;
        if (rotateCurrAngleX > rotateOriginalAngleX) {
            rotateOffsetAngleX = rotateCurrAngleX - rotateOriginalAngleX;
        }
        else {
            rotateOffsetAngleX = rotateCurrAngleX + 90f;
        }

        var shape = ps.shape;
        shape.angle = currentAngle - rotateOffsetAngleX/180f*originalAngle;

        if (shape.angle < originalAngle * 2 / 3 && shape.angle > 0f) 
        {
            if (!isPouringInCup)
            {
                isPouringInCup = true;
                if (BubblePourHandler != null)
                    BubblePourHandler();
            }
        }
        else if (shape.angle > originalAngle * 2 / 3 && shape.angle < originalAngle)
        {
            if (isPouringInCup)
            {
                isPouringInCup = false;
                if (BubbleStillHandler != null)
                    BubbleStillHandler();
            }
        }

    }
    void OnParticleCollision(GameObject other) 
    {
       // Debug.Log("Paticle Collision object "+ other.name);
        int numCollisionEvents = ps.GetCollisionEvents(other,collisionEvents);
       // Debug.Log("Paticle num " + numCollisionEvents);
        int i = 0;

        while (i < numCollisionEvents) {
            if (collisionEvents[i].colliderComponent.gameObject.name.Equals("floor"))
            {
                Vector3 pos = collisionEvents[i].intersection;
                CreateVestigitalMesh(pos);
            }

            if (collisionEvents[i].colliderComponent.gameObject.name.Equals("CupBottomCollision"))
            {
                if (!isCupLiquidShow)
                    LiquidInCupShow(true);

                if (ps.startSize < 0.05)
                {
                    if (i%5 == 0 && i%3 == 0 && i%2 == 0)
                    {
                        if (colaLiquidInCup) {
                            colaLiquidInCup.LiquidUp();
                        }
                    }
                }
                else if(ps.startSize>0.05)
                {
                    if (i % 5 == 0 && i % 3 == 0) {
                        if (colaLiquidInCup) {
                            colaLiquidInCup.LiquidUp();
                        }
                    }
                }
            }
            i++;
        }
    }

    /*
    private void OnParticleTrigger()
    {
        var enter = new List<ParticleSystem.Particle>();
       // var exit = new List<ParticleSystem.Particle>();

        int enterNum = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < enterNum; i++)
        {
            ParticleSystem.Particle p = enter[i];
            
        }
    }
    */
    private IEnumerator WaitForOneSecond() {
        yield return new WaitForSeconds(5f);
        while (lastLiftTime > 0f) {
            lastLiftTime--;
            yield return new WaitForSeconds(1f);
        }
    }

    private void InitPS()
    {
        ps.playOnAwake = false;
        ps.loop = false;
        ps.startSpeed = 0.6f;
        ps.Stop();
    }

    private void CreateVestigitalMesh(Vector3 createdPos)
    {
        GameObject go = CreatePlane(ps.startSize, ps.startSize, false, bubbleVestigitalMat);
        go.transform.position = createdPos + new Vector3(ps.startSize / 2, 0f, -ps.startSize/2);
        go.transform.rotation = Quaternion.Euler(0, 0, 180);
        go.transform.localScale = Vector3.one;
        go.layer = 10;
        if (bubbleVestigital)
        {
            go.transform.SetParent(bubbleVestigital);
        }
    }

    private GameObject CreatePlane
        (float length, float width, bool enbleCollider, Material material)
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof (MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof (MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();
        m.vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(width,0,0), 
            new Vector3(width,0,length),
            new Vector3(0,0,length) 
        };

        m.uv = new Vector2[]
        {
            new Vector2(0,0), 
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0) 
        };

        m.triangles = new int[] {0, 1, 2, 0, 2, 3};

        m.RecalculateBounds();
        m.RecalculateNormals();

        mf.mesh = m;

        if (enbleCollider)
        {
            (go.AddComponent(typeof (MeshCollider)) as MeshCollider).sharedMesh = m;
        }

        mr.material = material;
        mr.reflectionProbeUsage = ReflectionProbeUsage.Off;

        return go;
    }

    private void RecyclePlaneAfterWhile()
    {
        
    }

    #region   Method for Liquid In Cup
    private void LiquidInCupShow(bool isShow)
    {
        this.isCupLiquidShow = isShow;
        if (colaLiquidInCup)
        {
            colaLiquidInCup.gameObject.SetActive(isShow);
        }
    }

    private void OnLiquidCupFall(Vector3 pos)
    {
        CreateVestigitalOnFloor(pos);
    }

    private void CreateVestigitalOnFloor(Vector3 createdPos) {
        GameObject go = CreatePlane(1f, 1f, false, bubbleVestigitalMat);
        go.transform.position = createdPos + new Vector3(0.5f, 0f, -0.5f);
        Vector3 pos = go.transform.position;
        go.transform.position = new Vector3(pos.x,0.42f,pos.z);
        go.transform.rotation = Quaternion.Euler(0, 0, 180);
        go.transform.localScale = Vector3.one;
        go.layer = 10;
        if (bubbleVestigital) {
            go.transform.SetParent(bubbleVestigital);
        }
    }
    #endregion

    #region Coroutine

    //TODO 一段时间后消失 mesh

    #endregion

    //PUBLIC METHODS
    public void EmitBubble() {
        ps.Play();
        StartCoroutine("WaitForOneSecond");
    }

    public void StopBubble() {
        ps.Stop();
        StopCoroutine("WaitForOneSecond");
    }

    public void ShowBubbleLiquid(bool isShowed) {
        if (bubbleLiquid) {
            bubbleLiquid.SetActive(isShowed);
        }
    }

    public void UpdatePS()
    {
        ps.loop = true;
        ps.startSpeed = 0.3f;
        isPsUpdate = true;
    }
}
