namespace EnchantingGraph.Data;

public enum Element
{
    /// <summary>
    /// Unstable or diffuse blend. a.k.a. Noise
    /// </summary>
    Neutral = 0,
    Heat = 1,
    Cold = 2,
    Water = 3,
    Electricity = 4,
    Earth = 5,
    Metal = 6,
    Air = 7,
    Light = 8,
    Dark = 9,
    /// <summary>
    /// Light + Dark, but only under special circumstances
    /// </summary>
    Shadow = 10,
    /// <summary>
    /// Light + Dark (or Heat + Cold + Air + Earth + Water + Electricity under special circumstances)
    /// </summary>
    Gray = 11,
    /// <summary>
    /// Dark + Earth
    /// </summary>
    Decay = 12,
    /// <summary>
    /// Light + Heat
    /// </summary>
    Solar = 13,
    /// <summary>
    /// Light + Metal
    /// </summary>
    Prismatic = 14,
    /// <summary>
    /// Light + Metal + Air
    /// </summary>
    Autonomic = 15,
    /// <summary>
    /// Light + Metal + Heat
    /// </summary>
    Fervor = 16,
    /// <summary>
    /// Dark + Water
    /// </summary>
    Abyssal = 17,
    /// <summary>
    /// Dark + Cold
    /// </summary>
    Void = 18,
    /// <summary>
    /// Dark + Earth + Cold
    /// </summary>
    Necrotic = 19,
    /// <summary>
    /// Fire + Air
    /// </summary>
    Fire = 20,
    /// <summary>
    /// Water + Cold
    /// </summary>
    Ice = 21,
}