namespace EnchantingGraph.Effects;

/// <summary>
/// There are five modes for every enchantment effect, defined by the combination of two axes
/// 1. Effect mode
///   - Instantaneous (capacitor and/or finisher in path): Dumps the entire manastream in a single burst of power
///   - Passive (all other cases): A sustained effect, for ongoing effects
/// 2. Port type
///   - Target: directed towards whatever is being hit by this weapon
///   - Structure: directed back into the physical item
///   - Wielder: directed onto the wielder of the item (i.e. through the hilt)
/// Passive + Target is not allowed, because TargetNodes have an implicit on-attack trigger.
/// </summary>
public enum EnchantmentEffect
{
    /// <summary>
    /// Effects related to emitting mana into the environment
    /// Structure:
    ///   - Instantaneous: An unfocused AoE explosion blasting everything around you
    ///   - Passive: Lengthen the blade or otherwise make the dangerous bit of the weapon bigger.
    ///         Element will influence power, cost, and size change.
    /// Target:
    ///   - Instantaneous: Unleash a ranged bolt of power. Shape is influenced by the weapon's attack style
    /// Wielder:
    ///   - Instantaneous: Applies either a recovery effect to the wielder, or damages them, based on element.
    ///         Harmful with many elements. 
    ///   - Passive: Applies a buff or debuff to the wielder, depending on the element.
    ///         The effect will be either a regen, degen, damaging buff, or increased incoming damage debuff
    /// </summary>
    Emission = 1,
    /// <summary>
    /// Effects related to imbuing mana onto things, thereby infusing them with power.
    /// Structure:
    ///   - Instantaneous: Converts weapon damage to the specified element. Very cheap.
    ///   - Passive: Applies elemental damage bonus to attacks with this weapon.
    /// Target:
    ///   - Instantaneous: Lodges a packet of mana in the target, applying a damaging debuff.
    /// Wielder:
    ///   - Instantaneous: Adds a damage negation shield for this element, up to a certain cumulative total
    ///   - Passive: Applies a damage resistance buff for this element
    /// </summary>
    Infusion = 2,
    /// <summary>
    /// TODO: Stuff related to pulling in mana
    /// Structure:
    ///   - Instantaneous:
    ///   - Passive: 
    /// Target:
    ///   - Instantaneous:
    /// Wielder:
    ///   - Instantaneous:
    ///   - Passive: 
    /// </summary>
    Absorption = 3,
    /// <summary>
    /// TODO: Stuff related to permanently creating objects. Expensive.
    /// Structure:
    ///   - Instantaneous:
    ///   - Passive: 
    /// Target:
    ///   - Instantaneous:
    /// Wielder:
    ///   - Instantaneous:
    ///   - Passive: 
    /// </summary>
    Creation = 4,
    /// <summary>
    /// TODO: Stuff related to permanently destroying objects. Expensive.
    /// Structure:
    ///   - Instantaneous:
    ///   - Passive: 
    /// Target:
    ///   - Instantaneous:
    /// Wielder:
    ///   - Instantaneous:
    ///   - Passive: 
    /// </summary>
    Destruction = 5,
}