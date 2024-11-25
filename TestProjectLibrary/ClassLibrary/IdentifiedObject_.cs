using System;
//namespace TS.Core.Objects;

/// <summary>
/// Объект, имеющий уникальный числовой идентификатор
/// </summary>
[Serializable]
public class IdentifiedObject : IEquatable<IdentifiedObject>
{
    protected IdentifiedObject(int id)
    {
        Id = id;
    }

    /// <summary>
    /// В этом классе используется идентификатор типа int потому, что объекты требуют частого использования GetHashCode(), который возвращает int.
    /// </summary>
    public readonly int Id;

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is IdentifiedObject identifiedObject && Equals(identifiedObject);

    /// <inheritdoc/>
    public bool Equals(IdentifiedObject other) => other is not null && Id == other.Id;

    /// <inheritdoc/>
    public static bool operator ==(IdentifiedObject a, IdentifiedObject b) => a is null ? b is null : b is not null && a.Id == b.Id;

    /// <inheritdoc/>
    public static bool operator !=(IdentifiedObject a, IdentifiedObject b) => !(a == b);

    /// <inheritdoc/>
    public override int GetHashCode() => Id;

    /// <inheritdoc/>
    public override string ToString() => Id.ToString();

}
