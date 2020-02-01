    //The public float value which gets the private float value of PlayerHealth.
    public float PlyrHealth { get { return PlayerHealth; if(PlayerHealth == 0){m_Death.Invoke();}} }

    public float CstlHealth { get { return CastleHealth; if(CastleHealth == 0){m_Death.Invoke();} } }
    //The public float value which gets the private float value of PlayerHealth.
