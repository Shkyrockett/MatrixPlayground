namespace MatrixPlayground
{
    /// <summary>
    /// Angle of Incidence Categorization.
    /// </summary>
    public enum Incidence
        : sbyte
    {
        /// <summary>
        /// The angle of incidence is oblique.
        /// </summary>
        Oblique = -1,

        /// <summary>
        /// The angle of incidence is parallel.
        /// </summary>
        Parallel = 0,

        /// <summary>
        /// The angle of incidence is perpendicular.
        /// </summary>
        Perpendicular = 1,
    }
}
