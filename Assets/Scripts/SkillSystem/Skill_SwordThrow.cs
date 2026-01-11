using UnityEngine;

public class Skill_SwordThrow : Skill_Base
{
    [Range(0,10)]
    [SerializeField] private float throwPower = 5;
    [SerializeField] private float swordGravity = 3.5f;

    [Header("Trajectory prediction")]
    [SerializeField] private GameObject predictionDot;
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float spaceBetweenDots = .05f;
    private Transform[] dots;
    private Vector2 confirmedDirection;

    protected override void Awake()
    {
        base.Awake();
        dots = GenerateDots();
    }

    public void ThrowSword()
    {

    }

    public void PredictTrejactory(Vector2 direction)
    {
        for (int i=0; i<dots.Length; i++)
        {
            dots[i].position = GetTrajectoryPoints(direction, i * spaceBetweenDots);
        }
    }

    private Vector2 GetTrajectoryPoints(Vector2 direction, float t)
    {
        float scaledThrowPower = throwPower * 10;

        // This gives the initial velocity - the starting speed and direction of the throw;
        Vector2 initialVeclocity = direction * scaledThrowPower;

        // Gravity pulls the sword down over time. the longer it's in the air, the more it drops.
        Vector2 gravityEffect = 0.5f * Physics2D.gravity * swordGravity * (t * t);

        // We calculate how far the sword will travel after time 't',
        // by combining the initial throw direction with the gravity pulls,
        Vector2 predictedPoint = (initialVeclocity * t) + gravityEffect;

        Vector2 playerPosition = transform.root.position;

        return playerPosition + predictedPoint;
    }

    public void ConfirmTrajectory(Vector2 direction) => confirmedDirection = direction;

    public void EnableDots(bool enable)
    {
        foreach(Transform t in dots)
            t.gameObject.SetActive(enable);
    }

    private Transform[] GenerateDots()
    {
        Transform[] newDots = new Transform[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            newDots[i] = Instantiate(predictionDot, transform.position, Quaternion.identity, transform).transform;
            newDots[i].gameObject.SetActive(false);
        }

        return newDots;
    }
}
